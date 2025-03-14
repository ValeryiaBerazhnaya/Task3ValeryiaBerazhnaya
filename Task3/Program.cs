using CommandLine;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("--help") || args.Length == 0)
            {
                Console.WriteLine("Help information:");
                Console.WriteLine("Usage: Task3.exe <dice1> <dice2> [<dice3> ...]");
                Console.WriteLine("Example: Task3.exe 2,2,4,4,9,9 6,8,1,1,8,6 7,5,3,7,5,3");
                return;
            }

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    Dice[] dices = DiceParser.Parse(options.Dice.ToArray());
                    GameTable gameTable = new GameTable(dices);
                    gameTable.Play();
                })
                .WithNotParsed(errors =>
                {
                    Console.WriteLine("Invalid input. Use --help for more information.");
                });
        }
    }
}