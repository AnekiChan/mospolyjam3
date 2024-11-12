using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private AudioClip _battleMusic;
    [SerializeField] private AudioClip _noise;

    [Space]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _clickButtonSound;
    [SerializeField] private AudioClip _breakSound;

    [Space]
    [SerializeField] private AudioSource _glitchesAudioSource;
    [SerializeField] private List<AudioClip> _glitchesSounds = new List<AudioClip>();

    [Space]
    [SerializeField] private AudioSource _playerSoundSource;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _dodgeSound;
    [Space]
    [SerializeField] private AudioSource _bossSoundSource;
    [SerializeField] private AudioClip _bossDamageSound;
    [SerializeField] private AudioClip _bossDeathSound;
    [SerializeField] private AudioClip _bossHitSound;
    [SerializeField] private AudioClip _bossShootSound;
    [SerializeField] private AudioClip _bossExplosionSound;

    void Start()
    {
        CommonEvents.Instance.OnGameStart += SetGameMusic;
        CommonEvents.Instance.OnBattleStart += SetBattleMusic;
        CommonEvents.Instance.OnPlayerSoundPlay += PlayPlayerSound;
        CommonEvents.Instance.OnBossSoundPlay += PlayBossSound;
        CommonEvents.Instance.OnPlayerDeath += SetMenuMusic;
        CommonEvents.Instance.OnBossDeath += StopMusic;

        CommonEvents.Instance.OnNoiseStart += SetNoise;
        CommonEvents.Instance.OnMusicStop += StopMusic;
        CommonEvents.Instance.OnBreakScreen += BreakSound;
        CommonEvents.Instance.OnRandomGlitchSound += RandomGlitchSound;
    }

    void OnDisable()
    {
        //CommonEvents.Instance.OnGameStart -= SetGameMusic;
    }

    private void SetMenuMusic()
    {
        _musicSource.clip = _menuMusic;
        _musicSource.Play();
    }

    private void SetGameMusic()
    {
        _musicSource.clip = _gameMusic;
        _musicSource.Play();
    }

    private void SetBattleMusic()
    {
        _musicSource.clip = _battleMusic;
        _musicSource.Play();
    }

    private void SetNoise()
    {
        _musicSource.clip = _noise;
        _musicSource.Play();
    }

    private void StopMusic()
    {
        _musicSource.Stop();
    }

    private void BreakSound()
    {
        _uiAudioSource.clip = _breakSound;
        _uiAudioSource.Play();
    }

    private void RandomGlitchSound()
    {
        _glitchesAudioSource.clip = _glitchesSounds[Random.Range(0, _glitchesSounds.Count)];
        _glitchesAudioSource?.Play();
    }

    public void PlayButtonSound()
    {
        _uiAudioSource.clip = _clickButtonSound;
        _uiAudioSource.Play();
    }

    private void PlayPlayerSound(SoundType type)
    {
        _playerSoundSource.clip = null;
        switch (type)
        {
            case SoundType.Damage:
                _playerSoundSource.clip = _damageSound;
                break;
            case SoundType.Death:
                _playerSoundSource.clip = _deathSound;
                break;
            case SoundType.Attack:
                _playerSoundSource.clip = _hitSound;
                break;
            case SoundType.Shoot:
                _playerSoundSource.clip = _shootSound;
                break;
            default:
                break;
        }
        _playerSoundSource?.Play();
    }

    private void PlayBossSound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Damage:
                _playerSoundSource.clip = _bossDamageSound;
                break;
            case SoundType.Death:
                _playerSoundSource.clip = _bossDeathSound;
                break;
            case SoundType.Attack:
                _playerSoundSource.clip = _bossHitSound;
                break;
            case SoundType.Shoot:
                _playerSoundSource.clip = _bossShootSound;
                break;
            case SoundType.Explosion:
                _playerSoundSource.clip = _bossExplosionSound;
                break;
            default:
                break;
        }
        _playerSoundSource?.Play();
    }

    public enum SoundType
    {
        Damage,
        Death,
        Attack,
        Shoot,
        Explosion,
        Dodge
    }
}

