using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public List<SkinData> skinList;
    public GameObject prevButton;
    public GameObject nextButton;
    public GameObject priceButton;
    public GameObject pasangButton;
    public GameObject ConfirmationPanel;


    public TMP_Text Namaskin;
    public TMP_Text Hargaskin;
    public TMP_Text Hargaskin2;
    public TMP_Text ButtonPasang;

    public int index = 0;

    void Start()
    {
        ConfirmationPanel.SetActive(false);
        // Mengambil referensi list dari Singleton
        skinList = Skinmanager.Instance.skinList;

        index = Skinmanager.Instance.idSkinTerpilih;
        UpdateDisplay(); // Tampilkan data pertama kali saat start
    }

    // Fungsi pusat untuk update UI Nama, Harga, dan Tombol
    void UpdateDisplay()
    {

        // 1. Update Teks Nama dan Harga dari List berdasarkan Index
        if (skinList.Count > 0)
        {
            Namaskin.text = skinList[index].skinName;
            Hargaskin.text = skinList[index].hargaSkin.ToString();
            Hargaskin2.text = skinList[index].hargaSkin.ToString();
        }

        if(skinList[index].unlockedByDefault == true)
        {
            priceButton.SetActive(false);
            pasangButton.SetActive(true);

        }
        else
        {
            priceButton.SetActive(true);
            pasangButton.SetActive(false);
        }

        if(Skinmanager.Instance.idSkinTerpilih == index)
        {
            ButtonPasang.text = "Terpasang";
        }
        else
        {
            ButtonPasang.text = "Pasang";
        }

        // 2. Logika Tombol (Auto On/Off)
        prevButton.SetActive(index > 0);
        nextButton.SetActive(index < skinList.Count - 1);
    }

    public void PrevSkin()
    {
        if (index > 0)
        {
            AudioManager.instance.PlaybuttonClickSFX();
            index--;
            Skinmanager.Instance.ApplySkinByID(index);
            UpdateDisplay(); // Update UI setelah index berubah
        }
    }

    public void NextSkin()
    {
        if (index < skinList.Count - 1)
        {
            AudioManager.instance.PlaybuttonClickSFX();
            index++;
            Skinmanager.Instance.ApplySkinByID(index);
            UpdateDisplay(); // Update UI setelah index berubah
        }
    }

    public void PasangSkin()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        Skinmanager.Instance.idSkinTerpilih = index;
        PlayerPrefs.SetInt("SelectedSkinID", index);
        PlayerPrefs.Save();
        UpdateDisplay();
        ButtonPasang.text = "Terpasang";
    }

    public void ButtonPrice()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ConfirmationPanel.SetActive(true);
    }

    public void CloseConfirmationPanel()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ConfirmationPanel.SetActive(false);
    }

    public void BeliSkin()
    {
        AudioManager.instance.PlaybuttonClickSFX();

        bool beliskin = coinmanager.Instance.SubtractCoin(skinList[index].hargaSkin);

        if (!beliskin)
        {
            Debug.Log("Tidak cukup koin untuk melanjutkan!");
            ConfirmationPanel.SetActive(false);
            return;
        }


        skinList[index].unlockedByDefault = true;
        AudioManager.instance.PlaypurchaseSFX();
        ConfirmationPanel.SetActive(false);
        UpdateDisplay();
    }

    public void Skin()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        SceneManager.LoadScene(1);
    }
}