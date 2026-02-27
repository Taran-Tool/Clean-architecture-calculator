using System;

namespace Calculator.Domain.Services
{
    /// <summary>
    /// Интерфейс сервиса калькулятора
    /// Определяет ЧТО можно делать, но не КАК
    /// </summary>
    public interface ICalculatorService
    {
        /// <summary>
        /// Сложение двух чисел
        /// </summary>
        double Add(double a, double b);

        /// <summary>
        /// Вычитание двух чисел
        /// </summary>
        double Subtract(double a, double b);

        /// <summary>
        /// Умножение двух чисел
        /// </summary>
        double Multiply(double a, double b);

        /// <summary>
        /// Деление двух чисел
        /// </summary>
        /// <exception cref="DivideByZeroException">Если b == 0</exception>
        double Divide(double a, double b);
    }
}

