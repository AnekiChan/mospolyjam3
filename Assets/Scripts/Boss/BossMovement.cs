using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _attackInterval = 5f;
    [SerializeField] private float _attackDuration = 2f;
    [SerializeField] private float _meleeAttackDuration = 1f;

    [Space]
    [SerializeField] private List<BossAttack> _attacks = new List<BossAttack>();
    [Space]
    [Header("Glitch")]
    [SerializeField] private Vector2 _teleportArea = new Vector2(10f, 10f);

    private bool _isAttacking = false;
    private bool _isMoving = true;
    private int _currentAttackIndex = 0;

    public Transform PlayerTransform => _player;

    private void OnEnable()
    {
        StartCoroutine(FollowAndAttack());
    }

    void Update()
    {
        if (_isMoving)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;
        }
    }

    private IEnumerator FollowAndAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackInterval);

            StartCoroutine(Attack());

            yield return new WaitForSeconds(_attackDuration);
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        if (!_attacks[_currentAttackIndex].IsMovingWhileAttacking) _isMoving = false;
        _attacks[_currentAttackIndex].StartAttack();

        yield return new WaitForSeconds(_attackDuration);
        _isAttacking = false;
        _isMoving = true;
        _currentAttackIndex = (_currentAttackIndex + 1) % _attacks.Count;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Boss melee attack");
            StartCoroutine(MeleeAttack(other.GetComponent<PlayerHealth>()));
        }
    }

    private IEnumerator MeleeAttack(PlayerHealth playerHealth)
    {
        // attack animation
        playerHealth.TakeDamage(1);
        yield return new WaitForSeconds(_meleeAttackDuration);
    }

    public void RandomTeleportGlitch()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-(_teleportArea.x / 2), _teleportArea.x / 2), Random.Range(-(_teleportArea.y / 2), _teleportArea.y / 2), 0f);
        transform.position = randomPosition;
    }
}
