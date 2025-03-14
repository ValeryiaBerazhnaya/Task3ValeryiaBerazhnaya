using Task3;

public class GameTable
{
    private Dice[] dices;
    private Dice selectedUserDice;
    private Dice selectedComputerDice;
    private bool userFirstMove;

    public GameTable(Dice[] dices)
    {
        this.dices = dices;
    }

    public void DetermineFirstMove()
    {
        string key = FairRandomGenerator.GenerateRandomKey();
        int randomValue = FairRandomGenerator.GenerateRandomNumber(2);
        string hmac = HMACGenerator.GenerateHMAC(key, randomValue.ToString());

        Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac}).");
        Console.WriteLine("Try to guess my selection.");
        int userGuess = GetUserInput("Enter your guess (0 or 1):", 1);

        string actualKey = HMACGenerator.GenerateHMAC(key, randomValue.ToString());
        Console.WriteLine($"My selection: {randomValue} (KEY={actualKey}).");

        userFirstMove = userGuess == randomValue;
        if (userFirstMove)
        {
            Console.WriteLine("You make the first move.");
        }
        else
        {
            Console.WriteLine("I make the first move.");
            selectedComputerDice = dices[FairRandomGenerator.GenerateRandomNumber(dices.Length)];
            Console.WriteLine($"I choose the [{string.Join(",", selectedComputerDice.Sides)}] dice.");
        }
    }

    public void Play()
    {
        DetermineFirstMove();
        ShowProbabilities();

        if (userFirstMove)
        {
            UserSelectsDice();
        }
        else
        {
            ComputerSelectsDice();
        }

        if (selectedUserDice != null && selectedComputerDice != null)
        {
            PlayRound();
        }
    }

    private void ShowProbabilities()
    {
        var probabilities = ProbabilityCalculator.CalculateWinProbabilities(dices);

        Console.WriteLine("Win probabilities for each dice:");
        for (int i = 0; i < dices.Length; i++)
        {
            Console.WriteLine($"Dice {i} [{string.Join(",", dices[i].Sides)}]: {probabilities[dices[i]]:P2}");
        }
    }

    private void UserSelectsDice()
    {
        Console.WriteLine("Choose your dice:");
        for (int i = 0; i < dices.Length; i++)
        {
            Console.WriteLine($"{i} - {string.Join(",", dices[i].Sides)}");
        }

        int userChoice = GetUserInput("Enter your choice:", dices.Length - 1);
        selectedUserDice = dices[userChoice];
        Console.WriteLine($"You choose the [{string.Join(",", selectedUserDice.Sides)}] dice.");

        selectedComputerDice = dices.First(d => d != selectedUserDice);
        Console.WriteLine($"I choose the [{string.Join(",", selectedComputerDice.Sides)}] dice.");
    }

    private void ComputerSelectsDice()
    {
        Console.WriteLine("Choose your dice:");
        for (int i = 0; i < dices.Length; i++)
        {
            if (dices[i] != selectedComputerDice)
            {
                Console.WriteLine($"{i} - {string.Join(",", dices[i].Sides)}");
            }
        }

        int userChoice = GetUserInput("Enter your choice:", dices.Length - 1);
        selectedUserDice = dices[userChoice];
        Console.WriteLine($"You choose the [{string.Join(",", selectedUserDice.Sides)}] dice.");
    }

    private int GetUserInput(string prompt, int maxValue)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            for (int i = 0; i <= maxValue; i++)
            {
                Console.WriteLine($"{i} - {i}");
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            string? userInput = Console.ReadLine()?.Trim().ToLower();

            if (userInput == "x")
            {
                Console.WriteLine("Exiting the game...");
                Environment.Exit(0);
            }
            else if (userInput == "?")
            {
                Console.WriteLine($"Help: Enter a number between 0 and {maxValue} to make your choice. Enter 'X' to exit.");
                continue;
            }

            if (!int.TryParse(userInput, out int result))
            {
                Console.WriteLine("Invalid input. Please enter an integer number.");
                continue;
            }

            if (result < 0 || result > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a number between 0 and {maxValue}.");
                continue;
            }

            return result;
        }
    }

    private void PlayRound()
    {
        Console.WriteLine("It's time for my throw.");
        int computerThrow = ThrowDice(selectedComputerDice);
        Console.WriteLine($"My throw is {computerThrow}.");

        Console.WriteLine("It's time for your throw.");
        int userThrow = ThrowDice(selectedUserDice);
        Console.WriteLine($"Your throw is {userThrow}.");

        DetermineWinner(userThrow, computerThrow);
    }

    private int ThrowDice(Dice dice)
    {
        string key = FairRandomGenerator.GenerateRandomKey();
        int randomValue = FairRandomGenerator.GenerateRandomNumber(dice.Sides.Length);
        string hmac = HMACGenerator.GenerateHMAC(key, randomValue.ToString());

        Console.WriteLine($"I selected a random value in the range 0..{dice.Sides.Length - 1} (HMAC={hmac}).");
        int userNumber = GetUserInput("Add your number modulo 6:", dice.Sides.Length - 1);

        int computerNumber = randomValue;
        string actualKey = HMACGenerator.GenerateHMAC(key, computerNumber.ToString());

        Console.WriteLine($"My number is {computerNumber} (KEY={actualKey}).");

        int result = (userNumber + computerNumber) % dice.Sides.Length;
        Console.WriteLine($"The result is {userNumber} + {computerNumber} = {result} (mod {dice.Sides.Length}).");

        return dice.Sides[result];
    }

    private void DetermineWinner(int userThrow, int computerThrow)
    {
        Console.WriteLine($"Your number: {userThrow}");
        Console.WriteLine($"My number: {computerThrow}");

        if (userThrow > computerThrow)
        {
            Console.WriteLine("You win!");
        }
        else if (userThrow < computerThrow)
        {
            Console.WriteLine("I win!");
        }
        else
        {
            Console.WriteLine("It's a draw!");
        }
    }
}