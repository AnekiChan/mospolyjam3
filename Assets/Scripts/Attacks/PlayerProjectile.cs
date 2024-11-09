using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Weapon
{
    [SerializeField] private int _damage = 3;

    public override int Damage => _damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss")
        {
            other.GetComponent<BossHealth>()?.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
