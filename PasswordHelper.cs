using System;
using System.Security.Cryptography;
using System.Text;

namespace Ettermek
{
    public static class PasswordHelper
    {
        // Só generálása (biztonságos random bájtok)
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        // Jelszó hash-elése a sóval (PBKDF2 algoritmus)
        public static string HashPassword(string password, string salt)
        {
            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(32)); // 256-bit hash
            }
        }

        // Ellenőrzés: a beírt jelszó hash-e megegyezik-e a tárolttal?
        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string newHash = HashPassword(enteredPassword, storedSalt);
            return newHash == storedHash;
        }
    }
}
