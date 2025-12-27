using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class splashLogo : MonoBehaviour
{
    public Image logoImage;
    public float fadeInDuration = 1.5f;
    public float stayDuration = 2f;
    public float fadeOutDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // Set awal transparan
        Color logoColor = logoImage.color;
        logoColor.a = 0;
        logoImage.color = logoColor;

        // Fade In
        float timer = 0;
        while (timer <= fadeInDuration)
        {
            logoColor.a = Mathf.Lerp(0, 1, timer / fadeInDuration);
            logoImage.color = logoColor;
            timer += Time.deltaTime;
            yield return null;
        }

        // Pastikan alpha penuh
        logoColor.a = 1;
        logoImage.color = logoColor;

        // Tunggu sebentar
        yield return new WaitForSeconds(stayDuration);

        // Fade Out
        timer = 0;
        while (timer <= fadeOutDuration)
        {
            logoColor.a = Mathf.Lerp(1, 0, timer / fadeOutDuration);
            logoImage.color = logoColor;
            timer += Time.deltaTime;
            yield return null;
        }

        // Pastikan alpha 0
        logoColor.a = 0;
        logoImage.color = logoColor;

        // Setelah selesai, pindah scene atau hapus logo
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
