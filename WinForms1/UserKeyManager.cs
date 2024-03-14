using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WinForms1.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WinForms1
{
    public class UserKeyManager
    {
        public static List<(string user, byte[] aesKey)> userKeyPairs = new();
        private static string filePath = "userKeyPairs.bin";
        public static void Write()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                foreach (var pair in userKeyPairs)
                {
                    writer.Write(pair.user);
                    writer.Write(pair.aesKey);
                }
            }
        }

        public static void Read()
        {
          
            if(File.Exists(filePath))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        string userId = reader.ReadString();
                        byte[] aesKey = reader.ReadBytes(32 + 16);
                        userKeyPairs.Add((userId, aesKey));
                    }
                }
            }

            Get();

        }

        public static void Get()
        {
            string name = Program.user.name;
            var key_iv =userKeyPairs.Where(uk => uk.user == name).First().aesKey;
            var key = new byte[32];
            var iv = new byte[16];
            Array.Copy(key_iv, 0, key, 0, 32);
            Program.aes.Key = key;
            Array.Copy(key_iv, 32, iv, 0, 16);
            Program.aes.IV = iv;
        }
        public static void Add(string name, byte[] key)
        {
            if (!userKeyPairs.Where(uk => uk.user == name).Any())
            {
                userKeyPairs.Add((name, key));
                Write();
            }
            else
            {
                var key_iv = userKeyPairs.Where(uk => uk.user == name).First().aesKey;
                var _key = new byte[32];
                var iv = new byte[16];
                Array.Copy(key_iv, 0, _key, 0, 32);
                Program.aes.Key = _key;
                Array.Copy(key_iv, 32, iv, 0, 16);
                Program.aes.IV = iv;
            }
        }
    }
}
