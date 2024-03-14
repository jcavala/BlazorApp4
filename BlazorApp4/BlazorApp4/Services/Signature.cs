using System.Security.Cryptography;
using System.Text;

namespace BlazorApp4.Services
{
    public class Signature
    {

        public static void signPdf(string file,RSA rsa)
        {

            using SHA256 alg = SHA256.Create();
            byte[] hash = alg.ComputeHash(new FileStream(file + ".pdf", FileMode.Open));

            byte[] signedHash;

            RSAPKCS1SignatureFormatter rsaFormatter = new(rsa);
            rsaFormatter.SetHashAlgorithm(nameof(SHA256));

            signedHash = rsaFormatter.CreateSignature(hash);  
            string signature = file + "_signed.bin";
            File.WriteAllBytes(signature, signedHash);
            
        }
    }
}
