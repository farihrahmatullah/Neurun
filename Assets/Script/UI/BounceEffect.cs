using UnityEngine;
using System.Collections;

public class BounceEffect : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float scaleAmount = 0.2f;       // seberapa besar efek melompat
    public float bounceDuration = 0.4f;   // waktu naik-turun
    public AnimationCurve bounceCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public bool playOnStart = true;        // otomatis jalan saat start?

    private Vector3 originalScale;
    private Coroutine bounceRoutine;

    void Start()
    {
        originalScale = transform.localScale;
        if (playOnStart)
            Invoke("PlayBounce", 2);
    }

    /// <summary>
    /// Panggil efek bounce dari script lain.
    /// </summary>
    public void PlayBounce()
    {
        if (bounceRoutine != null)
            StopCoroutine(bounceRoutine);
        bounceRoutine = StartCoroutine(BounceOnce());
    }

    IEnumerator BounceOnce()
    {
        Vector3 targetScale = originalScale * scaleAmount;
        float elapsed = 0f;
        float half = bounceDuration * 0.5f;

        // naik
        while (elapsed < half)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / half;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, bounceCurve.Evaluate(t));
            yield return null;
        }

        elapsed = 0f;
        // turun
        while (elapsed < half)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / half;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, bounceCurve.Evaluate(t));
            yield return null;
        }

        transform.localScale = originalScale;
        bounceRoutine = null;
    }
}
