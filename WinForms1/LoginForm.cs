using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms1.Data;

namespace WinForms1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Program.Language);
            InitializeComponent();
            tbName.Text = Program.user.name;
            tbPassword.Text = "User2\"";
            
            System.Reflection.Assembly otherAssembly = System.Reflection.Assembly.Load("FormLib");
            System.Resources.ResourceManager resManager = new System.Resources.ResourceManager("FormLib.Properties.Resources", otherAssembly);
            var test = resManager.GetObject("profile_icon");
            pbLoginIcon.Image = (Bitmap)test;
            Console.WriteLine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HttpClient client = new() { BaseAddress = new Uri(Program.serverUrl) };
            HttpResponseMessage response;
            if (cbRegister.Checked) { response = client.PostAsJsonAsync("register", new { email = tbName.Text, password = tbPassword.Text }).Result; }
            else
            {
                response = client.PostAsJsonAsync("api/auth/login", new { email = tbName.Text, password = tbPassword.Text }).Result;

                var json = response.Content.ReadAsStringAsync().Result;
                var token = JsonSerializer.Deserialize<Token>(json);
                MessageBox.Show($"success, token: {token.accessToken}");
                Program.token = token;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void bRDG_Click(object sender, EventArgs e)
        {
            new RequestDocumentGeneration().Show();
        }
    }
    internal class Token
    {
        
        public string accessToken { get; set; }

        
        public string tokenType { get; set; }

        
        public int expiresIn { get; set; }

        
        public string refreshToken { get; set; }
    }
}
