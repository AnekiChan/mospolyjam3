using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : Weapon
{
    private int _damage = 1;

    [SerializeField] private Transform sword;
    [SerializeField] private Transform player;
    [SerializeField] private float swordRotationSpeed = 720f;
    [SerializeField] private float attackDuration = 0.2f;
    [SerializeField] private float swordDistanceFromPlayer = 1f;
    [SerializeField] private float checkRadius = 1f;
    private bool isAttacking = false;

    private Camera cam;

    public override int Damage => _damage;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        RotateSword();
    }

    private void RotateSword()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)player.position;

        if (!isAttacking)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            Vector3 offset = rotation * Vector3.right * swordDistanceFromPlayer;
            sword.position = player.position + offset;
            sword.rotation = rotation;
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = (mousePosition - (Vector2)player.position).normalized;

        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        sword.rotation = Quaternion.Euler(0, 0, attackAngle);

        Vector3 originalPosition = sword.localPosition;
        sword.localPosition += sword.right * 0.5f;
        CheackObject();

        yield return new WaitForSeconds(attackDuration);

        sword.localPosition = originalPosition;
        isAttacking = false;
    }

    private void CheackObject()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag == "Boss")
            {
                collider.GetComponent<BossHealth>()?.TakeDamage(_damage);
                CommonEvents.Instance.OnPlayerSwordAttack?.Invoke();
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
