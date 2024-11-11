using System.Collections;
using System.Collections.Generic;
using Kino;
using UnityEngine;

public class VisualAnalogGlitch : VisualGlitch
{
    [SerializeField] private float _duration = 3f;
    [SerializeField] private AnalogGlitch _analogGlitch;

    private float[] _settings = new float[4];

    public override float Duration => _duration;

    void Start()
    {
        _analogGlitch.enabled = true;
        _settings = new float[4] { _analogGlitch.scanLineJitter, _analogGlitch.verticalJump, _analogGlitch.horizontalShake, _analogGlitch.colorDrift };
    }

    public override void StartGlitch()
    {
        StartCoroutine(ActiveGlitch());
    }

    private IEnumerator ActiveGlitch()
    {
        _analogGlitch.scanLineJitter = 0.356f;
        _analogGlitch.verticalJump = 0.048f;
        _analogGlitch.horizontalShake = 0f;
        _analogGlitch.colorDrift = 0.152f;

        //_analogGlitch.enabled = true;
        yield return new WaitForSeconds(_duration);
        //_analogGlitch.enabled = false;
        _analogGlitch.scanLineJitter = _settings[0];
        _analogGlitch.verticalJump = _settings[1];
        _analogGlitch.horizontalShake = _settings[2];
        _analogGlitch.colorDrift = _settings[3];
    }
}
