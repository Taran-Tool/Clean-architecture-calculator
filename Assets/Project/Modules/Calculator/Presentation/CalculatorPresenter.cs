using UnityEngine;
using System;
using VContainer;
using Calculator.Application.UseCases;
using Calculator.Domain.Events;

namespace Calculator.Presentation
{
    /// <summary>
    /// Презентер калькулятора
    /// Связывает чистую логику с Unity View
    /// </summary>
    public class CalculatorPresenter:MonoBehaviour
    {
        [SerializeField] private CalculatorView view;

        // Зависимости, внедряемые через VContainer
        private CalculateUseCase _calculateUseCase;
        private GetHistoryUseCase _getHistoryUseCase;
        private IEventBus _eventBus;

        /// <summary>
        /// Конструктор с зависимостями (вызывается VContainer)
        /// </summary>
        [Inject]
        public void Construct(
            CalculateUseCase calculateUseCase,
            GetHistoryUseCase getHistoryUseCase,
            IEventBus eventBus)
        {
            _calculateUseCase = calculateUseCase;
            _getHistoryUseCase = getHistoryUseCase;
            _eventBus = eventBus;
        }

        private void Start()
        {
            // Подписка на события домена
            _eventBus.Subscribe<CalculationPerformedEvent>(OnCalculationPerformed);

            // Загрузка истории при старте
            LoadHistory();
        }

        /// <summary>
        /// Вызывается из View при нажатии кнопки
        /// </summary>
        public async void OnCalculateButtonClicked(string operation)
        {
            if (!view.TryGetOperands(out double a, out double b))
            {
                view.ShowError("Введите оба числа");
                return;
            }

            if (!_calculateUseCase.IsValidOperation(operation))
            {
                view.ShowError("Неизвестная операция");
                return;
            }

            try
            {
                // Вызов Use Case
                var result = await _calculateUseCase.Execute(a, b, operation);
                view.SetResult(result);
                view.ClearInputs(); // Очистить для нового ввода
            }
            catch (System.DivideByZeroException)
            {
                view.ShowError("Деление на ноль!");
            }
            catch (System.Exception ex)
            {
                view.ShowError($"Ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Загрузить историю вычислений
        /// </summary>
        private async void LoadHistory()
        {
            var history = await _getHistoryUseCase.Execute(10);
            view.UpdateHistory(history);
        }

        /// <summary>
        /// Обработчик события вычисления
        /// </summary>
        private void OnCalculationPerformed(CalculationPerformedEvent evt)
        {
            // Автоматически обновляем историю при новом вычислении
            LoadHistory();
        }

        private void OnDestroy()
        {
            _eventBus?.Unsubscribe<CalculationPerformedEvent>(OnCalculationPerformed);
        }
    }
}
