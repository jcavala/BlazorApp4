using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public interface ICryptoService { 
    public RSA getRsa(); 
    public byte[] exportKey(Aes aes); 
}
public class CryptoService : ICryptoService 
{
   private  RSA rsa = RSA.Create();
   string file = "server_rsa.xml";
    
     public CryptoService() {
        if(File.Exists(file))
        {
            rsa.FromXmlString(File.ReadAllText(file)); }
        else {
            var xml = rsa.ToXmlString(true);
            var xml2 = rsa.ToXmlString(false);
            File.WriteAllText(file, xml);
            File.WriteAllText("server_publicKey.xml",xml2);
        }
        
    }
    public RSA getRsa() { return rsa; }

    public byte[] exportKey(Aes aes)
    {
        var export = new byte[16+32];
        Array.Copy(aes.Key, export,32);
        Array.Copy (aes.IV,0, export,32,16);
        File.WriteAllBytes("aes", export);
        return export;
    }


}

