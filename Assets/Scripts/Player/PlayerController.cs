using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeDistance = 3f;
    [SerializeField] private float dodgeCooldown = 1f;

    [Space]
    [Header("Glitch")]
    [SerializeField] private PlayerSword _playerSword;
    [SerializeField] private float _movingGlitchDuration = 3f;

    private Vector2 movement;
    private Vector2 lastMovementDirection;
    private float lastDodgeTime;
    private bool _isMoving = true;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.magnitude > 0)
            lastMovementDirection = movement;

        // Уклонение на Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDodgeTime + dodgeCooldown && _isMoving)
        {
            Dodge();
        }

        if (_isMoving) MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 newPosition = transform.position + (Vector3)movement * moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void Dodge()
    {
        lastDodgeTime = Time.time;
        Vector3 dodgePosition = transform.position + (Vector3)lastMovementDirection * dodgeDistance;
        transform.position = dodgePosition;
    }

    public void SwordGlitch()
    {
        _playerSword.HideSword();
    }

    public void MovementGlitch()
    {
        StartCoroutine(StopMovement());
    }

    private IEnumerator StopMovement()
    {
        _isMoving = false;
        yield return new WaitForSeconds(_movingGlitchDuration);
        _isMoving = true;
    }
}
