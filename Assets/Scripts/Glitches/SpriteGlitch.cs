using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGlitch : MonoBehaviour
{
    [SerializeField] private Material _glitchMaterial;
    [SerializeField] private float _minDuration = 0.5f;
    [SerializeField] private float _maxDuration = 2f;

    [SerializeField] private float _minTimer = 10f;
    [SerializeField] private float _maxTimer = 20f;

    private SpriteRenderer _spriteRenderer;
    private Material _originalMaterial;

    void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
        StartCoroutine(GlitchCoroutine());
    }

    private IEnumerator GlitchCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minTimer, _maxTimer));
            float duration = Random.Range(_minDuration, _maxDuration);
            _spriteRenderer.material = _glitchMaterial;
            yield return new WaitForSeconds(duration);
            _spriteRenderer.material = _originalMaterial;
        }
    }
}
