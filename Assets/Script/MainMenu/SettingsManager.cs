using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Image musicImage;
    public Image sfxImage;
    public Image UIButtonImage;
    

    public GameObject ScreenCredit;
    public GameObject ScreenConfirm;

    [Header("Custom Colors")]
    public Color onColor = Color.white;
    public Color offColor = Color.gray;

    private void Start()
    {
        ScreenCredit.SetActive(false);
        ScreenConfirm.SetActive(false);
        // sinkron awal
        UpdateButtonMusicText();
        UpdateButtonSFXText();
        UpdateButtonUIText();
    }

    public void OnMusicButtonPressed()
    {
        AudioManager.instance.ToggleMusic();
        UpdateButtonMusicText();
    }

    public void OnSFXButtonPressed()
    {
        AudioManager.instance.ToggleSFX();
        UpdateButtonSFXText();
    }

    public void OnUIButtonPressed()
    {
        AudioManager.instance.ToggleUIButton();
        UpdateButtonUIText();
    }

    public void UpdateButtonMusicText()
    {
        if (AudioManager.instance == null) return;
        musicImage.color = AudioManager.instance.IsMusicOn() ? onColor : offColor;
    }

    public void UpdateButtonSFXText()
    {
        if (AudioManager.instance == null) return;
        sfxImage.color = AudioManager.instance.IsSFXOn() ? onColor : offColor;
    }

    public void UpdateButtonUIText()
    {
        if (AudioManager.instance == null) return;
        UIButtonImage.color = AudioManager.instance.IsUIButtonOn() ? onColor : offColor;
    }

    public void ToogleButtonMusic()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        AudioManager.instance.ToggleMusic();
        UpdateButtonMusicText();
    }

    public void ToogleButtonSFX()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        AudioManager.instance.ToggleSFX();
        UpdateButtonSFXText();
    }

    public void ToggleUIButton()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        AudioManager.instance.ToggleUIButton();
        UpdateButtonUIText();
    }

    public void credit()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ScreenCredit.SetActive(true);
    }

    public void privacypolice()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        Application.OpenURL("https://docs.google.com/document/d/13dTpCdfdwwBXONHT2_-8jj7XCmtpi3M6qBSKBrSCJaA/edit?usp=sharing");
        
    }

    public void rateus()
    {
        AudioManager.instance.PlaybuttonClickSFX();
    }

    public void deletedata()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ScreenConfirm.SetActive(true);
    }

     public void no()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ScreenConfirm.SetActive(false);
    }

    public void delete()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    
    public void silang()
    {
        AudioManager.instance.PlaybuttonClickSFX();
        ScreenConfirm.SetActive(false);
        ScreenCredit.SetActive(false);
    }
}
