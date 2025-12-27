using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Bertanggung jawab hanya untuk menentukan huruf mana yang akan muncul,
/// baik huruf target maupun huruf decoy (palsu).
/// Tidak mengatur posisi spawn, pooling, atau timing.
/// </summary>
public class LetterSelector : MonoBehaviour
{
    [Header("Dependencies")]
    public GamePlay gameplay;  // sumber kata yang sedang dimainkan
    public WordProgressManager wordprogressmanager;       // untuk mengecek huruf mana yang sudah dikumpulkan

    [Header("Settings")]
    public Mode currentMode;            // Mode bahasa (Latin, Hiragana, Katakana)
    public int decoyCount = 1;          // jumlah huruf palsu yang akan ikut muncul

    private string cachedAlphabet;      // huruf-huruf sesuai mode
    private int currentIndex = 0;       // pointer untuk tracking huruf target terakhir

    private readonly List<char> decoyLetters = new List<char>(4);
    private readonly HashSet<char> usedLetters = new HashSet<char>();

    void Start()
    {
        currentMode = GameManagerNeurun.SelectedMode;
        cachedAlphabet = GetAlphabetForMode(currentMode);
    }

    /// <summary>
    /// Mengambil kombinasi huruf yang akan muncul (1 huruf benar + decoy).
    /// </summary>
    public List<char> GetLettersForSpawn()
    {
        string word = gameplay.currentWord.word;

        if (string.IsNullOrEmpty(word))
            return null;

        char target = GetNextTargetLetter(word);
        if (target == '\0')
            return null;

        decoyLetters.Clear();

        // Ambil huruf decoy acak
        while (decoyLetters.Count < decoyCount)
        {
            char decoy = cachedAlphabet[Random.Range(0, cachedAlphabet.Length)];
            if (decoy != target && !decoyLetters.Contains(decoy) && !word.Contains(decoy))
                decoyLetters.Add(decoy);
        }

        // Gabungkan huruf target + decoy
        List<char> result = new List<char>(decoyLetters);
        int insertIndex = Random.Range(0, decoyLetters.Count + 1);
        result.Insert(insertIndex, target);
        

        return result;
    }

    /// <summary>
    /// Menentukan huruf berikutnya dari kata yang belum terkumpul.
    /// </summary>
    private char GetNextTargetLetter(string word)
    {
        for (int i = 0; i < word.Length; i++)
        {
            int checkIndex = (currentIndex + i) % word.Length;
            char c = word[checkIndex];

            int totalCount = 0;
            foreach (char ch in word)
                if (ch == c) totalCount++;

            int collectedCount = wordprogressmanager.GetCollectedCount(c);

            if (collectedCount < totalCount)
            {
                currentIndex = checkIndex + 1;
                usedLetters.Add(c);
                return c;
            }
        }

        return '\0'; // semua huruf sudah terkumpul
    }

    /// <summary>
    /// Mengembalikan string alphabet berdasarkan mode bahasa.
    /// </summary>
    private string GetAlphabetForMode(Mode mode)
    {
        switch (mode)
        {
            case Mode.Latin:
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            case Mode.Hiragana:
                return "あいうえおかきくけこがぎぐげござしすせそざじずぜぞたちつてとだぢづでどなにぬねのはひふへほばびぶべぼぱぴぷぺぽまみむめもやゆよらりるれろわをん";
            case Mode.Katakana:
                return "アイウエオカキクケコガギグゲゴサシスセソザジズゼゾタチツテトダヂヅデドナニヌネノハヒフヘホバビブベボパピプペポマミムメモヤユヨラリルレロワヲン";
            default:
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }
    }
}
