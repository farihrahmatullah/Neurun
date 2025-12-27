using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public SettingsManager settings;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Music Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    [Header("SFX Clips")]
    public AudioClip jumpSFX;
    // public AudioClip downSFX;
    public AudioClip collectLetterSFX;
    public AudioClip gameover;
    public AudioClip uinotif;
    public AudioClip switchword;
    public AudioClip buttonclick;
    public AudioClip purchase;

    private bool musicOn = true;
    private bool sfxOn = true;
    private bool UIButtonGameplay = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Ambil status dari PlayerPrefs
        musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        UIButtonGameplay = PlayerPrefs.GetInt("UIButton", 1) == 1;

        musicSource.mute = !musicOn;
        sfxSource.mute = !sfxOn;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // // Update referensi SettingsManager setiap scene dimuat
        // settings = FindObjectOfType<SettingsManager>();

        if (scene.name == "MainMenu")
            PlayMusic(mainMenuMusic);
        else if (scene.name == "GamePlay")
            PlayMusic(gameplayMusic);

        // Update semua tampilan setelah load scene
        if (settings != null)
        {
            settings.UpdateButtonMusicText();
            settings.UpdateButtonSFXText();
            settings.UpdateButtonUIText();
        }

        // Update status tombol UI gameplay (kalau ada)
        UpdateUIButtonVisibility();
    }

    // ---------- MUSIC ----------
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        musicSource.mute = !musicOn;

        PlayerPrefs.SetInt("MusicOn", musicOn ? 1 : 0);
        PlayerPrefs.Save();

        if (musicOn && !musicSource.isPlaying)
            musicSource.Play();

        settings?.UpdateButtonMusicText();
    }

    // ---------- SFX ----------
    public void ToggleSFX()
    {
        sfxOn = !sfxOn;
        sfxSource.mute = !sfxOn;

        PlayerPrefs.SetInt("SFXOn", sfxOn ? 1 : 0);
        PlayerPrefs.Save();

        settings?.UpdateButtonSFXText();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxOn)
            sfxSource.PlayOneShot(clip);
    }

    // ---------- UI BUTTON (Analog) ----------
    public void ToggleUIButton()
    {
        UIButtonGameplay = !UIButtonGameplay;
        PlayerPrefs.SetInt("UIButton", UIButtonGameplay ? 1 : 0);
        PlayerPrefs.Save();

        UpdateUIButtonVisibility();
        settings?.UpdateButtonUIText();
    }

    public void UpdateUIButtonVisibility()
    {
        // Hanya cari tombol UI di scene Gameplay
        GameObject buttonUI = GameObject.FindWithTag("GameController");

        if (buttonUI != null)
        {
            buttonUI.SetActive(UIButtonGameplay);
        }
    }

    // ---------- Utility ----------
    public void PlayJumpSFX() => PlaySFX(jumpSFX);
    public void PlayCollectLetterSFX() => PlaySFX(collectLetterSFX);
    // public void PlaydownSFX() => PlaySFX(downSFX);
    public void PlayGameoverSFX() => PlaySFX(gameover);
    public void PlayUiSFX() => PlaySFX(uinotif);
    public void PlaybuttonClickSFX() => PlaySFX(buttonclick);
    public void PlayswitchwordSFX() => PlaySFX(switchword);
    public void PlaypurchaseSFX() => PlaySFX(purchase);

    public bool IsMusicOn() => musicOn;
    public bool IsSFXOn() => sfxOn;
    public bool IsUIButtonOn() => UIButtonGameplay;

    
}
