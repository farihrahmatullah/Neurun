using UnityEngine;
using TMPro;
using System.Collections;

public class PopupText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float floatSpeed = 40f;
    public float duration = 0.8f;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show(string msg, Color color)
    {
        text.text = msg;
        text.color = color;
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float t = 0;
        Vector3 startPos = transform.position;

        while (t < duration)
        {
            t += Time.deltaTime;

            // naik
            transform.position = startPos + Vector3.up * (t * floatSpeed);

            // fade out
            canvasGroup.alpha = 1 - (t / duration);

            // scale kecil
            transform.localScale = Vector3.Lerp(Vector3.one * 1.2f, Vector3.one, t / duration);

            yield return null;
        }

        Destroy(gameObject);
    }
}
