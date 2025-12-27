using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GetHuruf : MonoBehaviour
{
    [Header("Setup Huruf")]
    public char letternow;
    public SpriteRenderer spriteRenderer;

    [Header("Sumber Sprite")]
    public AtlasSpriteData[] spriteAtlases; // semua atlas (Latin, Hiragana, Katakana, dll)

    [Header("Dependencies")]
    public WordProgressManager wordProgress;

    private void Awake()
    {
        if (wordProgress == null)
            wordProgress = FindObjectOfType<WordProgressManager>();

        // Optional fallback: cari satu atlas di scene kalau array kosong
        if (spriteAtlases == null || spriteAtlases.Length == 0)
            spriteAtlases = FindObjectsOfType<AtlasSpriteData>();
    }

    public void SetLetter(char letter)
    {
        letternow = letter;

        AtlasSpriteData chosenAtlas = GetAtlasForLetter(letter);
        if (chosenAtlas == null)
        {
            return;
        }

        Sprite newSprite = chosenAtlas.GetSpriteForLetter(letter);
        if (newSprite != null)
            spriteRenderer.sprite = newSprite;
    }

    /// <summary>
    /// Mencari atlas yang sesuai dengan huruf berdasarkan rentang unicode.
    /// </summary>
    private AtlasSpriteData GetAtlasForLetter(char letter)
    {
        foreach (var atlas in spriteAtlases)
        {
            if (atlas == null) continue;

            // Cek apakah atlas ini punya sprite untuk huruf tsb
            Sprite s = atlas.GetSpriteForLetter(letter);
            if (s != null)
                return atlas;
        }

        return null; // tidak ada yang cocok
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("Player"))
            return;

        Karakter player = collision.GetComponent<Karakter>();
        if (player != null && player.isInvincible)
            return;

        if (wordProgress == null)
            return;

        wordProgress.AddLetter(letternow);
        gameObject.SetActive(false);
    }
}
