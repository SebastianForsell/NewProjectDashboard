using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Helpers;

namespace MainSite.Models
{
    public class PasswordManager
    {
        public byte[] HashPassword(byte[] password)
        {
            using (SHA256 sha = new SHA256Managed())
            {
                byte[] saltedBytes = CreateSalt();
                //byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = new byte[password.Length + saltedBytes.Length];
                for (int i = 0; i < password.Length; i++)
                {
                    combinedBytes[i] = password[i];
                }
                for (int i = 0; i < saltedBytes.Length; i++)
                {
                    combinedBytes[password.Length + i] = saltedBytes[i];
                }
                //string stringedHash = System.Text.Encoding.UTF8.GetString(hashedPassword);
                return sha.ComputeHash(combinedBytes);
            }
        }
        static byte[] CreateSalt()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] buff = new byte[64];
            random.GetBytes(buff);
            return buff;
        }
        public void ComparePassword()
        {
            Queries query = new Queries();
            
            //Crypto.VerifyHashedPassword(stringedHash, password);
            //string stringedHash = System.Text.Encoding.UTF8.GetString();
        }
    }
}