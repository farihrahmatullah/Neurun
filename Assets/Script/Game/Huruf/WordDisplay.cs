using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WordDisplay : MonoBehaviour
{
    [Header("Referensi")]
    public TMP_Text targetWordText;
    public TMP_Text nextWordText; // tambahkan ini
    public TMP_Text WordTextPicked;
    public GamePlay gameplay;
    public WordProgressManager wordProgress;


    private WordData DisplayWordData;
    private int actualLength; // panjang huruf asli (hiragana/katakana/latin)

    void Start()
    {
        if (wordProgress == null)
            wordProgress = FindObjectOfType<WordProgressManager>();

        DisplayWordData = gameplay.currentWord;
        DisplayWord(DisplayWordData);

        if (wordProgress != null)
            wordProgress.OnLetterCollected += OnLetterCollected;
    }

    private void OnDestroy()
    {
        if (wordProgress != null)
            wordProgress.OnLetterCollected -= OnLetterCollected;
    }

    public void DisplayWord(WordData wordData)
    {
        if (wordData == null) return;

        Mode currentMode = GameManagerNeurun.SelectedMode;

        // default nilai awal
        string baseWord = wordData.word;
        string displayLatin = wordData.word;

        switch (currentMode)
        {
            case Mode.Hiragana:
            case Mode.Katakana:
                baseWord = wordData.word;          // kata asli kana
                displayLatin = wordData.latin ?? wordData.word; // tampilan latin
                break;
            case Mode.Latin:
            default:
                baseWord = wordData.word;
                displayLatin = wordData.word;
                break;
        }

        // simpan panjang kata asli (hiragana bisa 2, katakana bisa 3, dll)
        actualLength = baseWord.Length;

        // tampilkan underscore sesuai panjang kata asli
        string underscores = new string('_', actualLength);
        WordTextPicked.text = underscores;

        // tampilkan teks petunjuk (latin) di target word
        targetWordText.text = displayLatin;

            // ==========================
        //   KATA KEDUA (PREVIEW)
        // ==========================
        if (gameplay.poolGenerator.wordPool.Count > 1)
        {
            WordData nextWord = gameplay.poolGenerator.wordPool[1];
            string nextWordDisplay = nextWord.word;

            switch (currentMode)
            {
                case Mode.Hiragana:
                case Mode.Katakana:
                    nextWordDisplay = nextWord.latin ?? nextWord.word;
                    break;
            }

            nextWordText.text = nextWordDisplay;
        }
        else
        {
            nextWordText.text = "";
        }
    }

    private void OnLetterCollected(char letter)
    {
        string collected = wordProgress.GetCollectedWord();
        WordTextPicked.text = GenerateProgressText(collected, actualLength);
    }

    private string GenerateProgressText(string collected, int totalLength)
    {
        char[] display = new char[totalLength];
        for (int i = 0; i < totalLength; i++)
        {
            display[i] = (i < collected.Length) ? collected[i] : '_';
        }
        return new string(display);
    }

    IEnumerator ResetForNewWord(WordData newWord)
    {
        yield return new WaitForSeconds(1f);
        if (wordProgress != null)
            wordProgress.ResetProgress();

        DisplayWordData = newWord;
        DisplayWord(DisplayWordData);
    }
}
