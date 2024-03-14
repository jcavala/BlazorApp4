using BlazorApp4.Data;
using BlazorApp4.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BlazorApp4.Jobs
{
    public class ReportJob
    {
        public ApplicationDbContext Context { get; set; }
        public ICryptoService cryptoService { get; set; }
        public ReportJob(ApplicationDbContext context, ICryptoService _cryptoService) 
        {Context = context; 
            cryptoService = _cryptoService; }
        
        public void Execute()
        {
             Context.Products.ToList().ForEach(x=>Console.WriteLine(x.Name));
            
        }
        public void CreateAnalyticsReport()
        {
            var graph = new Graph();
            var groups =Context.Products.GroupBy(x => x.Category);
            foreach (var group in groups)
            {
                graph.counts.Add(group.Count());
                var maxVals = group.Max(x => x.discountedPrice());
                graph.maxPrices.Add(maxVals);
                var sums = group.Sum(x => x.discountedPrice());
                graph.sums.Add(sums);
                var minVals = group.Min(x => x.discountedPrice());
                
                var avgVals = group.Average(x=>x.discountedPrice());
                graph.avgPrices.Add(avgVals);
                graph.categories.Add(group.Key);
            }
            var html = graph.create();

            File.WriteAllText("analytics.html", html);
            
        }

        public void documentGeneration(string? FileName, string? ProductCategory, string? Location, string? FilterByDiscountValidity)
        {
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
                    }
                    );
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
            return;
        }
    }
}
