using System.Collections;
using UnityEngine;

public class CharacterRespawnEffect : MonoBehaviour
{
    public Karakter karakter;

    public SpriteRenderer[] renderers;
    public Collider2D[] colliders;
    public Rigidbody2D rb;
    private float originalGravity;


    private float fadeDuration = 0.3f;
    private int blinkCount = 6;

    void Awake()
    {
        if (karakter == null)
    karakter = GetComponent<Karakter>();


        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<SpriteRenderer>();

        if (colliders == null || colliders.Length == 0)
            colliders = GetComponentsInChildren<Collider2D>();

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        originalGravity = rb.gravityScale;
    }


    public void PlayRespawnEffect(System.Action onComplete = null)
    {
        StartCoroutine(RespawnRoutine(onComplete));
    }

    IEnumerator RespawnRoutine(System.Action onComplete)
    {
        karakter.isInvincible = true;

    for (int i = 0; i < blinkCount; i++)
    {
        yield return Fade(1f, 0f);
        yield return Fade(0f, 1f);
    }

    SetAlpha(1f);

    rb.gravityScale = originalGravity;

    karakter.isInvincible = false;

    onComplete?.Invoke();

    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            SetAlpha(a);
            yield return null;
        }

        SetAlpha(to);
    }

    void SetAlpha(float alpha)
    {
        foreach (var sr in renderers)
        {
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    void SetColliders(bool active)
    {
        foreach (var col in colliders)
        col.enabled = active;
    }

    public void SetInvisible()
    {
        SetAlpha(0f);
    }

}
