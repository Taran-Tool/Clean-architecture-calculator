using Calculator.Application.UseCases;
using Calculator.Domain.Events;
using Calculator.Domain.Repositories;
using Calculator.Domain.Services;
using Calculator.Infrastructure;
using Calculator.Infrastructure.Persistence;
using Calculator.Infrastructure.Services;
using Calculator.Presentation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Calculator.Root
{
    /// <summary>
    /// Composition Root - единственное место, где знают о всех реализациях
    /// VContainer автоматически создает этот объект при запуске сцены
    /// </summary>
    public class GameLifetimeScope:LifetimeScope
    {
        [SerializeField] private CalculatorView calculatorView; // Ссылка на View в сцене
        [SerializeField] private CalculatorPresenter calculatorPresenter;

        protected override void Configure(IContainerBuilder builder)
        {
            // ===== DOMAIN INTERFACES (регистрируем реализации) =====

            // EventBus - синглтон (один на все приложение)
            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);

            // Calculator Service - синглтон
            builder.Register<ICalculatorService, BasicCalculatorService>(Lifetime.Singleton);

            // Repository - синглтон (в реальности может быть Scoped)
            builder.Register<ICalculationRepository, InMemoryCalculationRepository>(Lifetime.Singleton);

            // ===== APPLICATION USE CASES (transient - каждый раз новый) =====
            builder.Register<CalculateUseCase>(Lifetime.Transient);
            builder.Register<GetHistoryUseCase>(Lifetime.Transient);

            // ===== PRESENTATION =====

            // Регистрируем презентер как компонент (он уже есть на сцене)
            builder.RegisterComponent(calculatorView);
            builder.RegisterComponent(calculatorPresenter);// Размещаем под объектом Scope
        }
    }
}

