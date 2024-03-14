using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.InteropServices;

namespace WinForms1.Data
{
    internal class MyClient
    {
        public static byte[] AesDecrypt(byte[] encryptedData)
        {
            var aes = Program.aes;
            
            try
            {
                using (MemoryStream decryptedMemoryStream = new MemoryStream())
                {
                    using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(decryptedMemoryStream, decryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(encryptedData, 0, encryptedData.Length);
                            cryptoStream.FlushFinalBlock();
                        }
                    }
                    return decryptedMemoryStream.ToArray();

                }
            }
            catch(Exception e) {
                MessageBox.Show($"Greška! možda nisi dohvatio aes ključ! {e.Message}");
                return new byte[0];
            }
        }








        public static void TcpDownload(string fileName)
        {
            try
            {
                var ipEndPoint = new IPEndPoint(IPAddress.Loopback, 12345);
                using TcpClient client = new();
                client.Connect(ipEndPoint);
                using NetworkStream stream = client.GetStream();
                string u_f = Program.user.name + "," + fileName;
                Byte[] data = Encoding.ASCII.GetBytes(u_f);
                stream.Write(data, 0, data.Length);
                var buffer = new byte[1024 * 100];
                int received = stream.Read(buffer);
                byte[] recievedData = new byte[received];
                Array.Copy(buffer, 0, recievedData, 0, received);
                var document = AesDecrypt(recievedData);
                var path = Program.documentFolder + "\\" + fileName + ".pdf";
                File.WriteAllBytes(path, document);
                FormLib.Message.ShowSuccess();
            }
            catch(Exception e)
            {
                FormLib.Message.ShowError(e);
            }
            
        }

        public static void UdpDownload(string fileName)
        {
            
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
                    File.WriteAllBytes(Program.documentFolder + "\\"+ fileName + "_signed.bin", receivedData);
                    FormLib.Message.ShowSuccess();
                }
                catch (Exception ex)
                {
                    FormLib.Message.ShowError(ex);
                    
                }
            }
        } 

           
            public void signatureVerify(string fileName)
            {
                RSA rsa = Program.serverRSA;

                RSAPKCS1SignatureDeformatter rsaDeformatter = new(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA256));
                byte[] signedHash = File.ReadAllBytes(Program.documentFolder + "\\" + fileName + "_signed.bin");
                using SHA256 alg = SHA256.Create();

                byte[] data = File.ReadAllBytes(Program.documentFolder + "\\" + fileName + ".pdf");
                byte[] hash = alg.ComputeHash(data);

                if (rsaDeformatter.VerifySignature(hash, signedHash))
                {
                    MessageBox.Show("Signature is valid");
                }
                else
                {
                    MessageBox.Show("Signature is not valid");
                }
            }
        }
    class IniFile   
    {
        string Path ;
        string Section = "Settings";

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile()
        {
            Path = new FileInfo("Settings.ini").FullName;
        }

        public string Read(string Key )
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, Path);
        }

       

        
        
    }
} 

