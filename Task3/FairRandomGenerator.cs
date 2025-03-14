namespace Task3
{
    public class FairRandomGenerator
    {
        private static readonly Random random = new Random();

        public static int GenerateRandomNumber(int max)
        {
            return random.Next(max);
        }

        public static string GenerateRandomKey()
        {
            var bytes = new byte[32];
            random.NextBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "");
        }
    }
}