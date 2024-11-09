using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDamage : Weapon
{
    [SerializeField] private int _damage;
    [SerializeField] private float _timeBeforeAttack;
    [SerializeField] private float _timeAfterAttack;
    [SerializeField] private Vector2 _zoneSize = new Vector2(2, 1);

    public override int Damage => _damage;
    void Start()
    {
        StartCoroutine(WaitingForAttack());
    }

    private IEnumerator WaitingForAttack()
    {
        yield return new WaitForSeconds(_timeBeforeAttack);
        GetComponent<SpriteRenderer>().color = Color.red;
        CheackObject();

        yield return new WaitForSeconds(_timeAfterAttack);
        Destroy(gameObject);
    }

    private void CheackObject()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, _zoneSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Player")
            {
                collider.GetComponent<PlayerHealth>()?.TakeDamage(_damage);
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _zoneSize);
    }
}
