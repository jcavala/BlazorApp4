using SoapCore;
using SoapService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSoapCore();
builder.Services.AddScoped<ISoapService, MySoapService>();
var app = builder.Build();




app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ISoapService>("/", new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
});



app.Run();
