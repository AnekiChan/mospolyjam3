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

    private int spikesSpawned = 0;

    public override bool IsMovingWhileAttacking => _isMovingWhileAttacking;

    public override void StartAttack()
    {
        StartCoroutine(SpawnSpikes());
    }

    private IEnumerator SpawnSpikes()
    {
        while (spikesSpawned < maxSpikes)
        {
            Vector3 spawnPosition = GetRandomPositionInArea();
            Instantiate(_zonePrefab, spawnPosition, Quaternion.identity);

            spikesSpawned++;

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        float xPos = Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float yPos = Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);
        return transform.position + new Vector3(xPos, yPos, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0f));
    }
}
