using System;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    public class HMACGenerator
    {
        public static string GenerateHMAC(string key, string message)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}