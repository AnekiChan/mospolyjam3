using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCameraRotateGlitch : VisualGlitch
{
    [SerializeField] private float _tiltAngle = 10f;
    [SerializeField] private float _tiltDuration = 1f;

    private Quaternion _originalRotation;
    private Camera _camera;

    public override float Duration => _tiltDuration;

    private void Start()
    {
        _camera = Camera.main;
        _originalRotation = _camera.transform.rotation;
    }

    public override void StartGlitch()
    {
        StartCoroutine(TiltCoroutine());
    }

    private IEnumerator TiltCoroutine()
    {
        _camera.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-_tiltAngle, _tiltAngle));
        yield return new WaitForSeconds(_tiltDuration);
        _camera.transform.rotation = _originalRotation;
    }
}
