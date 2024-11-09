using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaAttack : BossAttack
{
    [SerializeField] private GameObject _zonePrefab;
    [SerializeField] private bool _isMovingWhileAttacking = true;

    [SerializeField] private Vector2 spawnAreaSize = new Vector2(10f, 10f);
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxSpikes = 10;

    [Space]
    [Header("Glitch")]
    //[SerializeField] private float _chanceToGlitch = 1f;
    private float _glitchStartTimer = 0f;
    [SerializeField] private BossMovement _bossMovement;

    private int spikesSpawned = 0;
    private bool _isSpawning = false;
    private float _timer = 0f;
    private bool _isGlitchWasActive = false;

    public override bool IsMovingWhileAttacking => _isMovingWhileAttacking;

    public override void StartAttack()
    {
        _timer = 0;
        spikesSpawned = 0;
        _isSpawning = true;
        _isGlitchWasActive = false;

        StartCoroutine(SpawnSpikes());
    }

    void Update()
    {
        if (_isSpawning) _timer += Time.deltaTime;
    }

    private IEnumerator SpawnSpikes()
    {
        _glitchStartTimer = spawnInterval * maxSpikes;
        if (ProbabilityChecker.CheckProbability(0.5f))
        {
            _glitchStartTimer = Random.Range(0.5f, spawnInterval * maxSpikes / 2);
            Debug.Log("Area Attack Glitching");
        }

        while (spikesSpawned < maxSpikes)
        {
            Vector3 spawnPosition;
            if (_timer > _glitchStartTimer)
            {
                if (!_isGlitchWasActive)
                {
                    CommonEvents.Instance.OnDigitalGlitch?.Invoke();
                    _isGlitchWasActive = true;
                }

                spawnPosition = _bossMovement.PlayerTransform.position;
            }
            else
                spawnPosition = GetRandomPositionInArea();

            Instantiate(_zonePrefab, spawnPosition, Quaternion.identity);
            spikesSpawned++;

            yield return new WaitForSeconds(spawnInterval);
        }

        _isSpawning = false;
    }

    private Vector3 GetRandomPositionInArea()
    {
        float xPos = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float yPos = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);
        return new Vector3(xPos, yPos, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0f));
    }
}
