using System.Collections;
using Kino;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameMachine : MonoBehaviour
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject GameOverPanel;

    [Space]
    [SerializeField] private GameObject _glitchSystem;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _bossHealthBar;
    [SerializeField] private GameObject _player;

    [Space]
    [SerializeField] private DigitalGlitch _digitalGlitch;
    [SerializeField] private AnalogGlitch _analogGlitch;
    void Start()
    {
        StartMenu.SetActive(true);
        GameOverPanel.SetActive(false);

        _glitchSystem.SetActive(false);
        _boss.SetActive(false);
        _bossHealthBar.SetActive(false);
        _player.SetActive(false);

        CommonEvents.Instance.OnBattleStart += StartBattle;
        CommonEvents.Instance.OnPlayerDeath += GameOver;
    }

    void OnDisable()
    {
        //CommonEvents.Instance.OnBattleStart -= StartBattle;
        //CommonEvents.Instance.OnPlayerDeath -= GameOver;
    }

    public void StartGame()
    {
        _player.SetActive(true);
        StartMenu.SetActive(false);
        CommonEvents.Instance.OnGameStart?.Invoke();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void StartBattle()
    {
        _glitchSystem.SetActive(true);
        _boss.SetActive(true);
        _bossHealthBar.SetActive(true);
    }

    private void GameOver()
    {
        GameOverPanel.SetActive(true);
        _boss.SetActive(false);
        _player.SetActive(false);
        _glitchSystem.SetActive(false);

        StartCoroutine(GlitchEffect());
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator GlitchEffect()
    {
        _analogGlitch.scanLineJitter = 0.055f;
        _analogGlitch.verticalJump = 0.036f;
        _analogGlitch.horizontalShake = 0f;
        _analogGlitch.colorDrift = 0.015f;
        _analogGlitch.enabled = true;

        _digitalGlitch.intensity = 0.113f;

        while (true)
        {
            _digitalGlitch.enabled = true;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            _digitalGlitch.enabled = false;
            yield return new WaitForSeconds(Random.Range(2f, 5f));
        }
    }
}
