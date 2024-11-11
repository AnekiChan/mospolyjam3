using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private Animator _animator;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

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
            _animator.SetTrigger("Hurt");
            //Debug.Log("Player took damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
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

    private IEnumerator Die()
    {
        Debug.Log("Player died.");
        _animator.SetTrigger("Die");
        GetComponent<PlayerController>().IsDead = true;

        yield return new WaitForSeconds(2f);
        CommonEvents.Instance.OnPlayerDeath?.Invoke();
    }
}
