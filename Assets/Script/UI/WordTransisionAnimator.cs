using System.Collections;
using UnityEngine;

public class WordTransitionAnimator : MonoBehaviour
{
    public CanvasGroup wordGroup;
    public float fadeDuration = 0.4f;
    public AnimationCurve fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine currentRoutine;

    void Awake()
    {
        if (wordGroup == null)
            wordGroup = GetComponent<CanvasGroup>();
    }

    public void PlayIn()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(Fade(0f, 1f));
    }

    public void PlayOut(System.Action onComplete = null)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(Fade(1f, 0f, onComplete));
    }

    private IEnumerator Fade(float from, float to, System.Action onComplete = null)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            float curveT = fadeCurve.Evaluate(t);
            wordGroup.alpha = Mathf.Lerp(from, to, curveT);
            yield return null;
        }

        wordGroup.alpha = to;
        onComplete?.Invoke();
    }
}
