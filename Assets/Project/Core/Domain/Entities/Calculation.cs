using System;

namespace Calculator.Domain.Entities
{
    /// <summary>
    /// Сущность "Вычисление" - представляет одну операцию калькулятора
    /// </summary>
    public class Calculation
    {
        // Свойства только для чтения - неизменяемость после создания
        public string Id
        {
            get;
        }
        public double Operand1
        {
            get;
        }
        public double Operand2
        {
            get;
        }
        public string Operation
        {
            get;
        } // "+", "-", "*", "/"
        public double Result
        {
            get;
        }
        public DateTime Timestamp
        {
            get;
        }

        /// <summary>
        /// Конструктор - единственное место, где задаются значения
        /// </summary>
        public Calculation(double operand1, double operand2, string operation, double result)
        {
            Id = Guid.NewGuid().ToString(); // Уникальный идентификатор
            Operand1 = operand1;
            Operand2 = operand2;
            Operation = operation;
            Result = result;
            Timestamp = DateTime.UtcNow; // Всегда UTC для консистентности            
        }
        /// <summary>
        /// Форматированное представление для отображения
        /// </summary>
        /// <returns></returns>
        public string GetFormattedExpression()
        {
            return $"{Operand1} {Operation} {Operand2} = {Result}";
        }
    }
}

