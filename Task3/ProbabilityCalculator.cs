namespace Task3
{
    public class ProbabilityCalculator
    {
        public static Dictionary<Dice, double> CalculateWinProbabilities(Dice[] dices)
        {
            var probabilities = new Dictionary<Dice, double>();

            foreach (var dice in dices)
            {
                int wins = 0;
                int totalComparisons = 0;

                foreach (var otherDice in dices)
                {
                    if (dice == otherDice) continue;

                    foreach (int userSide in dice.Sides)
                    {
                        foreach (int computerSide in otherDice.Sides)
                        {
                            if (userSide > computerSide)
                            {
                                wins++;
                            }
                            totalComparisons++;
                        }
                    }
                }

                double probability = (double)wins / totalComparisons;
                probabilities[dice] = probability;
            }

            return probabilities;
        }
    }
}