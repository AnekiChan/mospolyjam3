using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeDistance = 3f;
    [SerializeField] private float dodgeCooldown = 1f;
    [SerializeField] private Animator _animator;

    [Space]
    [Header("Glitch")]
    [SerializeField] private PlayerSword _playerSword;
    [SerializeField] private float _movingGlitchDuration = 3f;
    [SerializeField] private Vector2 _teleportArea = new Vector2(10f, 10f);

    private Vector2 movement;
    private Vector2 lastMovementDirection;
    private Vector2 lastDirection;
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

        ChangeAnimation();
    }

    private void ChangeAnimation()
    {
        // Если игрок не двигается, переключаемся на Idle, сбросив все триггеры
        if (movement.magnitude == 0)
        {
            _animator.ResetTrigger("Up");
            _animator.ResetTrigger("Down");
            _animator.ResetTrigger("Right");
            _animator.SetTrigger("Idle");
            return; // Завершаем выполнение метода
        }

        // Если направление движения изменилось, устанавливаем триггеры для соответствующей анимации
        if (movement != lastDirection)
        {
            lastDirection = movement;

            if (movement.y > 0) // Движение вверх
            {
                _animator.ResetTrigger("Down");
                _animator.ResetTrigger("Right");
                _animator.SetTrigger("Up");
                transform.localScale = new Vector3(1, 1, 1); // Сбрасываем зеркалирование
            }
            if (movement.y < 0) // Движение вниз
            {
                _animator.ResetTrigger("Up");
                _animator.ResetTrigger("Right");
                _animator.SetTrigger("Down");
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (movement.x > 0) // Движение вправо
            {
                _animator.ResetTrigger("Up");
                _animator.ResetTrigger("Down");
                _animator.SetTrigger("Right");
                transform.localScale = new Vector3(1, 1, 1); // Сбрасываем зеркалирование
            }
            if (movement.x < 0) // Движение влево
            {
                _animator.ResetTrigger("Up");
                _animator.ResetTrigger("Down");
                _animator.SetTrigger("Right");
                transform.localScale = new Vector3(-1, 1, 1); // Отражаем спрайт по оси X
            }
        }
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

    public void RandomTeleportGlitch()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-(_teleportArea.x / 2), _teleportArea.x / 2), Random.Range(-(_teleportArea.y / 2), _teleportArea.y / 2), 0f);
        transform.position = randomPosition;
    }
}
