using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormLib
{
    public partial class RequestDocumentGeneration : Form
    {
         string serverUrl = "http://localhost:8087";
         public string location {  get; set; }
        public RequestDocumentGeneration()
        {
            InitializeComponent();
            tbLocation.Text = location;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient() { };
            string uri = $"{serverUrl}/generateDocument?";
            if (!String.IsNullOrEmpty(tbDocumentName.Text))
                uri = uri + $"FileName={tbDocumentName.Text}";
            if (!String.IsNullOrEmpty(tbCategory.Text))
                uri = uri + "&" + $"ProductCategory={tbCategory.Text}";
            if (!String.IsNullOrEmpty(tbLocation.Text))
                uri = uri + "&" + $"Location={tbLocation.Text}";
            if (cbFilterByDiscountValidity.Checked)
                uri = uri + "&" + "FilterByDiscountValidity=true";
            lRequestShow.Text = uri;
            var res = client.GetAsync(uri);
            lRequestShow.Text = res.Result.Content.ReadAsStringAsync().Result;
        }
    }
}
