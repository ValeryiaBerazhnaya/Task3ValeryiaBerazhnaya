using System;
using System.Security.Cryptography;

namespace Task3
{
    public class FairRandomGenerator
    {
        public static int GenerateRandomNumber(int max)
        {
            return RandomNumberGenerator.GetInt32(max);
        }

        public static string GenerateRandomKey()
        {
            byte[] key = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            return Convert.ToHexString(key).ToLower();
        }
    }
}
