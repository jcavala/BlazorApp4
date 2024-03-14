using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace WinForms1
{
    public partial class RequestDocumentGeneration : Form
    {
        public RequestDocumentGeneration()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bRequest_Click(object sender, EventArgs e)
        {
            HttpClient client = new() { };
            
            string uri = $"{Program.serverUrl}/generateDocument?";
            
            if (!String.IsNullOrEmpty(tbDocumentName.Text))
                uri = uri + $"FileName={tbDocumentName.Text}";
            if (!String.IsNullOrEmpty(tbCategory.Text))
                uri = uri + "&" + $"Category={tbCategory.Text}";
            if (!String.IsNullOrEmpty(tbLocation.Text))
                uri = uri + "&" + $"Location={tbLocation.Text}";
            if (!cbIgnoreDate.Checked)
                uri = uri + "&" + $"Date={dtpDate.Value}";
            if (!String.IsNullOrEmpty(tbStoreId.Text))
                uri = uri + "&" + $"StoreId={tbStoreId.Text}";

            lRequestShow.Text = uri;
            var res = client.GetAsync(uri);
            lRequestShow.Text = res.Result.Content.ReadAsStringAsync().Result;
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            HttpClient client = new() {BaseAddress=new Uri($"{Program.serverUrl}") };
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.token.accessToken);
            var docs = client.GetAsync("getDocuments").Result.Content.ReadAsStringAsync().Result;
            MessageBox.Show(docs);
            File.WriteAllText(Program.jsonFile,docs);
        }
    }
}
