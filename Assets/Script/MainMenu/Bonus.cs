using System; // Wajib tambah ini untuk DateTime
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public GameObject ScreenBonus;
    public GameObject ButtonBonus;
    
    private string cooldownKey = "LastBonusTime";
    private float cooldownDuration = 120f;

    void Start()
    {
        ScreenBonus.SetActive(false);
        CheckCooldown();
    }

    void Update() 
    {
        // Opsional: Cek terus menerus apakah sudah boleh muncul (jika scene tidak ganti)
        if (!ButtonBonus.activeSelf)
        {
            CheckCooldown();
        }
    }

    public void BonusCoin()
    {
        AdsManager.Instance.ShowRewarded(() =>
        {
            coinmanager.Instance.AddCoin(10);
            coinmanager.Instance.UpdateCoinUI();
            
            // Simpan waktu SEKARANG ke PlayerPrefs
            PlayerPrefs.SetString(cooldownKey, DateTime.Now.ToBinary().ToString());
            ButtonBonus.SetActive(false);
        });

        ScreenBonus.SetActive(true);
    }

    void CheckCooldown()
    {
        if (PlayerPrefs.HasKey(cooldownKey))
        {
            long lastTick = Convert.ToInt64(PlayerPrefs.GetString(cooldownKey));
            DateTime lastDate = DateTime.FromBinary(lastTick);
            TimeSpan difference = DateTime.Now - lastDate;

            if (difference.TotalSeconds < cooldownDuration)
            {
                ButtonBonus.SetActive(false);
                // Jalankan pemunculan otomatis setelah sisa waktu habis
                float remaining = cooldownDuration - (float)difference.TotalSeconds;
                Invoke("ShowButton", remaining);
            }
            else
            {
                ButtonBonus.SetActive(true);
            }
        }
    }

    void ShowButton() { ButtonBonus.SetActive(true); }

    public void Ok()
    {
        ScreenBonus.SetActive(false);
        AudioManager.instance.PlaybuttonClickSFX();
        AudioManager.instance.PlaypurchaseSFX();
    }
}