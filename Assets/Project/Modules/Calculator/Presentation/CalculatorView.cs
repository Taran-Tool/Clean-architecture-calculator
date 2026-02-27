using Calculator.Domain.Entities;
using Calculator.Presentation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Calculator.Presentation
{
    /// <summary>
    /// View - отвечает только за отображение
    /// Не содержит логики, только вызовы методов презентера
    /// </summary>
    public class CalculatorView:MonoBehaviour
    {
        [Header("Input Fields")]
        [SerializeField] private TMP_InputField firstNumberInput;
        [SerializeField] private TMP_InputField secondNumberInput;

        [Header("Display")]
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private Transform historyContainer;
        [SerializeField] private GameObject historyItemPrefab;

        [Header("Buttons")]
        [SerializeField] private Button addButton;
        [SerializeField] private Button subtractButton;
        [SerializeField] private Button multiplyButton;
        [SerializeField] private Button divideButton;

        private CalculatorPresenter _presenter;

        [Inject]
        public void Construct(CalculatorPresenter presenter)
        {
            _presenter = presenter;
        }

        private void Start()
        {
            // Находим презентер (в реальном проекте он внедряется через VContainer)
            _presenter = GetComponent<CalculatorPresenter>();

            // Подписка на кнопки
            addButton.onClick.AddListener(() => _presenter.OnCalculateButtonClicked("+"));
            subtractButton.onClick.AddListener(() => _presenter.OnCalculateButtonClicked("-"));
            multiplyButton.onClick.AddListener(() => _presenter.OnCalculateButtonClicked("*"));
            divideButton.onClick.AddListener(() => _presenter.OnCalculateButtonClicked("/"));

            ClearError();
        }

        /// <summary>
        /// Попытаться получить числа из полей ввода
        /// </summary>
        public bool TryGetOperands(out double a, out double b)
        {
            bool validA = double.TryParse(firstNumberInput.text, out a);
            bool validB = double.TryParse(secondNumberInput.text, out b);

            return validA && validB;
        }

        /// <summary>
        /// Установить результат
        /// </summary>
        public void SetResult(double result)
        {
            resultText.text = $"= {result}";
            resultText.color = Color.green;
            ClearError();
        }

        /// <summary>
        /// Показать ошибку
        /// </summary>
        public void ShowError(string message)
        {
            errorText.text = message;
            errorText.gameObject.SetActive(true);
            resultText.text = "= ?";
            resultText.color = Color.red;
        }

        /// <summary>
        /// Очистить сообщение об ошибке
        /// </summary>
        public void ClearError()
        {
            errorText.gameObject.SetActive(false);
        }

        /// <summary>
        /// Очистить поля ввода
        /// </summary>
        public void ClearInputs()
        {
            firstNumberInput.text = "";
            secondNumberInput.text = "";
        }

        /// <summary>
        /// Обновить историю вычислений
        /// </summary>
        public void UpdateHistory(IEnumerable<Calculation> history)
        {
            // Очистить старую историю
            foreach (Transform child in historyContainer)
            {
                Destroy(child.gameObject);
            }

            // Создать новые элементы
            foreach (var calculation in history)
            {
                var item = Instantiate(historyItemPrefab, historyContainer);
                item.GetComponentInChildren<TextMeshProUGUI>().text =
                    calculation.GetFormattedExpression();
            }
        }
    }
}
