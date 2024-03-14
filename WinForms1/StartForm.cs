using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoapService;
namespace WinForms1
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void bLogin_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
        }

        private void bKeysForm_Click(object sender, EventArgs e)
        {

            new GetKeyForm().Show();
        }

        private void bDoc_Click(object sender, EventArgs e)
        {
            var x = new FormLib.RequestDocumentGeneration();
            x.location = Program.Location;
            x.Show();
        }

        private void bUserSettingsClick(object sender, EventArgs e)
        {
            new EditUserDataForm().Show();
        }

        private void bSettings_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void bDocuments_Click(object sender, EventArgs e)
        {
            new DocumentForm().Show();
        }

        private void bGetRsa_Click(object sender, EventArgs e)
        {
            ISoapService client = new SoapServiceClient(SoapServiceClient.EndpointConfiguration.BasicHttpBinding_ISoapService_soap);
            var pk = client.GetServerPublicKeyAsync(new GetServerPublicKeyRequest() { Body = new GetServerPublicKeyRequestBody() { } });
            var result = pk.Result;
            Program.serverRSA.FromXmlString(result.Body.GetServerPublicKeyResult);
            // label1.Text = result.Body.GetServerPublicKeyResult;
        }

        private void bGetAesKey_Click(object sender, EventArgs e)
        {
            ISoapService client = new SoapServiceClient(SoapServiceClient.EndpointConfiguration.BasicHttpBinding_ISoapService_soap);

            var response =client.GetSymmetricKeyAsync(
                new GetSymmetricKeyRequest(new GetSymmetricKeyRequestBody(
                    Program.userRSA.ToXmlString(false), Program.user.name)));
            var bytes = response.Result.Body.GetSymmetricKeyResult;
            var data = Program.userRSA.Decrypt(bytes, RSAEncryptionPadding.Pkcs1);
            UserKeyManager.Add(Program.user.name, data);
            var x = new byte[32];
            var y = new byte[16];
            Array.Copy(data, 0, x, 0, 32);
            Program.aes.Key = x;
            Array.Copy(data, 32, y, 0, 16);
            Program.aes.IV = y;
        }
    }
}
