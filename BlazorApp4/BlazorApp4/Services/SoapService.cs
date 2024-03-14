using BlazorApp4.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace SoapService
{

    [ServiceContract]
    public interface ISoapService
    {
        [OperationContract]
        byte[] GetSymmetricKey(string clientPublicKey, string userName);

        [OperationContract]
        string GetServerPublicKey();
    }

    public class MySoapService : ISoapService
    {
        
        ApplicationDbContext context;
       ICryptoService cryptoService;

        public MySoapService(ApplicationDbContext context, [FromServices]  ICryptoService _cryptoService)
        {
            this.context = context;
            cryptoService = _cryptoService;
        }

        public byte[] GetSymmetricKey(string clientPublicKey, string userName)
        {
            var rsa =RSA.Create();
            rsa.FromXmlString(clientPublicKey);
            Aes aes = Aes.Create(); 
            ApplicationUser user = context.Users.First(x=>x.UserName.Equals(userName));
            if(user.aesKey == null || user.aesKey.Length==0) {
                user.aesKey = aes.Key;
                user.iv = aes.IV;
                context.Users.Update(user);
                context.SaveChanges();
            }
            else { aes.Key = user.aesKey; aes.IV = user.iv; }
            var bytes = cryptoService.exportKey(aes);
            return rsa.Encrypt(bytes, RSAEncryptionPadding.Pkcs1);
        }
            
        public string GetServerPublicKey()
        {
            return cryptoService.getRsa().ToXmlString(false);
        }


    }
}
