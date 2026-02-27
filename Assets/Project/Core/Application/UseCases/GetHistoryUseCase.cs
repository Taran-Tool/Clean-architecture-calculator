using System.Collections.Generic;
using System.Threading.Tasks;
using Calculator.Domain.Entities;
using Calculator.Domain.Repositories;

namespace Calculator.Application.UseCases
{
    /// <summary>
    /// Use Case: Получить историю вычислений
    /// </summary>
    public class GetHistoryUseCase
    {
        private readonly ICalculationRepository _repository;

        public GetHistoryUseCase(ICalculationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получить всю историю
        /// </summary>
        public async Task<IEnumerable<Calculation>> Execute()
        {
            return await _repository.GetAll();
        }

        /// <summary>
        /// Получить последние N вычислений
        /// </summary>
        public async Task<IEnumerable<Calculation>> Execute(int count)
        {
            return await _repository.GetRecent(count);
        }
    }
}

