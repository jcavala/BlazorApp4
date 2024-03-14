
using BlazorApp4.Components;
using BlazorApp4.Components.Account;
using BlazorApp4.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlazorApp4.Services;
using System.Security.Claims;
using SoapCore;
using SoapService;
using System.Text.Json;
using Hangfire;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRazorComponents().AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        
    }).AddIdentityCookies();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder().AddPolicy("api", p =>
{
    p.RequireAuthenticatedUser();
    p.AddAuthenticationSchemes(IdentityConstants.BearerScheme); 
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(connectionString));
builder.Services.AddQuickGridEntityFrameworkAdapter();;
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IPasswordHasher<ApplicationUser>, CustomPasswordHasher>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSoapCore();
builder.Services.AddSingleton<ICryptoService, CryptoService>();
builder.Services.AddScoped<ISoapService, MySoapService>();
builder.Services.AddHostedService<MyTcpBackgroundService>();
builder.Services.AddHostedService<UdpBackgroundService>();
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


var app = builder.Build();
app.MapGroup("api/auth").MapIdentityApi<ApplicationUser>();
app.MapGet("/requires-auth", (ClaimsPrincipal user) => $"Hello, {user.Identity?.Name}!").RequireAuthorization();
var jsonFile = "documents.json";
app.MapGet("/generateDocument",
    (string? FileName, string? ProductCategory, string? Location,string? FilterByDiscountValidity, ApplicationDbContext context,  ICryptoService cryptoService) 
    => {
        List<Product> products;
        if (String.IsNullOrEmpty(Location))
            products = context.Products.Include(product => product.Discount).Include(product => product.Store).ToList();
        else { 
            var stores = context.Stores.Include(s => s.Products).ThenInclude(p => p.Discount).ToList();
            products = stores.Where(s => s.Location.Equals(Location)).First().Products; 
        }
        
        var productsFiltered = !String.IsNullOrEmpty(ProductCategory) 
        ? products.Where(p => p.Category == ProductCategory) 
        : products;

        if (!String.IsNullOrEmpty(FilterByDiscountValidity))
        {
            var Date = DateTime.Now;

            productsFiltered = productsFiltered.Where(
                p => {
                    if (p.Discount != null)
                    {
                        return p.Discount.StartDate.CompareTo(Date) == -1
                        && p.Discount.EndDate.CompareTo(Date) == 1;
                    }
                    else return false;
                }
                );
        }
        
        productsFiltered =productsFiltered.OrderBy(p => p.ProductId);
        Product.GeneratePDF(productsFiltered, FileName);
        Signature.signPdf(FileName,cryptoService.getRsa());
        string json = File.ReadAllText(jsonFile);
        List<Document> documents;
        try { documents = JsonSerializer.Deserialize<List<Document>>(json); }
        catch(Exception e) { documents = new List<Document>(); }  
        var d = new Document();
        d.Id = documents.Count+1;
        d.Name = FileName; d.SignatureIsValid = false;
        d.HasSignature = false;
        d.IsDownloaded = false;
        documents.Add(d);
        string docsText = JsonSerializer.Serialize(documents);
        File.WriteAllText(jsonFile, docsText);
        return "request successfully processed!";
    });

app.MapGet("/getDocuments",(HttpContext context, UserManager<ApplicationUser> manager) =>
{
    var user =manager.GetUserAsync(context.User).Result;
    if (user.Role == "privileged")
        return File.ReadAllText(jsonFile);
    else { context.Response.StatusCode = 403; return "unauthorized"; }
    })
    .RequireAuthorization("api");

app.MapGet("createJob", () => {
    
    return "";
});

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapGet("/test", (ApplicationDbContext context) => {
    var z = new Store() { Name = "Konzum", Location = "Zagreb", Products=new() };
    
    var x = new Discount() { Amount=0.1m,StartDate=DateTime.Now.AddDays(-1),EndDate = DateTime.Now.AddDays(30)
    };
    context.Discounts.Add(x);
    context.Stores.Add(z);
    var y = new Product() { Category="food",Name="Sir",Price=2.99m,Discount=x,Store=z};
    
    
    context.Products.Add(y);
    z.Products.Add(y);
    y.Discount = x;
    context.SaveChanges();
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode().AddInteractiveWebAssemblyRenderMode();

app.MapAdditionalIdentityEndpoints();
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ISoapService>("/soap", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
    endpoints.MapControllers();
    endpoints.MapHangfireDashboard();
});

if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }



app.Run();

