using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Vehicle_Assembly.Utilities.AccountUtility.AdminAccount
{
    public class AdminAccountUtility : IAdminAccountUtility
    {
        public byte[] PasswordHash;
        public string generatedPassword;

        public AdminAccountUtility()
        {
            SetRandomPassword();
        }
        private string SetRandomPassword()
        {
            generatedPassword = GenerateRandomPassword(8);
            using (var sha256 = SHA256.Create())
            {
                PasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(generatedPassword));
            }
            return generatedPassword;
        }
        public bool VerifyPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return StructuralComparisons.StructuralEqualityComparer.Equals(PasswordHash, hashedPassword);
            }
        }
        private static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
