namespace Task3
{
    public class DiceParser
    {
        public static Dice[] Parse(string[] diceStrings)
        {
            if (diceStrings.Length <= 2)
            {
                Console.WriteLine("Error: You must provide more than 2 dice.");
                Environment.Exit(1);
            }

            var dices = new List<Dice>();
            int expectedSidesCount = -1;

            foreach (var diceString in diceStrings)
            {
                var sides = new List<int>();
                var sideValues = diceString.Split(',');

                foreach (var side in sideValues)
                {
                    if (!int.TryParse(side, out int sideValue))
                    {
                        Console.WriteLine($"Invalid input: '{side}' is not a valid integer.");
                        Environment.Exit(1);
                    }
                    sides.Add(sideValue);
                }

                if (expectedSidesCount == -1)
                {
                    expectedSidesCount = sides.Count;
                }
                else if (sides.Count != expectedSidesCount)
                {
                    Console.WriteLine("Error: All dice must have the same number of sides.");
                    Environment.Exit(1);
                }

                dices.Add(new Dice(sides.ToArray()));
            }

            return dices.ToArray();
        }
    }
}