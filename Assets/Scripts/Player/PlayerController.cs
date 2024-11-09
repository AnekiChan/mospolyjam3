using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeDistance = 3f;
    [SerializeField] private float dodgeCooldown = 1f;
    private Vector2 movement;
    private Vector2 lastMovementDirection;
    private float lastDodgeTime;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.magnitude > 0)
            lastMovementDirection = movement;

        // Уклонение на Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDodgeTime + dodgeCooldown)
        {
            Dodge();
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 newPosition = transform.position + (Vector3)movement * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void Dodge()
    {
        Debug.Log("Dodge");
        lastDodgeTime = Time.time;
        Vector3 dodgePosition = transform.position + (Vector3)lastMovementDirection * dodgeDistance;
        transform.position = dodgePosition;
    }
}
