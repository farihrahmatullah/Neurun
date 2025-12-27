using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using TMPro;

public class LoadingAssets : MonoBehaviour, ILoader
{
    [Header("Preload Assets")]
    public List<SpriteAtlas> spriteAtlasesToPreload;
    public List<TMP_FontAsset> fontsToPreload;
    public List<GameObject> prefabsToPreload;
    public List<AudioClip> audioToPreload;

    private float progress;

    public IEnumerator Load()
    {
        progress = 0f;

        int total = spriteAtlasesToPreload.Count + fontsToPreload.Count + prefabsToPreload.Count + audioToPreload.Count;
        int done = 0;

        // SpriteAtlas
        foreach (var atlas in spriteAtlasesToPreload)
        {
            if (atlas != null)
            {
                Sprite[] sprites = new Sprite[atlas.spriteCount];
                atlas.GetSprites(sprites);
            }
            done++;
            progress = (float)done / total;
            yield return null;
        }

        // Fonts
        foreach (var font in fontsToPreload)
        {
            if (font != null)
            {
                var tmpObj = new GameObject("TMPFontPreload");
                var tmp = tmpObj.AddComponent<TextMeshProUGUI>();
                tmp.font = font;
                tmp.text = " ";
                Destroy(tmpObj);
            }
            done++;
            progress = (float)done / total;
            yield return null;
        }

        // Prefabs
        foreach (var prefab in prefabsToPreload)
        {
            if (prefab != null)
            {
                GameObject temp = Instantiate(prefab);
                temp.SetActive(false);
                Destroy(temp);
            }
            done++;
            progress = (float)done / total;
            yield return null;
        }

        // Audio
        foreach (var clip in audioToPreload)
        {
            if (clip != null)
                AudioSource.PlayClipAtPoint(clip, Vector3.zero, 0f);
            done++;
            progress = (float)done / total;
            yield return null;
        }
    }

    public float GetProgress() => progress;
    public string GetDescription() => "Memuat aset game...";
}
