using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 20;
    [SerializeField] private ParticleSystem _damageParticles;

    private int currentHealth;
    private Animator _animator;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void OnEnable()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            CommonEvents.Instance.OnBossChangeHealth.Invoke(currentHealth);

            _animator.SetTrigger("Hurt");
            _damageParticles.Play();
            //Debug.Log("Boss took damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    public void Heal(int heal)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = (currentHealth + heal) % maxHealth;
            CommonEvents.Instance.OnBossChangeHealth?.Invoke(currentHealth);
            //Debug.Log("Boss healed. Current health: " + currentHealth);
        }
    }

    private IEnumerator Die()
    {
        Debug.Log("BOSS died.");
        GetComponent<BossMovement>().IsDead = true;
        _animator.SetBool("IsDead", true);
        _animator.SetTrigger("Die");

        yield return new WaitForSeconds(2f);
        CommonEvents.Instance.OnBossDeath?.Invoke();
    }
}
