using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCircleBulletSpawner : BossAttack
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private bool _isMovingWhileAttacking = false;

    [SerializeField] private float _firingRate = 1f;
    [SerializeField] private float _maxTimer = 5f;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private int[] _bulletsPerWave;

    [Space]
    [Header("Glitch")]
    private float _glitchStartTimer = 0f;

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

        StartCoroutine(SpawningWaves());
    }

    private IEnumerator SpawningWaves()
    {
        _glitchStartTimer = _maxTimer;
        if (ProbabilityChecker.CheckProbability(0.5f))
        {
            _glitchStartTimer = Random.Range(0.5f, _maxTimer / 2);
            if (ProbabilityChecker.CheckProbability(0.5f)) CommonEvents.Instance.OnRandomGlitchSound?.Invoke();
            Debug.Log("Bullet Spawner Glitching");
        }

        foreach (int bulletCount in _bulletsPerWave)
        {
            FireWave(bulletCount);
            yield return new WaitForSeconds(_firingRate);
        }

        _isSpawning = false;
    }

    private void FireWave(int bulletCount)
    {
        float angleStep = 360f / bulletCount; // Угол между снарядами

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Instantiate(_bulletPrefab, transform.position, rotation);
        }
    }
}
