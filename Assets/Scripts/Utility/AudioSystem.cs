using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;

    [Space]
    [SerializeField] private AudioSource _soundSource;

    void Start()
    {
        CommonEvents.Instance.OnGameStart += SetGameMusic;
    }

    void OnDisable()
    {
        CommonEvents.Instance.OnGameStart -= SetGameMusic;
    }

    private void SetGameMusic()
    {
        _musicSource.clip = _gameMusic;
        _musicSource.Play();
    }

}
