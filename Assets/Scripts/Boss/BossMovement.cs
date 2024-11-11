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
    private bool _isMoving = false;
    private int _currentAttackIndex = 0;
    private float _attackCooldownTimer = 0f;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public Transform PlayerTransform => _player;
    public bool IsDead { get; set; } = false;

    private void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        StartCoroutine(StartAttack());
    }

    void Update()
    {
        if (IsDead) return;

        if (_isMoving)
        {
            FollowPlayer();
        }

        if (_attackCooldownTimer > 0f) _attackCooldownTimer -= Time.deltaTime;
    }

    private void FollowPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            transform.position += direction * _moveSpeed * Time.deltaTime;

            _animator.SetFloat("Speed", direction.magnitude);
            if (direction.x < 0) _spriteRenderer.flipX = false;
            else _spriteRenderer.flipX = true;
        }
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(1.2f);
        _isMoving = true;

        while (!IsDead)
        {
            yield return new WaitForSeconds(_attackInterval);

            if (!IsDead) StartCoroutine(Attack());

            yield return new WaitForSeconds(_attackDuration);
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        if (!_attacks[_currentAttackIndex].IsMovingWhileAttacking) _isMoving = false;
        _attacks[_currentAttackIndex].StartAttack();
        if (_attacks[_currentAttackIndex].GetType() == typeof(BossAreaAttack)) _animator.SetTrigger("RangeAttack");
        else _animator.SetTrigger("BulletAttack");

        yield return new WaitForSeconds(_attackDuration);
        _isAttacking = false;
        _animator.SetTrigger("StopAttack");
        _isMoving = true;
        _currentAttackIndex = (_currentAttackIndex + 1) % _attacks.Count;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isAttacking && other.tag == "Player" && _attackCooldownTimer <= 0f)
        {
            Debug.Log("Boss melee attack");
            StartCoroutine(MeleeAttack(other.GetComponent<PlayerHealth>()));
            _attackCooldownTimer = 3f;
        }
    }

    private IEnumerator MeleeAttack(PlayerHealth playerHealth)
    {
        _animator.SetTrigger("Attack");
        CommonEvents.Instance.OnBossSoundPlay?.Invoke(AudioSystem.SoundType.Attack);
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
