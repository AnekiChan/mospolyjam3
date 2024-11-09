using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : BossAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private bool _isMovingWhileAttacking = false;

    [SerializeField] private SpawnerType _spawnerType;
    [SerializeField] private float _fiiringRate = 1f;
    [SerializeField] private float _maxTimer = 5f;
    [SerializeField] private float _rotationSpeed = 1f;

    private GameObject _spawnedBullet;
    private float _timer = 0;
    private bool _isSpawning = false;

    public override bool IsMovingWhileAttacking => _isMovingWhileAttacking;

    void Update()
    {
        if (_isSpawning) _timer += Time.deltaTime;

        if (_spawnerType == SpawnerType.Spiral)
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + _rotationSpeed);
    }

    public override void StartAttack()
    {
        _timer = 0;
        _isSpawning = true;
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (_timer < _maxTimer)
        {
            yield return new WaitForSeconds(_fiiringRate);
            Fire();
        }

        _isSpawning = false;
    }

    private void Fire()
    {
        if (_bulletPrefab)
        {
            _spawnedBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            _spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}

enum SpawnerType
{
    Straight,
    Spiral
}