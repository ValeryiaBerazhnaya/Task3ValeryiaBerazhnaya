using System;
using System.Security.Cryptography;

namespace Task3
{
    public class FairRandomGenerator
    {
        public static int GenerateRandomNumber(int max)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomNumber = new byte[4];
                rng.GetBytes(randomNumber);
                int value = BitConverter.ToInt32(randomNumber, 0);
                return Math.Abs(value % max);
            }
        }

        public static string GenerateRandomKey()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] key = new byte[32];
                rng.GetBytes(key);
                return BitConverter.ToString(key).Replace("-", "");
            }
        }
    }
}