using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VisualGlitch : MonoBehaviour
{
    public abstract float Duration { get; }

    public abstract void StartGlitch();
}
