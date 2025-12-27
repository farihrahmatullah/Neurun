using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadingJSON : MonoBehaviour, ILoader
{
    public static LoadingJSON Instance { get; private set; }
    public List<WordData> AllWords { get; private set; }

    private WordList cachedWordList;
    private Mode currentMode;
    private float progress;
    private string description;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Load()
    {
        description = "Memuat data kata...";
        progress = 0f;

        yield return new WaitForSeconds(0.2f); // simulasi delay

        LoadWords(GameManagerNeurun.SelectedMode);
        progress = 1f;
        description = "Data kata berhasil dimuat.";
    }

    public float GetProgress() => progress;

    public string GetDescription() => description;

    public void LoadWords(Mode mode)
    {
        ClearAllWords();
        currentMode = mode;

        string fileName = GetJsonFileName(mode);
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);

        if (jsonFile == null)
        {
            // Debug.LogError($"File JSON '{fileName}' tidak ditemukan di Resources!");
            return;
        }

        cachedWordList = JsonUtility.FromJson<WordList>(jsonFile.text);
        AllWords = cachedWordList.words; // ✅ tambahkan ini

        Debug.Log($"Berhasil memuat {AllWords.Count} kata dari {fileName}");
    }

    public WordList GetAllWords() => cachedWordList;

    public Mode GetCurrentMode() => currentMode;

    private string GetJsonFileName(Mode mode)
    {
        switch (mode)
        {
            case Mode.Latin: return "Kata";
            case Mode.Inggris: return "KataInggris";
            case Mode.Hiragana: return "KataHRGN";
            case Mode.Katakana: return "KataKTKN";
            default: return "Kata";
        }
    }

    public void ClearAllWords()
    {
        if (AllWords != null)
        {
            AllWords.Clear();
            AllWords = null;
        }

        cachedWordList = null;
        // Optional: reset info lainnya kalau mau benar-benar kosong
        progress = 0f;
        description = "Data kata dibersihkan.";
    }

}
