using BlazorApp4.Data;
using BlazorApp4.Jobs;
using BlazorApp4.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;
using System.IO;

namespace BlazorApp4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public ApplicationDbContext Context { get; set; }
        ICryptoService cryptoService { get; set; }

        public JobController(ApplicationDbContext context, ICryptoService cryptoService)
        {
            Context = context;
            this.cryptoService = cryptoService;
        }
        [HttpGet]
        public ActionResult Get()
        {
            BackgroundJob.Enqueue<ReportJob>((x) =>  x.Execute());
            return Ok();
        }
        [HttpGet]
        [Route("recurring")]
        public ActionResult Recurring() {
            RecurringJob.AddOrUpdate(() => Console.WriteLine("recurring job triggered"), "* * * * *");
            return Ok(); 
        }

        [HttpGet]
        [Route("documentGeneration")]
        public IActionResult documentGeneration(string? FileName, string? ProductCategory, string? Location, string? FilterByDiscountValidity, string cron ) {
            if (!String.IsNullOrEmpty(cron))
            {
                RecurringJob.AddOrUpdate(() => documentGeneration(FileName, ProductCategory, Location, FilterByDiscountValidity),cron); ;
            }
            else
            {
                documentGeneration(FileName, ProductCategory, Location, FilterByDiscountValidity);
            }
            return Ok();
        }

        public async Task documentGeneration(string? FileName, string? ProductCategory, string? Location, string? FilterByDiscountValidity) {
        var jsonFile = "documents.json";
        List<Product> products;
        if (String.IsNullOrEmpty(Location))
            products = Context.Products.Include(product => product.Discount).Include(product => product.Store).ToList();
        else
        {
            var stores = Context.Stores.Include(s => s.Products).ThenInclude(p => p.Discount).ToList();
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
                });
        }

        productsFiltered = productsFiltered.OrderBy(p => p.ProductId);
        Product.GeneratePDF(productsFiltered, FileName);
        Signature.signPdf(FileName, cryptoService.getRsa());
        string json = System.IO.File.ReadAllText(jsonFile);
        List<Document> documents;
        try { documents = JsonSerializer.Deserialize<List<Document>>(json); }
        catch (Exception e) { documents = new List<Document>(); }
        var d = new Document();
        d.Id = documents.Count + 1;
        d.Name = FileName; d.SignatureIsValid = false;
        d.HasSignature = false;
        d.IsDownloaded = false;
        documents.Add(d);
        string docsText = JsonSerializer.Serialize(documents);
        System.IO.File.WriteAllText(jsonFile, docsText);
    }

        [HttpGet]
        [Route("analyticsReport")]
        public ActionResult AnalyticsReport() {
            var graph = new Graph();
            var products = Context.Products.Include(x => x.Discount);
            var groups = products.GroupBy(x => x.Category);
            foreach (var group in groups)
            {
                graph.counts.Add(group.Count());
                var maxVals = group.Max(x => x.discountedPrice());
                graph.maxPrices.Add(maxVals);
                var sums = group.Sum(x => x.discountedPrice());
                graph.sums.Add(sums);
                var avgVals = group.Average(x => x.discountedPrice());
                graph.avgPrices.Add(avgVals);
                graph.categories.Add(group.Key);
            }
            var html = graph.create();
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(html);
            DateTimeFormatInfo fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            doc.Save($"analytics.pdf-{DateTime.Now.ToString("dd-MM-yy")}");
            doc.Close();
            System.IO.File.WriteAllText("analytics.html", html);
            return Ok();
        }
    }
}
