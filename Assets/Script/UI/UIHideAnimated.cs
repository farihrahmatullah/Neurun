using System.Collections;
using UnityEngine;

public class UIHideAnimated : MonoBehaviour
{
    public RectTransform uiElement;
    public float duration = 0.8f;
    public Vector2 offsetEnd = new Vector2(0, -300);
    public float delay = 0f;

    private Vector2 originalPosition;
    private Coroutine animRoutine;

    void Awake()
    {
        if (uiElement == null)
            uiElement = GetComponent<RectTransform>();

        originalPosition = uiElement.anchoredPosition;
    }

    public void PlayHideAnimation()
    {
        if (animRoutine != null)
            StopCoroutine(animRoutine);

        animRoutine = StartCoroutine(AnimateOutAndDisable());
    }

    private IEnumerator AnimateOutAndDisable()
    {
        yield return new WaitForSeconds(delay);

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            t = Mathf.Sin(t * Mathf.PI * 0.5f); // ease-out
            uiElement.anchoredPosition = Vector2.Lerp(originalPosition, originalPosition + offsetEnd, t);
            yield return null;
        }

        uiElement.anchoredPosition = originalPosition + offsetEnd;
    }
}
