using UnityEngine;
using UnityEngine.UI;
using System;

public class BossHealthUI : MonoBehaviour
{
    private Slider _healthSlider;
    [SerializeField] private BossHealth _bossHealth;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
        _healthSlider.maxValue = _bossHealth.MaxHealth;
        _healthSlider.value = _bossHealth.MaxHealth;

        CommonEvents.Instance.OnBossChangeHealth += UpdateHealthUI;
    }

    private void OnDisable()
    {
        CommonEvents.Instance.OnBossChangeHealth -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int currentHealth)
    {
        _healthSlider.value = currentHealth;
    }
}
