using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms1.Data
{
    class Product
    {
        public int ProductId;
        public string ProductName;
        public decimal Price;
        public int ammount;
    }

    class Order
    {
        public DateTime date { get; set; }
        public User user { get; set; }
        public List<Product> products { get; set; }
        public decimal Total { get; set; }
        public Order()
        {
            products = new List<Product>();
            date = DateTime.Now;
        }
    }

    public class User
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
 
    }

    public class UserSettings
    {
        public string name { get; set; }
        public string language { get; set; }
        public string location { get; set; }
        public bool active {  get; set; }
    }
}
