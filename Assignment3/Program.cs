using System;
namespace Assignment3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first number (A):");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter second number (B):");
            double b = Convert.ToDouble(Console.ReadLine());

            char op;
            while (true)
            {
                Console.WriteLine("Enter operation (+, -, *, /):");
                op = Convert.ToChar(Console.ReadLine());

                // Validate operation before creating the calculator
                if (op == '+' || op == '-' || op == '*' || op == '/')
                    break;
                else
                    Console.WriteLine("Invalid operation! Please try again.");
            }

            try
            {
                Calculator calculator = new Calculator(a, b, op);
                double result = calculator.Calculate();
                Console.WriteLine($"Result: {a} {op} {b} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}