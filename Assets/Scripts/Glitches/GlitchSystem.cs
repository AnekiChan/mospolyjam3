using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class GlitchSystem : MonoBehaviour
{
    // внешние глитчи
    [SerializeField] private float _startIntervalVisual = 5f;
    [SerializeField] private float _endIntervalVisual = 1f;
    [SerializeField] private List<VisualGlitch> _visualGlitches = new List<VisualGlitch>();

    [Space]
    // глитчи босса/игрока
    [SerializeField] private float _startInterval = 5f;
    [SerializeField] private float _endInterval = 1f;
    [SerializeField] private DigitalGlitch _digitalGlitch;
    [SerializeField] private float _digitalGlitchDuration = 1f;

    private int _glitchesCount = 0;

    [Space]
    [SerializeField] private BossMovement _bossMovement;
    [SerializeField] private PlayerController _playerController;

    void Start()
    {
        StartCoroutine(VisualGlitch());
        StartCoroutine(BossAndPlayerGlitch());
    }

    private IEnumerator VisualGlitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(_startIntervalVisual);
            int randomGlitch = Random.Range(0, _visualGlitches.Count);
            _visualGlitches[randomGlitch].StartGlitch();
        }
    }

    private IEnumerator BossAndPlayerGlitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(_startInterval);
            ActiveBossOrPlayerGlitch();
            StartCoroutine(DigitalGlitch());
            _glitchesCount++;
        }
    }

    private void ActiveBossOrPlayerGlitch()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                {
                    Debug.Log("Teleport glitch");
                    _bossMovement.RandomTeleportGlitch();
                }
                break;
            case 1:
                {
                    Debug.Log("Sword glitch");
                    _playerController.SwordGlitch();
                }
                break;
            case 2:
                {
                    Debug.Log("Movement glitch");
                    _playerController.MovementGlitch();
                }
                break;
            case 3:
                {

                }
                break;
            case 4:
                {

                }
                break;
            case 5:
                {

                }
                break;
        }
    }

    private IEnumerator DigitalGlitch()
    {
        _digitalGlitch.enabled = true;
        yield return new WaitForSeconds(_digitalGlitchDuration);
        _digitalGlitch.enabled = false;
    }
}
