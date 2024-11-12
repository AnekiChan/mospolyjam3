using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dodgeSpeed = 10f;
    [SerializeField] private float dodgeDistance = 3f;
    [SerializeField] private float dodgeCooldown = 1f;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    [Space]
    [Header("Glitch")]
    [SerializeField] private PlayerSword _playerSword;
    [SerializeField] private float _movingGlitchDuration = 3f;
    [SerializeField] private Vector2 _teleportArea = new Vector2(10f, 10f);
    [SerializeField] private ParticleSystem _walkParticles;

    private Vector2 movement;
    private float _currentSpeed = 0f;
    private Vector2 lastMovementDirection;
    private Vector2 lastDirection;
    private float lastDodgeTime;
    private bool _isMoving = true;

    public bool IsDead { get; set; } = false;
    public bool IsDodge { get; private set; } = false;

    void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentSpeed = moveSpeed;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.magnitude > 0)
        {
            lastMovementDirection = movement;
            if (!_walkParticles.isPlaying) _walkParticles.Play();
        }
        else _walkParticles.Stop();

        _animator.SetFloat("Speed", movement.magnitude);
        if (movement.x > 0) gameObject.transform.rotation = Quaternion.Euler(0, 0, 0); //_spriteRenderer.flipX = false;
        else if (movement.x < 0) gameObject.transform.rotation = Quaternion.Euler(0, 180, 0); // _spriteRenderer.flipX = true;

        // Уклонение на Shift
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDodgeTime + dodgeCooldown && _isMoving)
        {
            StartCoroutine(Dodge());
        }

        if (_isMoving && !IsDead) MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 newPosition = transform.position + (Vector3)movement * _currentSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private IEnumerator Dodge()
    {
        IsDodge = true;
        lastDodgeTime = Time.time;
        _currentSpeed = dodgeSpeed;
        CommonEvents.Instance.OnPlayerSoundPlay?.Invoke(AudioSystem.SoundType.Dodge);

        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(dodgeCooldown);
        collider.enabled = true;

        _currentSpeed = moveSpeed;
        IsDodge = false;
        //Vector3 dodgePosition = transform.position + (Vector3)lastMovementDirection * dodgeDistance;
        //transform.position = dodgePosition;

    }

    public void SwordGlitch()
    {
        _playerSword.HideSword();
        CommonEvents.Instance.OnRandomGlitchSound?.Invoke();
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
