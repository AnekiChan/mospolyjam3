using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeTextOnTrigger : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float delayAfter = 2f;
    [SerializeField] private string fullText;

    [SerializeField] private Text text;
    private string currentText = "";

    private IEnumerator TypeText()
    {
        text.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);  // Постепенно берем символы от 0 до i
            text.text = currentText;                 // Присваиваем обновленный текст компоненту TextMesh
            yield return new WaitForSeconds(delay);       // Ждем заданное время перед следующей буквой
        }

        yield return new WaitForSeconds(delayAfter);
        text.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") StartCoroutine(TypeText());
    }
}
