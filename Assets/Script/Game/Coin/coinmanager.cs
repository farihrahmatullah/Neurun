using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class coinmanager : MonoBehaviour
{
    public PlayerData data;
    private string filePath;

    public TMP_Text coinText;

    
    public static coinmanager Instance { get; private set; }

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
        // Tentukan lokasi file: dataPlayer.json
        filePath = Application.persistentDataPath + "/dataPlayer.json";
        
        // Langsung muat data saat game mulai
        LoadCoin();
    }

    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        data.totalcoins += amount;
        SaveCoin(); // Simpan setiap kali koin bertambah
    }

    public bool SubtractCoin(int amount)
    {
        if(data.totalcoins < amount)
        {
            Debug.Log("Tidak cukup koin!");
            return false;
        }
        else
        {
            data.totalcoins -= amount;
        }

        if (data.totalcoins < 0)
        {
            data.totalcoins = 0; // Pastikan koin tidak negatif
        }
        
        SaveCoin(); // Simpan setiap kali koin berkurang

        return true;
   
    }

    public void SaveCoin()
    {
        // Ubah class GameData menjadi string JSON
        string json = JsonUtility.ToJson(data, true);
        
        // Tulis string tersebut ke file fisik
        File.WriteAllText(filePath, json);
        Debug.Log("Koin disimpan! Total: " + data.totalcoins);
    }

    public void UpdateCoinUI()
    {
        coinText = GameObject.Find("cointext").GetComponent<TMP_Text>();
        if (coinText != null)
        {
            coinText.text = data.totalcoins.ToString();
        }
        else
        {
            Debug.LogWarning("CoinText UI element not found!");
        }
    }

    public void LoadCoin()
    {
        if (File.Exists(filePath))
        {
            // Baca teks dari file
            string json = File.ReadAllText(filePath);
            
            // Masukkan teks JSON ke dalam objek data
            data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            // Jika file tidak ada, buat data baru
            data = new PlayerData();
            SaveCoin();
        }

        UpdateCoinUI();
    }

    public void ResetCoin()
    {
        data.totalcoins = 0;
        SaveCoin();
    }
}
