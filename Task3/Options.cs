using CommandLine;

namespace Task3
{
    public class Options
    {
        [Option('h', "help", Required = false, HelpText = "Display help information.")]
        public bool Help { get; set; }

        [Value(0, MetaName = "dice", HelpText = "List of dice sides separated by commas.")]
        public IEnumerable<string> Dice { get; set; }
    }
}