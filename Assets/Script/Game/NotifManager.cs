using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class NotifManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject NotifTrue;
    public GameObject NotifFalse;
    public GameObject NotifArti;

    public TMP_Text TrueText;
    public TMP_Text FalseText;
    public TMP_Text ArtiKataText;
    public TMP_Text ArtiMeaningText;

    [Header("Data Settings")]
    public TextAsset kataInggrisJson;
    public TextAsset kataHiraganaJson;
    public TextAsset kataKatakanaJson;

    private WordList wordList;

    void Start()
    {
        // Nonaktifkan semua notif di awal
        NotifTrue.SetActive(false);
        NotifFalse.SetActive(false);
        NotifArti.SetActive(false);

        // Muat data JSON sesuai mode yang aktif dari GameManagerNeurun
        LoadWordList();
    }

    private void LoadWordList()
    {
        TextAsset selectedJson = null;

        switch (GameManagerNeurun.SelectedMode)
        {
            case Mode.Inggris:
                selectedJson = kataInggrisJson;
                break;
            case Mode.Hiragana:
                selectedJson = kataHiraganaJson;
                break;

            case Mode.Katakana:
                selectedJson = kataKatakanaJson;
                break;

            default:
                return;
        }

        if (selectedJson != null)
        {
            wordList = JsonUtility.FromJson<WordList>(selectedJson.text);
        }
    }

    public void TrueWord(string latinWord)
    {
        StartCoroutine(ShowTrueThenArti(latinWord));
    }

    public void FalseWord(string latinWord)
    {
        NotifFalse.SetActive(true);
        AudioManager.instance.PlayUiSFX();
        FalseText.text = latinWord;
        StartCoroutine(HideNotif(NotifFalse));
    }

    private IEnumerator ShowTrueThenArti(string latinWord)
    {
        // --- Munculkan notif benar dulu ---
        NotifTrue.SetActive(true);
        AudioManager.instance.PlayUiSFX();
        TrueText.text = latinWord;
        yield return new WaitForSeconds(2f);
        NotifTrue.SetActive(false);

        // Tampilkan arti hanya jika mode mendukung & data tersedia
        if (wordList != null)
        {
            ShowArti(latinWord);
        }
    }

    private void ShowArti(string latinWord)
    {
        Debug.Log("Sampe show Arti gak si" +latinWord );
        var data = wordList.words.Find(w => w.word.ToUpper() == latinWord.ToUpper());
        if (data != null)
        {
                if (GameManagerNeurun.SelectedMode == Mode.Inggris)
            {
                // Inggris: latin = word
                ArtiKataText.text = data.word;
            }
            else
            {
                // Jepang: latin = huruf jepang
                ArtiKataText.text = data.latin;
            }

            ArtiMeaningText.text = data.arti.ToUpper(); // arti kata dalam bahasa Indonesia
            NotifArti.SetActive(true);
            AudioManager.instance.PlayUiSFX();
            StartCoroutine(HideNotif(NotifArti, 3f));
        }

    }

    private IEnumerator HideNotif(GameObject notif, float delay = 2f)
    {
        yield return new WaitForSeconds(delay);
        notif.SetActive(false);
    }
}
