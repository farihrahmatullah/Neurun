using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tombolMode;             // Tombol yang ingin diubah namanya
    public TMP_Text teksTombolMode;           // Text yang tampil di tombol
    public SpriteRenderer modeImage;         // Image yang berubah sprite-nya

    [Header("Camera & Screens")]
    public GameObject screenMainmenu;
    public GameObject screenMode;
    public camerazoom CameraZoom;

    [Header("Sprites per Mode")]
    public Sprite latinSprite;
    public Sprite inggrisSprite;
    public Sprite hiraganaSprite;
    public Sprite katakanaSprite;
    
    void Start()
    {
        // Set default mode pertama kali (Latin)
        if (GameManagerNeurun.SelectedMode == Mode.None || GameManagerNeurun.SelectedMode == 0)
        {
            GameManagerNeurun.SelectedMode = Mode.Latin;
        }

         UpdateHuruf();
    }


    public void PilihLatin()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        GameManagerNeurun.SelectedMode = Mode.Latin;
        screenMode.SetActive(false);
        screenMainmenu.SetActive(true);

        UpdateHuruf();
        CameraZoom.PlayZoomFrom3To5();
        // Debug.Log("Mode Latin");
    
    }

     public void PilihIngrris()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        GameManagerNeurun.SelectedMode = Mode.Inggris;
        screenMode.SetActive(false);
        screenMainmenu.SetActive(true);

        UpdateHuruf();
        CameraZoom.PlayZoomFrom3To5();
        // Debug.Log("Mode Latin");
    
    }

    public void PilihHiragana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        GameManagerNeurun.SelectedMode = Mode.Hiragana;
        screenMode.SetActive(false);
        screenMainmenu.SetActive(true);

         UpdateHuruf();
        CameraZoom.PlayZoomFrom3To5();

        // Debug.Log("Mode Hiragana");
        
    }

    public void PilihKatakana()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        GameManagerNeurun.SelectedMode = Mode.Katakana;
        screenMode.SetActive(false);
        screenMainmenu.SetActive(true);

        UpdateHuruf();
        CameraZoom.PlayZoomFrom3To5();
    
    // Debug.Log("Mode Katakana");
    }

    public void UpdateHuruf()
    {
         if (GameManagerNeurun.SelectedMode == Mode.Latin)
        {
             modeImage.sprite = latinSprite;
             teksTombolMode.text = "Latin";
        }

        if (GameManagerNeurun.SelectedMode == Mode.Inggris)
        {
             modeImage.sprite = inggrisSprite;
             teksTombolMode.text = "Inggris";
        }

        if (GameManagerNeurun.SelectedMode == Mode.Hiragana)
        {
             modeImage.sprite = hiraganaSprite;
             teksTombolMode.text = "Hiragana";
        }
        if (GameManagerNeurun.SelectedMode == Mode.Katakana)
        {
             modeImage.sprite = katakanaSprite;
             teksTombolMode.text = "Katakana";
        }
    }

}