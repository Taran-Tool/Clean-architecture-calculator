using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.Domain.Entities;

namespace Calculator.Domain.Repositories
{
    public interface ICalculationRepository
    {
        /// <summary>
        /// Сохранить вычисление
        /// </summary>
        Task Save(Calculation calculation);

        /// <summary>
        /// Получить все вычисления
        /// </summary>
        Task<IEnumerable<Calculation>> GetAll();

        /// <summary>
        /// Получить последние N вычислений
        /// </summary>
        Task<IEnumerable<Calculation>> GetRecent(int count);

        /// <summary>
        /// Очистить всю историю
        /// </summary>
        Task Clear();
    }
}

