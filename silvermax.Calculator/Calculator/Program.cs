using System.Text.RegularExpressions;
using CalculatorLibrary;
class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        bool endApp = false;
        int CalculatorUsedTimes = 0;
        List<string> latestQuestions = new();
        double previousResult = 0;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        while (!endApp)
        {
            Console.WriteLine(@"What Calculation would you like to perform?
                                1. Four-function Arithmetic
                                2. SquareRoot
                                3. Power Calculation
                                4. Trigonometry");

            var readResult = Console.ReadLine();
            switch (readResult)
            {
                case "1":

                    // Declare variables and set to empty.
                    // Use Nullable types (with ?) to match type of System.Console.ReadLine
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;

                    if (previousResult != 0)
                    {
                        // Ask the user to type the first number.
                        Console.WriteLine("Would you like to use the previous answer?Type y or n: ");
                        string? response = Console.ReadLine();
                        if (response == "y")
                        {
                            numInput1 = Convert.ToString(previousResult);
                        }
                    }
                    else
                    {
                        Console.Write("Type a number, and then press Enter: ");
                        numInput1 = Console.ReadLine();
                    }

                    double cleanNum1 = 0;
                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }

                    // Ask the user to type the second number.
                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }

                    // Ask the user to choose an operator.
                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.Write("Your option? ");

                    string? op = Console.ReadLine();

                    // Validate input is not null, and matches the pattern
                    if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
                    {
                        Console.WriteLine("Error: Unrecognized input.");
                    }
                    else
                    {
                        try
                        {
                            result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                            if (double.IsNaN(result))
                            {
                                Console.WriteLine("This operation will result in a mathematical error.\n");
                            }
                            else
                            {
                                Console.WriteLine("Your result: {0:0.##}\n", result);
                                latestQuestions.Add($"{cleanNum1} {op} {cleanNum2} = {result}");
                                CalculatorUsedTimes++;
                                previousResult = result;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    break;

                case "2":
                    Console.WriteLine("Input the number: ");
                    readResult = Console.ReadLine();

                    double num = 0;

                    while (!double.TryParse(readResult, out num))
                    {
                        Console.WriteLine("Invalid input please input a valid number");
                        readResult = Console.ReadLine();
                    }

                    result = calculator.SquareRoot(num);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    latestQuestions.Add($"The square root of {num} is {result}");
                    CalculatorUsedTimes++;
                    previousResult = result;

                    break;

                case "3":
                    Console.WriteLine("Input the number: ");
                    readResult = Console.ReadLine();
                    double num1 = 0;

                    while (!double.TryParse(readResult, out num1))
                    {
                        Console.WriteLine("Invalid input please input a valid number");
                        readResult = Console.ReadLine();
                    }

                    Console.WriteLine("input a number to raise to: ");
                    readResult = Console.ReadLine();
                    double power = 0;

                    while (!double.TryParse(readResult, out power))
                    {
                        Console.WriteLine("Invalid input please input a valid number");
                        readResult = Console.ReadLine();
                    }

                    result = calculator.PowerCalculate(num1, power);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    latestQuestions.Add($"The power of {num1} to {power} is {result}");
                    CalculatorUsedTimes++;
                    previousResult = result;

                    break;
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write("Press 'n' and Enter to close the app or press c to clear questions history, or press any other key and Enter to continue: ");
            if (Console.ReadLine() == "n")
            {
                endApp = true;
            }
            else if (Console.ReadLine() == "c")
            {
                latestQuestions.Clear();
            }

            Console.WriteLine("\n");
        }
        calculator.Finish();
        return;
    }
}