using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletSpawner : BossAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private bool _isMovingWhileAttacking = false;

    [SerializeField] private float _fiiringRate = 1f;
    [SerializeField] private float _maxTimer = 5f;
    [SerializeField] private float _rotationSpeed = 1f;

    [Space]
    [Header("Glitch")]
    private float _glitchStartTimer = 0f;

    private GameObject _spawnedBullet;
    private float _timer = 0;
    private bool _isSpawning = false;
    private bool _isGlitchWasActive = false;

    public override bool IsMovingWhileAttacking => _isMovingWhileAttacking;

    void Update()
    {
        if (_isSpawning)
        {
            _timer += Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + _rotationSpeed);
        }
    }

    public override void StartAttack()
    {
        _timer = 0;
        _isSpawning = true;
        _isGlitchWasActive = false;

        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        _glitchStartTimer = _maxTimer;
        if (ProbabilityChecker.CheckProbability(0.5f))
        {
            _glitchStartTimer = Random.Range(0.5f, _maxTimer / 2);
            Debug.Log("Bullet Spawner Glitching");
        }

        while (_timer < _maxTimer)
        {
            if (!_isGlitchWasActive)
            {
                CommonEvents.Instance.OnDigitalGlitch?.Invoke();
                _isGlitchWasActive = true;
            }

            yield return new WaitForSeconds(_fiiringRate);
            if (_timer < _glitchStartTimer) Fire();
            else FireRandom();
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

    private void FireRandom()
    {
        if (_bulletPrefab)
        {
            _spawnedBullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            _spawnedBullet.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
    }
}