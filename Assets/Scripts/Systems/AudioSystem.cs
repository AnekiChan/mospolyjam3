using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _menuMusic;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private AudioClip _battleMusic;

    [Space]
    [SerializeField] private AudioSource _uiAudioSource;
    [SerializeField] private AudioClip _clickButtonSound;

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
    }

    void OnDisable()
    {
        //CommonEvents.Instance.OnGameStart -= SetGameMusic;
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

    public void PlayButtonSound()
    {
        _uiAudioSource.clip = _clickButtonSound;
        _uiAudioSource.Play();
    }

    private void PlayPlayerSound(SoundType type)
    {
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
            case SoundType.Dodge:
                _playerSoundSource.clip = _dodgeSound;
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

