using System.Collections;
using System.Collections.Generic;
using Kino;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FinalCutscene : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string[] textArray;
    [SerializeField] private GameObject _dark;
    [SerializeField] private GameObject _break;
    [SerializeField] private DigitalGlitch _digitalGlitch;
    [SerializeField] private AnalogGlitch _analogGlitch;

    [Space]
    [SerializeField] private float delay = 0.1f; // Задержка между символами
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float _digitalGlitchIntensity = 0.5f;
    [SerializeField] private float[] _analogGlitchSettings = new float[4];

    private string _currentText = "";
    private string _fullText = "";
    private bool _isTyping = false;

    void Awake()
    {
        _dark.SetActive(false);
        _break.SetActive(false);
        _digitalGlitch.enabled = false;
    }

    public void StartFinalCutscene()
    {
        StartCoroutine(StartCutscene());
    }

    private IEnumerator StartCutscene()
    {
        _isTyping = true;

        _text.text = "";
        //yield return new WaitForSeconds(startDelay);

        foreach (string phrase in textArray)
        {
            CommonEvents.Instance.OnNoiseStart?.Invoke();
            _digitalGlitch.intensity = _digitalGlitchIntensity;
            _digitalGlitch.enabled = true;
            yield return new WaitForSeconds(1f);

            if (textArray[textArray.Length - 1] == phrase)
            {

                yield return new WaitForSeconds(0.5f);

                _analogGlitch.scanLineJitter = _analogGlitchSettings[0];
                _analogGlitch.verticalJump = _analogGlitchSettings[1];
                _analogGlitch.horizontalShake = _analogGlitchSettings[2];
                _analogGlitch.colorDrift = _analogGlitchSettings[3];
            }
            _text.text = "";
            _fullText = phrase;
            for (int i = 0; i < _fullText.Length; i++)
            {
                _currentText = _fullText.Substring(0, i + 1);  // Постепенно берем символы от 0 до i
                _text.text = _currentText;                 // Присваиваем обновленный текст компоненту TextMesh
                yield return new WaitForSeconds(delay);       // Ждем заданное время перед следующей буквой
            }
        }
        _isTyping = false;

        yield return new WaitForSeconds(0.7f);

        CommonEvents.Instance.OnMusicStop?.Invoke();
        _dark.SetActive(true);
        _digitalGlitch.enabled = false;
        _analogGlitch.enabled = false;
        yield return new WaitForSeconds(0.8f);
        CommonEvents.Instance.OnBreakScreen?.Invoke();
        yield return new WaitForSeconds(0.12f);
        _break.SetActive(true);
        // звук

        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
