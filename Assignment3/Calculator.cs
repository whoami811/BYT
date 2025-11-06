using System;

namespace Assignment3
{
    public class Calculator
    {
        private double A;
        private double B;
        private char Operation;

        public Calculator(double a, double b, char operation)
        {
            A = a;
            B = b;
            Operation = operation;
        }

        public double Calculate()
        {
            return Operation switch
            {
                '+' => A + B,
                '-' => A - B,
                '*' => A * B,
                '/' => B != 0 ? A / B : throw new DivideByZeroException("Cannot divide by zero."),
                _ => throw new InvalidOperationException("Invalid operation.")
            };
        }
    }
}