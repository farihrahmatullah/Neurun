using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class adsinterstitial : MonoBehaviour
{
    public static adsinterstitial Instance;
    [SerializeField] private int jumlahgameovernow = 0;

    

    public void Awake()
    {
        if( Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Tambahjumlahgameover()
    {
        jumlahgameovernow++;

        if (jumlahgameovernow == 3)
        {
            jumlahgameovernow = 0;
            AdsManager.Instance.ShowInterstitial();
        }
    }
}
