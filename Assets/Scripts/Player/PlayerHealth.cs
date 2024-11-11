using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float _damageCooldown = 1f;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ParticleSystem _damageParticles;

    private Animator _animator;
    private int currentHealth;
    private float _damageTimer = 0f;

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
        if (currentHealth > 0 && !_playerController.IsDodge)
        {
            currentHealth -= damage;
            CommonEvents.Instance.OnPlayerChangeHealth?.Invoke(currentHealth);
            StartCoroutine(SwitchOffCollider());
            //_damageTimer = _damageCooldown;
            _animator.SetTrigger("Hurt");
            CommonEvents.Instance.OnPlayerSoundPlay?.Invoke(AudioSystem.SoundType.Damage);
            _damageParticles.Play();
            //Debug.Log("Player took damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }
    }

    private IEnumerator SwitchOffCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(_damageCooldown);
        collider.enabled = true;
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
        CommonEvents.Instance.OnPlayerSoundPlay?.Invoke(AudioSystem.SoundType.Death);
        _playerController.IsDead = true;

        yield return new WaitForSeconds(2f);
        CommonEvents.Instance.OnPlayerDeath?.Invoke();
    }
}
