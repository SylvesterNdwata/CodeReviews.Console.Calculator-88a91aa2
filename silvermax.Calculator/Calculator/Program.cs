using System.Text.RegularExpressions;
using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator();
        bool endApp = false;
        int calculatorUsedTimes = 0;
        List<string> latestQuestions = new();
        double previousResult = 0;

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
                    string? numInput1 = "";
                    string? numInput2 = "";
                    double result = 0;

                    if (previousResult != 0)
                    {
                        Console.WriteLine("Would you like to use the previous answer? Type y or n: ");
                        string? response = Console.ReadLine()?.Trim().ToLower();
                        if (response == "y")
                        {
                            numInput1 = Convert.ToString(previousResult);
                        }
                        else
                        {
                            Console.WriteLine("Type a number, and then press Enter: ");
                            numInput1 = Console.ReadLine();
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

                    Console.Write("Type another number, and then press Enter: ");
                    numInput2 = Console.ReadLine();

                    double cleanNum2 = 0;
                    while (!double.TryParse(numInput2, out cleanNum2))
                    {
                        Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput2 = Console.ReadLine();
                    }

                    Console.WriteLine("Choose an operator from the following list:");
                    Console.WriteLine("\ta - Add");
                    Console.WriteLine("\ts - Subtract");
                    Console.WriteLine("\tm - Multiply");
                    Console.WriteLine("\td - Divide");
                    Console.Write("Your option? ");

                    string? op = Console.ReadLine().Trim().ToLower();

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
                                switch (op) 
                                {
                                    case "a":
                                        latestQuestions.Add($"{cleanNum1} + {cleanNum2} = {result}");
                                        break;

                                    case "s":
                                        latestQuestions.Add($"{cleanNum1} - {cleanNum2} = {result}");
                                        break;

                                    case "m":
                                        latestQuestions.Add($"{cleanNum1} * {cleanNum2} = {result}");
                                        break;

                                    case "d":
                                        latestQuestions.Add($"{cleanNum1} / {cleanNum2} = {result}");
                                        break;
                                }
                                calculatorUsedTimes++;
                                previousResult = result;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                        }
                    }
                    PrintQuestions(latestQuestions);
                    Console.WriteLine($"The calculator has been used {calculatorUsedTimes} times");
                    break;

                case "2":
                    Console.WriteLine("Input the number: ");
                    var squareResponse = Console.ReadLine();
                    double num = 0;

                    while (!double.TryParse(squareResponse, out num))
                    {
                        Console.WriteLine("Invalid input please input a valid number");
                        squareResponse = Console.ReadLine();
                    }

                    result = calculator.SquareRoot(num);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    latestQuestions.Add($"The square root of {num} is {result}");
                    calculatorUsedTimes++;
                    previousResult = result;
                    PrintQuestions(latestQuestions);
                    Console.WriteLine($"The calculator has been used {calculatorUsedTimes} times");
                    break;

                case "3":
                    Console.WriteLine("Input the number: ");
                    var reply = Console.ReadLine();
                    double num1 = 0;

                    while (!double.TryParse(reply, out num1))
                    {
                        Console.WriteLine("Invalid input please input a valid number");
                        reply = Console.ReadLine();
                    }

                    Console.WriteLine("Input a number to raise to: ");
                    var powerResponse = Console.ReadLine();
                    double power = 0;

                    while (!double.TryParse(powerResponse, out power))
                    {
                        Console.WriteLine("Invalid input please input a valid number");
                        powerResponse = Console.ReadLine();
                    }

                    result = calculator.PowerCalculate(num1, power);
                    Console.WriteLine("Your result: {0:0.##}\n", result);
                    latestQuestions.Add($"The power of {num1} to {power} is {result}");
                    calculatorUsedTimes++;
                    previousResult = result;
                    PrintQuestions(latestQuestions);
                    Console.WriteLine($"The calculator has been used {calculatorUsedTimes} times");
                    break;


                case "4":
                    Console.WriteLine("Choose a trigonometric function:");
                    Console.WriteLine("\tsin - Sine");
                    Console.WriteLine("\tcos - Cosine");
                    Console.WriteLine("\ttan - Tangent");
                    Console.Write("Your option? ");

                    string? trigFunc = Console.ReadLine()?.Trim().ToLower();
                    
                    if (trigFunc == null || (trigFunc != "sin" && trigFunc != "cos" && trigFunc != "tan"))
                    {
                        Console.WriteLine("Error: Unrecognized trigonometric function.");
                        break;
                    }

                    Console.Write("Enter the angle in degrees: ");
                    var angleInput = Console.ReadLine();
                    double angle = 0;

                    while (!double.TryParse(angleInput, out angle))
                    {
                        Console.WriteLine("Invalid input, please enter a valid number: ");
                        angleInput = Console.ReadLine();
                    }

                    double trigResult = calculator.Trigonometry(trigFunc, angle);
                    Console.WriteLine("Your result: {0:0.##}\n", trigResult);
                    latestQuestions.Add($"{trigFunc}({angle}°) = {trigResult}");
                    calculatorUsedTimes++;
                    previousResult = trigResult;
                    PrintQuestions(latestQuestions);
                    Console.WriteLine($"The calculator has been used {calculatorUsedTimes} times");
                    break;

                default:
                    Console.WriteLine("Error: Unrecognized option.");
                    break;
            } // closes switch

            Console.WriteLine("------------------------\n");

            Console.Write("Press 'n' and Enter to close the app, 'c' to clear question history, or any other key to continue: ");
            var endInput = Console.ReadLine();

            if (endInput == "n")
            {
                endApp = true;
            }
            else if (endInput == "c")
            {
                latestQuestions.Clear();
            }

            Console.WriteLine("\n");
            Console.Clear();
        }

        calculator.Finish();
    }

    static void PrintQuestions(List<string> list)
    {
        Console.WriteLine("Questions History: \n");
        foreach (var qn in list)
        {
            Console.WriteLine(qn);
        }
        Console.WriteLine();
    }
}

