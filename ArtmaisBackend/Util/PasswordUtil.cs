using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ArtmaisBackend.Util
{
    public static class PasswordUtil
    {
        public static string Encrypt(string password, string userSalt = "")
        {
            SHA512 shaM = new SHA512Managed();

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var convertedSalt = userSalt != "" ? userSalt : Convert.ToBase64String(salt);
            
            var hash = shaM.ComputeHash(Encoding.UTF8.GetBytes($"{convertedSalt}{password}"));
            var convertedHash = Convert.ToBase64String(hash);

            return $"{convertedSalt}{convertedHash}";
        }
    }
}
