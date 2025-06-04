using System;
using System.Security.Cryptography;
using System.Text;

namespace QLKHO.Services
{
    public class HashingServices
    {
        public static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            using (SHA256 hash = SHA256.Create())
            {
                var passHash = hash.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in passHash)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool VerifyPassword(string plainText, string hashedFromDb)
        {
            if (string.IsNullOrWhiteSpace(hashedFromDb)) return false;

            string hashOfInput = HashPassword(plainText);
            // So sánh không phân biệt hoa-thường, dùng time-constant compare nếu cần
            return string.Equals(hashOfInput, hashedFromDb, StringComparison.OrdinalIgnoreCase);
        }
    }
}
