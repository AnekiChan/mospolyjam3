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
}
