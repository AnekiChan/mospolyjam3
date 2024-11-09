using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _attackInterval = 5f;
    [SerializeField] private float _attackDuration = 2f;
    [Space]
    [SerializeField] private List<BossAttack> _attacks = new List<BossAttack>();

    private bool _isAttacking = false;
    private bool _isMoving = true;
    private int _currentAttackIndex = 0;

    private void Start()
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

        Debug.Log("Boss is attacking!");

        yield return new WaitForSeconds(_attackDuration);
        _isAttacking = false;
        _isMoving = true;
        _currentAttackIndex = (_currentAttackIndex + 1) % _attacks.Count;
    }
}
