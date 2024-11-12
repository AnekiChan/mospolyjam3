using System.Collections;
using UnityEngine;

public class MusicGlitch : VisualGlitch
{
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private float _minDuration = 1f;
    [SerializeField] private float _maxDuration = 3f;

    private float _duration;

    public override float Duration => _duration;

    public override void StartGlitch()
    {
        _duration = Random.Range(_minDuration, _maxDuration);
        StartCoroutine(GlitchCoroutine());
        Debug.Log("Music glitch started");
    }

    private IEnumerator GlitchCoroutine()
    {
        _musicAudioSource.pitch += Random.Range(-0.15f, 0.15f);
        yield return new WaitForSeconds(_duration);
        _musicAudioSource.pitch = 1f;
    }
}
