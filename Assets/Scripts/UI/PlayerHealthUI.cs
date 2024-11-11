using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;  // Ссылка на компонент PlayerHealth
    [SerializeField] private Image[] healthImages;       // Массив Image-компонентов для отображения здоровья

    private void OnDisable()
    {
        //CommonEvents.Instance.OnPlayerChangeHealth -= UpdateHealthUI;
    }

    private void Start()
    {
        UpdateHealthUI(5);
        CommonEvents.Instance.OnPlayerChangeHealth += UpdateHealthUI;
    }

    private void UpdateHealthUI(int health)
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            healthImages[i].enabled = i < health;
        }
    }
}
