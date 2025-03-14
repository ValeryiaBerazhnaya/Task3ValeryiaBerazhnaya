using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    public class HMACGenerator
    {
        public static string GenerateHMAC(string keyHex, string message)
        {
            byte[] key = Convert.FromHexString(keyHex);

            byte[] messageBytes = Encoding.UTF8.GetBytes(message.Trim());

            using (var hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(messageBytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
