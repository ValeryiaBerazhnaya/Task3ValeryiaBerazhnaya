namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Task3.exe <dice1> <dice2> [<dice3> ...]");
                Console.WriteLine("Example: Task3.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
                return;
            }

            try
            {
                Dice[] dices = DiceParser.Parse(args);
                GameTable gameTable = new GameTable(dices);
                gameTable.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}