using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject screenmode;
    public GameObject screenMainmenu;
    public GameObject screenIngatan;
    public GameObject screenPrestasi;
    public GameObject screenSettings;
    public GameObject screenHowtoplay;
    void Start()
    {
        screenMainmenu.SetActive(true);
        screenmode.SetActive(false);
        screenIngatan.SetActive(false);
        screenPrestasi.SetActive(false);
        screenSettings.SetActive(false);
        screenHowtoplay.SetActive(false);
    }

    public void Play()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        SceneManager.LoadScene(2);
    }

    public void Silang()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        screenMainmenu.SetActive(true);
        screenmode.SetActive(false);
        screenPrestasi.SetActive(false);
        screenIngatan.SetActive(false);
        screenHowtoplay.SetActive(false);
        screenSettings.SetActive(false);
        
    }

    // public void Exit()
    // {
    //     Application.Quit();
    // }

    public void ModeKata()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        screenmode.SetActive(true);
        screenMainmenu.SetActive(false);
    }

    public void Ingatan()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        screenIngatan.SetActive(true);
        screenMainmenu.SetActive(false);
    }

    public void Prestasi()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        screenPrestasi.SetActive(true);
        screenMainmenu.SetActive(false);
    }

    public void info()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        screenHowtoplay.SetActive(true);
        screenMainmenu.SetActive(false);
    }

    public void Settings()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        screenSettings.SetActive(true);
        screenMainmenu.SetActive(false);
    }

    public void Skin()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        SceneManager.LoadScene(4);
    }
     
}
