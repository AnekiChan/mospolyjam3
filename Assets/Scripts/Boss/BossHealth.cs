using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;

    private int currentHealth;

    public int CurrentHealth => currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            CommonEvents.Instance.OnBossChangeHealth?.Invoke(currentHealth);
            Debug.Log("Boss took damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth++;
            CommonEvents.Instance.OnBossChangeHealth?.Invoke(currentHealth);
            Debug.Log("Boss healed. Current health: " + currentHealth);
        }
    }

    private void Die()
    {
        Debug.Log("Boss died.");
        CommonEvents.Instance.OnBossDeath?.Invoke();
    }
}
