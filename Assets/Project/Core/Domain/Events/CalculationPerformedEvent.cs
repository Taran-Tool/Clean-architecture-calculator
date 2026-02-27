using System;
using Calculator.Domain.Entities;

namespace Calculator.Domain.Events
{
    /// <summary>
    /// Событие - произошло вычисление
    /// Все события должны реализовывать пустой интерфейс IEvent для типобезопасности
    /// </summary>
    public interface IEvent
    {

    }
    public class CalculationPerformedEvent : IEvent
    {
        public Calculation Calculation
        {
            get;
        }
        public double Operand1 => Calculation.Operand1;
        public double Operand2 => Calculation.Operand2;
        public string Operation => Calculation.Operation;
        public double Result => Calculation.Result;

        public CalculationPerformedEvent(Calculation calculation)
        {
            Calculation = calculation;
        }
    }
}

