using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab; // Префаб снаряда
    [SerializeField] private float projectileSpeed = 10f; // Скорость снаряда
    [SerializeField] private float projectileLifetime = 2f; // Время жизни снаряда

    [SerializeField] private int _maxMana = 10;
    [SerializeField] private Animator _animator;

    private int _currentMana = 0;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        _currentMana = _maxMana;
        CommonEvents.Instance.OnPlayerSwordAttack += UpdateMana;
    }

    void OnDisable()
    {
        //CommonEvents.Instance.OnPlayerSwordAttack -= UpdateMana;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_currentMana == _maxMana)
            {
                LaunchProjectile();
                _currentMana = 0;
                CommonEvents.Instance.OnPlayerManaChanged.Invoke(_currentMana);
            }
        }
    }

    private void LaunchProjectile()
    {
        if (_animator) _animator.SetTrigger("Throw");

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

        Destroy(projectile, projectileLifetime);
    }

    private void UpdateMana()
    {
        if (_currentMana < _maxMana)
        {
            _currentMana++;
            CommonEvents.Instance.OnPlayerManaChanged.Invoke(_currentMana);
            //Debug.Log("Current mana: " + _currentMana);
        }
    }
}
