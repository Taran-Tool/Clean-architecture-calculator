using System;
using Calculator.Domain.Services;

namespace Calculator.Infrastructure.Services
{
    public class BasicCalculatorService : ICalculatorService
    {
        public double Add(double a, double b)
        {
            var result = a + b;
            Console.WriteLine($"[Calculator] {a} + {b} = {result}");
            return result;
        }

        public double Subtract(double a, double b)
        {
            var result = a - b;
            Console.WriteLine($"[Calculator] {a} - {b} = {result}");
            return result;
        }

        public double Multiply(double a, double b)
        {
            var result = a * b;
            Console.WriteLine($"[Calculator] {a} * {b} = {result}");
            return result;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Деление на ноль невозможно");

            var result = a / b;
            Console.WriteLine($"[Calculator] {a} / {b} = {result}");
            return result;
        }
    }
}

