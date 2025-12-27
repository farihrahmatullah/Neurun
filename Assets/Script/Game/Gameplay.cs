using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    public WordPoolGenerator poolGenerator;
    public WordProgressManager progressManager;
    public WordDisplay worddisplay;
    public WordData currentWord;
    public Karakter karakter;
    public coingameplay coinGameplay;

    [Header("UI Game Over")]
    public GameObject GameOverUI;

    [Header("Transition Animator")]
    public WordTransitionAnimator transitionAnimator; // <--- Tambahan

    [Header("Variabel")]
    public bool karakterlife;

    private void Start()
    {
        GameOverUI.SetActive(false);
        karakterlife = true;
        poolGenerator.GeneratePool();
        LoadNextWord();
    }

    public void LoadNextWord()
    {
        if (transitionAnimator != null)
        {
            // animasi keluar dulu baru lanjut ganti kata
            transitionAnimator.PlayOut(() => StartCoroutine(DoLoadNextWord()));
        }
        else
        {
            StartCoroutine(DoLoadNextWord());
        }
    }

    private IEnumerator DoLoadNextWord()
    {
        yield return new WaitForSeconds(0.1f);

        currentWord = poolGenerator.wordPool[0];

        progressManager.StartLoadNewWord();
        worddisplay.DisplayWord(currentWord);

        if (transitionAnimator != null)
        {
            transitionAnimator.PlayIn();
            AudioManager.instance.PlayswitchwordSFX();
        }

        Invoke(nameof(RemoveCurrentWord), 1f);
    }

    private void RemoveCurrentWord()
    {
        poolGenerator.RemoveWord(currentWord);
    }

    public void NextWord() => LoadNextWord();

    public void GameOver()
    {
        karakterlife = false;
        GameOverUI.SetActive(true);
        PlayerPrefs.Save();
        adsinterstitial.Instance.Tambahjumlahgameover();
        coinGameplay.Savecoin();
    }

    public void BackMainMenu()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        SceneManager.LoadScene(1);
        poolGenerator.wordPool.Clear();

        if (LoadingJSON.Instance != null)
        {
            Destroy(LoadingJSON.Instance.gameObject);
        }

    }

    public void Continue()
    {
        bool continuegame = coinmanager.Instance.SubtractCoin(30);

        if (!continuegame)
        {
            Debug.Log("Tidak cukup koin untuk melanjutkan!");
            return;
        }

        AudioManager.instance.PlaypurchaseSFX();

        karakterlife = true;
        GameOverUI.SetActive(false);
        AudioManager.instance.PlaybuttonClickSFX();
        karakter.Continue();
        coinGameplay.Resetcoin();
    }

    public void Again()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        SceneManager.LoadScene(2);
    }
}
