using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;

namespace BlazorApp4.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public int DiscountId { get; set; }
        public int StoreId { get; set; }
        public Discount Discount { get; set; }
        public Store Store { get; set; }
       

        public string ToHtml()
        {
            
            decimal discountedPrice = Price - Price * Discount.Amount;
            var discount = Discount!=null ? Discount.Amount : 0m;
            var validUntil = Discount != null ? Discount.EndDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture) : "    ";
            return $"<tr><td>{ProductId}</td><td>{Name}</td><td>{Category}</td><td>{discountedPrice}<td>{discount}</td></td> <td>{validUntil}</td> <td>{Store}</td></tr>";
        }

        public static void GeneratePDF(IEnumerable<Product> products, string fileName)
        {
            string start = "<html> <head>\r\n<style>\r\n#products {font-family: Arial, Helvetica, sans-serif; border-collapse: collapse;width: 100%;} #products td, #products th {border: 1px solid;padding: 8px;} #products tr:nth-child(even){background-color: #f2f2f2;} #products th {padding-top: 12px;padding-bottom: 12px;padding-left: 10px;text-align: left;background-color: #04AA6D;color: white;}\r\n</style></head><body> <table id=\"products\">";
            string end = " </table> </body> </html>";
            string header = $" <tr> <th>ID</th> <th>Name</th>\r\n  <th>Category</th>  <th>Price</th> <th>Discount Amount</th> <th> Discount Valid Until </th> <th>Store</th> </tr>";
            StringBuilder sb = new StringBuilder();
            sb.Append(start);
            sb.Append(header);
            foreach (Product product in products)
            {
                sb.Append(product.ToHtml());
            }
            sb.Append(end);

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(sb.ToString());
            doc.Save(fileName+".pdf");
            doc.Close();
        }
        public decimal discountedPrice() { return Price - Price * Discount.Amount; }
    }

    public class Discount
    {

        public int DiscountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public override string? ToString()
        {
            return Amount.ToString();
        }
    }

    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<Product> Products { get; set; }

        public override string? ToString()
        {
            return Name + ", " + Location;
        }
    }


    class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDownloaded { get; set; }
        public bool HasSignature { get; set; }
        public bool SignatureIsValid { get; set; }

        public override string? ToString()
        {
            return Id.ToString() + "-" + Name + "-" + IsDownloaded + "-" + HasSignature + "-" + SignatureIsValid;
        }
    }
}
