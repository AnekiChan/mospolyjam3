using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    private Text text;
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private string[] textArray;
    private int currentIndex = 0;

    private string currentText = "";
    private bool _isTyping = false;

    private void Start()
    {
        text = GetComponent<Text>();
        //fullText = text.text;
        StartCoroutine(TypeText());
    }

    void Update()
    {
        if (!_isTyping && Input.GetMouseButtonDown(0))
        {
            if (currentIndex < textArray.Length)
            {
                StartCoroutine(TypeText());
            }
            else
            {
                CommonEvents.Instance.OnFirstTextEnded.Invoke();
            }
        }
    }

    private IEnumerator TypeText()
    {
        _isTyping = true;
        text.text = "";
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < textArray[currentIndex].Length; i++)
        {
            currentText = textArray[currentIndex].Substring(0, i + 1);
            text.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        currentIndex++;
        _isTyping = false;
    }
}
