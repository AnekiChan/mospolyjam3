using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommonEvents : MonoBehaviour
{
    public static CommonEvents Instance;

    void Awake()
    {
        Instance = this;
    }

    public Action OnPlayerSwordAttack;
    public Action<int> OnPlayerManaChanged;
    public Action<int> OnPlayerChangeHealth;
    public Action OnPlayerDeath;

    public Action<int> OnBossChangeHealth;
    public Action OnBossDeath;

    public Action OnDigitalGlitch;

    // общее
    public Action OnFirstTextEnded;
    public Action OnGameStart;
    public Action OnBattleStart;
    public Action OnBattleEnd;
    public Action OnGameEnd;

    public Action<AudioSystem.SoundType> OnPlayerSoundPlay;
    public Action<AudioSystem.SoundType> OnBossSoundPlay;
}
