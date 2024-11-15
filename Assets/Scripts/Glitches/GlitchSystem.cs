using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class GlitchSystem : MonoBehaviour
{
    // внешние глитчи
    [SerializeField] private float _startIntervalVisual = 5f;
    //[SerializeField] private float _endIntervalVisual = 1f;
    [SerializeField] private List<VisualGlitch> _visualGlitches = new List<VisualGlitch>();

    [Space]
    // глитчи босса/игрока
    [SerializeField] private float _startInterval = 5f;
    [SerializeField] private float _endInterval = 1f;
    [SerializeField] private float _intervalDecrease = 0.1f;
    [SerializeField] private int _intervalDecreaseSpeed = 5;
    [SerializeField] private int _chanceToGlitchIncreaseSpeed = 3;

    [Space]
    [SerializeField] private DigitalGlitch _digitalGlitch;
    [SerializeField] private float _digitalGlitchDuration = 1f;

    [Space]
    [SerializeField] private BossMovement _bossMovement;
    [SerializeField] private BossHealth _bossHealth;
    [SerializeField] private PlayerController _playerController;


    private int _glitchesCount = 0;
    private int _glitchVariantsCount = 1;

    private float _currentIntervalVisual = 0f;
    private float _currentInterval = 0f;
    private static float _chanceToGlitch = 0.3f;
    public static float ChanceToGlitch => _chanceToGlitch;

    void OnEnable()
    {
        _currentInterval = _startInterval;
        StartCoroutine(VisualGlitch());
        StartCoroutine(BossAndPlayerGlitch());

        CommonEvents.Instance.OnDigitalGlitch += StartDigitalGlitch;
    }

    void OnDisable()
    {
        //CommonEvents.Instance.OnDigitalGlitch -= StartDigitalGlitch;
    }

    private IEnumerator VisualGlitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(_startIntervalVisual);
            //Debug.Log("Visual glitch");

            int randomGlitch = Random.Range(0, _visualGlitches.Count);
            _visualGlitches[randomGlitch].StartGlitch();
            CommonEvents.Instance.OnRandomGlitchSound?.Invoke();
        }
    }

    private IEnumerator BossAndPlayerGlitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(_startInterval);
            ActiveBossOrPlayerGlitch();
            StartDigitalGlitch();
            _glitchesCount++;
            CheackBossAndPlayerGlitchVariants();
        }
    }

    private void ActiveBossOrPlayerGlitch()
    {
        switch (Random.Range(0, _glitchVariantsCount))
        {
            case 0:
                {
                    //Debug.Log("Teleport glitch");
                    _bossMovement.RandomTeleportGlitch();
                }
                break;
            case 1:
                {
                    //Debug.Log("Sword glitch");
                    _playerController.SwordGlitch();
                }
                break;
            case 2:
                {
                    //Debug.Log("Player teleport glitch");
                    _playerController.RandomTeleportGlitch();
                }
                break;
            case 3:
                {
                    //Debug.Log("Movement glitch");
                    _playerController.MovementGlitch();
                }
                break;
            case 4:
                {
                    Debug.Log("Boss heal glitch");
                    _bossHealth.Heal(Random.Range(5, 20));
                }
                break;
        }
    }

    private void CheackBossAndPlayerGlitchVariants()
    {
        switch (_glitchesCount)
        {
            case 2:
                _glitchVariantsCount = 2;
                break;
            case 4:
                _glitchVariantsCount = 3;
                break;
            case 6:
                _glitchVariantsCount = 4;
                break;
            case 10:
                _glitchVariantsCount = 5;
                break;
        }

        if (_chanceToGlitch < 1 && _glitchesCount % _chanceToGlitchIncreaseSpeed == 0)
            _chanceToGlitch += 0.1f;

        if (_currentInterval > _endInterval && _glitchesCount % _intervalDecreaseSpeed == 0)
        {
            _currentInterval -= _intervalDecrease;
            _currentIntervalVisual -= _intervalDecrease;
            if (_digitalGlitch.intensity < 0.9f) _digitalGlitch.intensity += 0.1f;
        }
    }

    public void StartDigitalGlitch()
    {
        StartCoroutine(DigitalGlitch());
    }

    private IEnumerator DigitalGlitch()
    {
        _digitalGlitch.enabled = true;
        yield return new WaitForSeconds(_digitalGlitchDuration);
        _digitalGlitch.enabled = false;
    }
}
