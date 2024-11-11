using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    [SerializeField] private Image imageToFade;
    [SerializeField] private float targetAlpha = 1f;
    [SerializeField] private float fadeDuration = 2f;

    private void OnEnable()
    {
        if (imageToFade != null)
        {
            StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeIn()
    {
        float startAlpha = 0f;
        float elapsedTime = 0f;

        Color color = imageToFade.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            imageToFade.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

        imageToFade.color = new Color(color.r, color.g, color.b, targetAlpha);
    }
}
