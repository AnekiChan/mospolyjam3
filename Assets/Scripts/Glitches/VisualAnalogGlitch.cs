using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class VisualAnalogGlitch : VisualGlitch
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private AnalogGlitch _analogGlitch;


    public override float Duration => _duration;

    void Start()
    {
        _analogGlitch.enabled = false;
    }

    public override void StartGlitch()
    {
        StartCoroutine(ActiveGlitch());
    }

    private IEnumerator ActiveGlitch()
    {
        _analogGlitch.enabled = true;
        yield return new WaitForSeconds(_duration);
        _analogGlitch.enabled = false;
    }
}
