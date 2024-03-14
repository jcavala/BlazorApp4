using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinForms1.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.Xml;

namespace WinForms1
{
    public partial class DocumentForm : Form
    {
        string jsonFile = Program.jsonFile;
        Mutex mutex;
        int verifiedSignatures;
        public DocumentForm()
        {
            InitializeComponent();
            mutex = new Mutex();
            load();

        }
        void load()
        {
            string json = File.ReadAllText(jsonFile);
            var bs = new BindingSource();
            bs.DataSource = JsonSerializer.Deserialize<List<Document>>(json);
            documentTable.DataSource = bs;
        }

        private void bDownloadDocument_Click(object sender, EventArgs e)
        {
            var file = documentTable.SelectedRows[0].Cells[1].Value.ToString();
            MyClient.TcpDownload(file);
            var document = getDocumentObject();
            document.IsDownloaded = true;

        }

        private void bSave_Click(object sender, EventArgs e)
        {
            var bs = (BindingSource)documentTable.DataSource;
            File.WriteAllText(jsonFile, JsonSerializer.Serialize((List<Document>)bs.DataSource));
        }

        private void bDelete_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in this.documentTable.SelectedRows)
            {
                var bs = (BindingSource)documentTable.DataSource;
                var list = (List<Document>)bs.DataSource;
                list.RemoveAt(row.Index);
                var bs2 = new BindingSource();
                bs2.DataSource = list;
                documentTable.DataSource = bs2;
            }

        }

        private void documentTable_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void bDownloadSignature_Click_1(object sender, EventArgs e)
        {
            var fileName = documentTable.SelectedRows[0].Cells[1].Value.ToString();
            string serverIpAddress = "127.0.0.1";
            int serverPort = 12346;
            using (UdpClient udpClient = new UdpClient())
            {
                try
                {
                    IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Parse(serverIpAddress), serverPort);
                    byte[] data = Encoding.UTF8.GetBytes(fileName);
                    udpClient.Send(data, data.Length, serverEndpoint);
                    IPEndPoint senderEndpoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedData = udpClient.Receive(ref senderEndpoint);
                    File.WriteAllBytes(Program.documentFolder + "\\" + fileName + "_signed.bin", receivedData);
                    FormLib.Message.ShowSuccess();
                }
                catch (Exception ex)
                {
                    FormLib.Message.ShowError(ex);

                }
            }
            var document = getDocumentObject();
            document.HasSignature = true;
        }

        Document getDocumentObject()
        {
            var objList = (List<Document>)((BindingSource)documentTable.DataSource).DataSource;
            string name = documentTable.SelectedRows[0].Cells[1].Value.ToString();
            return objList.Find(x => x.Name == name);
        }
        List<string> getDocumentList()
        {
            List<string> documents = new();
            foreach (DataGridViewRow row in documentTable.SelectedRows)
            {
                documents.Add((string)row.Cells[1].Value);
            }

            return documents;
        }
        void updateList(Document document)
        {
            var objList = (List<Document>)((BindingSource)documentTable.DataSource).DataSource;

        }

        private void bCheckSignature_Click_1(object sender, EventArgs e)
        {
            List<string> files = getDocumentList();

            verifiedSignatures = 0;

            foreach (var file in files)
            {
                ThreadPool.QueueUserWorkItem(checkSignature, file);
            }

        }

        private void checkSignature(object? file)
        {
            var documentFile = (string)file;
            var bytes = File.ReadAllBytes(Program.documentFolder + "\\" + documentFile + ".pdf");
            var signedHash = File.ReadAllBytes(Program.documentFolder + "\\" + documentFile + "_signed.bin");
            using SHA256 alg = SHA256.Create();
            byte[] hash = alg.ComputeHash(bytes);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new(Program.serverRSA); // verificiranje potpisa se radi sa javnim ključem poslužitelja
            rsaDeformatter.SetHashAlgorithm(nameof(SHA256));
            if (rsaDeformatter.VerifySignature(hash, signedHash)){

                if (mutex.WaitOne())
                {
                    try
                    {
                        var js1 = File.ReadAllText(jsonFile);
                        var docs = JsonSerializer.Deserialize<List<Document>>(js1);
                        docs.Where(d => d.Name == documentFile).First().SignatureIsValid = true;
                        var js2 = JsonSerializer.Serialize(docs);
                        File.WriteAllText(jsonFile, js2);

                    }
                    finally { mutex.ReleaseMutex(); }
                }
                MessageBox.Show("The signature is valid.");
            }
            else
            {
                MessageBox.Show("The signature is not valid.");
            }
        }
        private void bOpen_Click(object sender, EventArgs e)
        {
            var document = getDocumentObject();
            Process process = new Process();
            process.StartInfo = new ProcessStartInfo(@$"C:\dotnet\BlazorApp4\DocumentOpen\bin\Debug\net8.0\DocumentOpen.exe") { UseShellExecute = true };
            process.StartInfo.Arguments = Program.documentFolder + "\\" + document.Name + ".pdf";
            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                MessageBox.Show($"process failed {process.ExitCode}");
            }
        }

        private void bDownloadDocument_Click_1(object sender, EventArgs e)
        {
            var fileName = documentTable.SelectedRows[0].Cells[1].Value.ToString();
            try
            {
                var ipEndPoint = new IPEndPoint(IPAddress.Loopback, 12345);
                using TcpClient client = new();
                client.Connect(ipEndPoint);
                using NetworkStream stream = client.GetStream();
                string u_f = Program.user.name + "," + fileName;
                byte[] data = Encoding.ASCII.GetBytes(u_f);
                stream.Write(data, 0, data.Length);
                var buffer = new byte[1024 * 100];
                int received = stream.Read(buffer);
                byte[] encryptedData = new byte[received];
                Array.Copy(buffer, 0, encryptedData, 0, received);
                byte[] document;
                var aes = Program.aes;

                try
                {
                    using (MemoryStream msDecrypt = new MemoryStream())
                    {
                        using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                        {
                            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                            {
                                csDecrypt.Write(encryptedData, 0, encryptedData.Length);
                                csDecrypt.FlushFinalBlock();
                            }
                        }
                        document = msDecrypt.ToArray();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Greška! možda nisi dohvatio aes ključ! {ex.Message}");
                    document = Array.Empty<byte>();
                }
                var path = Program.documentFolder + "\\" + fileName + ".pdf";
                File.WriteAllBytes(path, document);
                FormLib.Message.ShowSuccess();
            }
            catch (Exception ex)
            {
                FormLib.Message.ShowError(ex);
            }
            var documentx = getDocumentObject();
            documentx.IsDownloaded = true;
        }

        private void bDocListDownload_Click(object sender, EventArgs e)
        {

            if (Program.token != null)
            {
                HttpClient client = new() { BaseAddress = new Uri($"{Program.serverUrl}") };
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Program.token.accessToken);
                var docs = client.GetAsync("getDocuments").Result.Content.ReadAsStringAsync().Result;
                MessageBox.Show(docs);
                File.WriteAllText(Program.jsonFile, docs);
                load();
            }
            else { MessageBox.Show("Nisi ulogiran!"); }

        }

        private void bRefresh_Click(object sender, EventArgs e)
        {
            load();
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
            return Name + ".pdf";
        }
    }
}
