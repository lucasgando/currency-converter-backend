using System.Security.Cryptography;
using System.Text;

namespace currency_converter.Helpers
{
    public static class PasswordHasher
    {
        public static string GetPasswordHash(string password)
        {
            StringBuilder hashedPassword = new StringBuilder();
            byte[] byteHash = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            foreach (byte theByte in byteHash)
                hashedPassword.Append(theByte.ToString("x2"));
            return hashedPassword.ToString();
        }
    }
}
