using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    public abstract bool IsMovingWhileAttacking { get; }

    public abstract void StartAttack();
}
