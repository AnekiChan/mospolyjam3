using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Weapon
{
    [SerializeField] private int _damage = 1;

    [SerializeField] private float _lifetime = 2f;
    [SerializeField] private float _rotation = 0f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _chanceToGlitch = 0.1f;

    private Vector2 _spawnPoint;
    private float _timer;
    private float _glitchTimer = 0f;

    public override int Damage => _damage;

    void Start()
    {
        _glitchTimer = _lifetime + 1;
        _spawnPoint = new Vector2(transform.position.x, transform.position.y);
        if (ProbabilityChecker.CheckProbability(_chanceToGlitch))
        {
            _glitchTimer = Random.Range(0f, _lifetime);
        }
        Destroy(gameObject, _lifetime);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _glitchTimer -= Time.deltaTime;

        if (_glitchTimer < 0)
        {
            _glitchTimer = _lifetime + 1;
            if (ProbabilityChecker.CheckProbability(0.5f)) _speed *= 1.7f;
            else gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 5f);
        }
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
