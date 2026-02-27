using UnityEngine;
using VContainer.Unity;


namespace Calculator.Root
{
    /// <summary>
    /// Точка входа в игру
    /// </summary>
    public class Bootstrapper:MonoBehaviour
    {
        private void Awake()
        {
            // Настройка VContainer (если не настроено через LifetimeScope)
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Debug.Log("Calculator App Started with Clean Architecture!");
            Debug.Log("Доступные операции: +, -, *, /");
        }
    }
}

