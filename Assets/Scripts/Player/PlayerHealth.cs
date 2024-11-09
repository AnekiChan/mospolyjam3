using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

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
            CommonEvents.Instance.OnPlayerChangeHealth?.Invoke(currentHealth);
            //Debug.Log("Player took damage. Current health: " + currentHealth);

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
            CommonEvents.Instance.OnPlayerChangeHealth?.Invoke(currentHealth);
            //Debug.Log("Player healed. Current health: " + currentHealth);
        }
    }

    private void Die()
    {
        Debug.Log("Player died.");
        CommonEvents.Instance.OnPlayerDeath?.Invoke();
    }
}
