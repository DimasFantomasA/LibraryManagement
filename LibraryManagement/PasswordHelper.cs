
using System;
using System.Security.Cryptography;

namespace LibraryManagement
{
    internal static class PasswordHelper
    {
        public const int Iterations = 600000;

        public static byte[] GenerateSalt()
        {
            return RandomNumberGenerator.GetBytes(16);
        }

        public static byte[] HashPassword(
            string password,
            byte[] salt,
            int iterations = Iterations)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                32);
        }

        public static bool VerifyPassword(
            string password,
            byte[] salt,
            byte[] storedHash,
            int iterations)
        {
            byte[] calculatedHash = HashPassword(
                password,
                salt,
                iterations);

            return CryptographicOperations.FixedTimeEquals(
                calculatedHash,
                storedHash);
        }
    }
}
