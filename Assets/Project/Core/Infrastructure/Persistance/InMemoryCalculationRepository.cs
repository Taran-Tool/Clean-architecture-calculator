using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculator.Domain.Entities;
using Calculator.Domain.Repositories;

namespace Calculator.Infrastructure.Persistence
{
    public class InMemoryCalculationRepository : ICalculationRepository
    {
        // "База данных" в памяти
        private readonly List<Calculation> _calculations = new();

        public Task Save(Calculation calculation)
        {
            // Имитация асинхронной операции
            return Task.Run(() =>
            {
                _calculations.Add(calculation);
                Console.WriteLine($"[Repository] Сохранено: {calculation.GetFormattedExpression()}");
            });
        }

        public Task<IEnumerable<Calculation>> GetAll()
        {
            return Task.FromResult<IEnumerable<Calculation>>(
                _calculations.OrderByDescending(c => c.Timestamp).ToList()
            );
        }

        public Task<IEnumerable<Calculation>> GetRecent(int count)
        {
            return Task.FromResult<IEnumerable<Calculation>>(
                _calculations.OrderByDescending(c => c.Timestamp).Take(count).ToList()
            );
        }

        public Task Clear()
        {
            return Task.Run(() => _calculations.Clear());
        }
    }
}

