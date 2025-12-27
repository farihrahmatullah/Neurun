using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoadingManager : MonoBehaviour
{
    [Header("Setup")]
    public string nextSceneName = "Gameplay";
    public LoadingUI ui;

    [Header("Loader Modules")]
    public List<MonoBehaviour> loaderComponents; // Tambahkan di inspector: LoadingAssets, LoadingJson, dll

    private List<ILoader> loaders = new List<ILoader>();

    private IEnumerator Start()
    {
        // Konversi semua ke interface ILoader
        foreach (var component in loaderComponents)
        {
            if (component is ILoader loader)
                loaders.Add(loader);
        }

        // Jalankan satu per satu loader
        foreach (var loader in loaders)
        {
            yield return StartCoroutine(loader.Load());
        }

        // Setelah semua loader selesai
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(nextSceneName);
    }

    private void Update()
    {
        // Hitung rata-rata progress semua loader
        if (loaders.Count > 0)
        {
            float totalProgress = 0f;
            string currentMessage = "";
            foreach (var loader in loaders)
            {
                totalProgress += loader.GetProgress();
                currentMessage = loader.GetDescription();
            }
            float average = totalProgress / loaders.Count;
            ui.UpdateProgress(average, currentMessage);
        }
    }
}
