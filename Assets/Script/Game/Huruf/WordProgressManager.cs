using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mengatur logika kata aktif: huruf yang diambil, hasil benar/salah, dan progres huruf.
/// Tidak berurusan dengan UI, score, atau notifikasi.
/// </summary>
public class WordProgressManager : MonoBehaviour
{
    public event Action<char> OnLetterCollected; // 👉 event baru

    [Header("Hubungan Dengan Script Lain")]
    public GamePlay gameplay;
    public PointManager pointManager;

    public string TargetWord { get; private set; }

    // huruf yang sudah diambil player
    public List<char> collectedLetters = new List<char>();
    public event System.Action<bool> OnLetterChecked;


    // menghitung berapa kali huruf tertentu berhasil diambil dengan benar
    private Dictionary<char, int> correctLetterCount = new Dictionary<char, int>();

    void Start()
    {
        StartCoroutine(LoadNewWord());
    }
    /// <summary>
    /// Ambil kata baru dari KataLoader dan reset progres.
    /// </summary>

    public void StartLoadNewWord()
    {
        StartCoroutine(LoadNewWord());
    }
    
    IEnumerator LoadNewWord()
    {
        yield return new WaitForSeconds(2f);
        collectedLetters.Clear();
        correctLetterCount.Clear();
        TargetWord = gameplay.currentWord.word.ToUpper();
    }

    /// <summary>
    /// Menambahkan huruf yang diambil pemain ke progres.
    /// </summary>
    public void AddLetter(char letter)
    {
        letter = char.ToUpper(letter);
        // Debug.Log($"huruf diambil {letter}");

        if (string.IsNullOrEmpty(TargetWord))
        {
            // Debug.LogWarning("Belum ada kata yang dimuat di WordProgressManager.");
            return;
        }

        if (collectedLetters.Count >= TargetWord.Length)
            return;

        int index = collectedLetters.Count;
        collectedLetters.Add(letter);

        // Panggil event setiap kali huruf dikumpulkan
        OnLetterCollected?.Invoke(letter);

        if (index < TargetWord.Length && letter == TargetWord[index])
        {
            if (!correctLetterCount.ContainsKey(letter))
                correctLetterCount[letter] = 0;

            correctLetterCount[letter]++;

            OnLetterChecked?.Invoke(true);  // ← huruf benar
        }
        else
        {
            OnLetterChecked?.Invoke(false); // ← huruf salah
        }


        
    }


    /// <summary>
    /// Mengambil berapa kali huruf tertentu sudah dikumpulkan dengan benar.
    /// </summary>
    public int GetCollectedCount(char letter)
    {
        return correctLetterCount.ContainsKey(letter) ? correctLetterCount[letter] : 0;
    }

    public string GetCollectedWord()
    {
        return new string(collectedLetters.ToArray());
    }
    /// <summary>
    /// Reset progres huruf tanpa ganti kata.
    /// </summary>
    public void ResetProgress()
    {
        collectedLetters.Clear();
        correctLetterCount.Clear();
    }

    /// <summary>
    /// Mengembalikan salinan daftar huruf yang sudah dikumpulkan.
    /// </summary>
    public List<char> GetCollectedLetters()
    {
        return new List<char>(collectedLetters);
    }
}
