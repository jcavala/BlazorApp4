using BlazorApp4.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace BlazorApp4.Services
{
    public class CustomPasswordHasher : IPasswordHasher<ApplicationUser>
    {
        byte[][] peppers;
        Random rnd;


        public CustomPasswordHasher()
        {
            var x = Encoding.UTF8.GetBytes("56701913982852056888719231535878");
            var y = Encoding.UTF8.GetBytes("72512459405958456599950110627398");
            var z = Encoding.UTF8.GetBytes("21437177558103692482916698583229");
            rnd = new Random();
            
            peppers = new byte[][] { x, y, z };

        }
        
        public string HashPassword(ApplicationUser user, string password)
        {
            byte[] buffer = new byte[128];
            using SHA256 alg = SHA256.Create();
            byte[] salt = alg.ComputeHash(Encoding.UTF8.GetBytes(user.UserName));
            
            int randInt = rnd.Next(0, 2);
            Array.Copy(salt,buffer, 32);
            Array.Copy(peppers[randInt], 0, buffer, 32, 32);
            var passBytes = Encoding.UTF8.GetBytes(password);
            Array.Copy(passBytes, 0, buffer, 64, passBytes.Length);
            
            var hashBytes = alg.ComputeHash(buffer);
            string hashed = Convert.ToBase64String(hashBytes);
            return hashed;
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {

            string hashedProvidedPassword;
            using SHA256 alg = SHA256.Create();
            byte[] salt = alg.ComputeHash(Encoding.UTF8.GetBytes(user.UserName));
            for(int i = 0;i<3;i++)
            {
                byte[] buffer = new byte[128];
                Array.Copy(salt, buffer, 32);
                Array.Copy(peppers[i], 0, buffer, 32, 32);
                var passBytes = Encoding.UTF8.GetBytes(providedPassword);
                Array.Copy(passBytes, 0, buffer, 64, passBytes.Length);
                var hashBytes = alg.ComputeHash(buffer);
                hashedProvidedPassword = Convert.ToBase64String(hashBytes);

                if (hashedPassword == hashedProvidedPassword)
                {
                    return PasswordVerificationResult.Success;
                }
            }

            return PasswordVerificationResult.Failed;
        }

       
    }
}
