
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace TokaInfrestructura.Halper
{
    public class HashHelper
    {
        public static HashPassword Hash(string password)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 258 / 8));

            return new HashPassword() { Password = hashed, Salt = Convert.ToBase64String(salt) };
        }
        public static bool CheckHash(string attempPassoword ,string hash ,string salt )
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                      password: attempPassoword,
                      salt:Convert.FromBase64String(salt),
                      prf: KeyDerivationPrf.HMACSHA256,
                     iterationCount: 10000,
                     numBytesRequested: 258 / 8));
            return hash == hashed;
        }
        public static  byte[] GetHash(string password, string salt)
        {
            byte[] unhashBytes = Encoding.Unicode.GetBytes(string.Concat(salt, password));
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashBytes = sha256.ComputeHash(unhashBytes);
            return hashBytes;
        }
    }
    public class HashPassword
    {
        public string Password { get; set; } 
         public string Salt { get; set; }
    }
}
