using SoapService;
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
using WinForms1.Data;

namespace WinForms1
{
    public partial class GetKeyForm : Form
    {
        public GetKeyForm()
        {
            InitializeComponent();
        }

        private void GetRsaKeyClick(object sender, EventArgs e)
        {
            ISoapService client = new SoapServiceClient(SoapServiceClient.EndpointConfiguration.BasicHttpBinding_ISoapService_soap);
            var pk = client.GetServerPublicKeyAsync(new GetServerPublicKeyRequest() { Body = new GetServerPublicKeyRequestBody() { } });
            var result = pk.Result;
            Program.serverRSA.FromXmlString(
                result.Body.GetServerPublicKeyResult);
            label1.Text = result.Body.GetServerPublicKeyResult;
        }

        private void GetAesKey_Click(object sender, EventArgs e)
        {
            ISoapService client = new SoapServiceClient(SoapServiceClient.EndpointConfiguration.BasicHttpBinding_ISoapService_soap);

            var response =
                client.GetSymmetricKeyAsync(new GetSymmetricKeyRequest(new GetSymmetricKeyRequestBody(
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
            label1.Text = BitConverter.ToString(data);
            label2.Text = BitConverter.ToString(x);
            label3.Text = BitConverter.ToString(y);
            
            
        }

        private void bTest1_Click(object sender, EventArgs e)
        {
            MyClient.TcpDownload("dokument3");


        }

        private void bTestUdp_Click(object sender, EventArgs e)
        {
            MyClient.UdpDownload("file");

        }

        private void bDocuments_Click(object sender, EventArgs e)
        {
            new DocumentForm().Show();
        }

        private void bDocGen_Click(object sender, EventArgs e)
        {
            //new RequestDocumentGeneration().Show();
        }

        private void bUsers_Click(object sender, EventArgs e)
        {
            new FormLib.Users().Show();
        }

        private void GetKeyForm_Load(object sender, EventArgs e)
        {

        }
    }
}
