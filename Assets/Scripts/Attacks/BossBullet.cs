using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Weapon
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _lifetime = 2f;
    [SerializeField] private float _rotation = 0f;
    [SerializeField] private float _speed = 5f;

    private Vector2 _spawnPoint;
    private float _timer;

    public override int Damage => _damage;

    void Start()
    {
        _spawnPoint = new Vector2(transform.position.x, transform.position.y);
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        transform.position = Movement(_timer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else if (other.tag != "Boss")
        {
            Destroy(gameObject);
        }
    }

    private Vector2 Movement(float timer)
    {
        float x = timer * _speed * transform.right.x;
        float y = timer * _speed * transform.right.y;
        return new Vector2(x + _spawnPoint.x, y + _spawnPoint.y);
    }
}
