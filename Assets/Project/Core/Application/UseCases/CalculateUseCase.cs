using Calculator.Domain.Entities;
using Calculator.Domain.Events;
using Calculator.Domain.Repositories;
using Calculator.Domain.Services;
using System;
using System.Threading.Tasks;

namespace Calculator.Application.UseCases
{
    /// <summary>
    /// Use Case: Выполнить вычисление
    /// </summary>
    public class CalculateUseCase
    {
        // Зависимости приходят через конструктор (Dependency Injection)
        private readonly ICalculatorService _calculator;
        private readonly ICalculationRepository _repository;
        private readonly IEventBus _eventBus;

        public CalculateUseCase(
            ICalculatorService calculator,
            ICalculationRepository repository,
            IEventBus eventBus)
        {
            _calculator = calculator;
            _repository = repository;
            _eventBus = eventBus;
        }

        /// <summary>
        /// Выполнить вычисление
        /// </summary>
        /// <returns>Результат вычисления</returns>
        public async Task<double> Execute(double a, double b, string operation)
        {
            // 1. Бизнес-логика (через доменный сервис)
            double result = operation switch
            {
                "+" => _calculator.Add(a, b),
                "-" => _calculator.Subtract(a, b),
                "*" => _calculator.Multiply(a, b),
                "/" => _calculator.Divide(a, b),
                _ => throw new ArgumentException($"Неизвестная операция: {operation}")
            };

            // 2. Создание сущности (чистое доменное создание)
            var calculation = new Calculation(a, b, operation, result);

            // 3. Сохранение (через репозиторий)
            await _repository.Save(calculation);

            // 4. Оповещение всего мира о событии (через EventBus)
            _eventBus.Publish(new CalculationPerformedEvent(calculation));

            return result;
        }

        /// <summary>
        /// Проверить, валидна ли операция
        /// </summary>
        public bool IsValidOperation(string operation)
        {
            return operation is "+" or "-" or "*" or "/";
        }
    }
}

