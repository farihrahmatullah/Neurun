using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;

    [Header("UI Achievement")]
    public GameObject achievementPopup;
    public TMP_Text titleText;
    public float popupDuration = 2f;

    private AchievementList achievementList;
    private HashSet<string> unlockedAchievements = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        LoadAchievements();
        LoadUnlockedAchievements();
    }

    private void Start()
    {
        achievementPopup.SetActive(false);
    }

    private void OnEnable()
    {
        AchievementEventHub.OnProgressReported += HandleProgressEvent;
    }

    private void OnDisable()
    {
        AchievementEventHub.OnProgressReported -= HandleProgressEvent;
    }

    private void LoadAchievements()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("achievements");
        if (jsonFile != null)
            achievementList = JsonUtility.FromJson<AchievementList>(jsonFile.text);
        else
            achievementList = new AchievementList { achievements = new List<AchievementsData>() };
    }

    private void LoadUnlockedAchievements()
    {
        string data = PlayerPrefs.GetString("UnlockedAchievements", "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] ids = data.Split(',');
            foreach (string id in ids)
                unlockedAchievements.Add(id);
        }
    }

    private void SaveUnlockedAchievements()
    {
        PlayerPrefs.SetString("UnlockedAchievements", string.Join(",", unlockedAchievements));
        PlayerPrefs.Save();
    }

    private void HandleProgressEvent(string type, int currentValue)
    {
        foreach (var ach in achievementList.achievements)
        {
            if (ach.type == type && currentValue >= ach.target && !unlockedAchievements.Contains(ach.id))
            {
                UnlockAchievement(ach);
            }
        }
    }

    private void UnlockAchievement(AchievementsData ach)
    {
        unlockedAchievements.Add(ach.id);
        SaveUnlockedAchievements();
        StartCoroutine(ShowPopupRoutine(ach.title, ach.description));
    }

    private System.Collections.IEnumerator ShowPopupRoutine(string title, string description)
    {
        achievementPopup.SetActive(true);
        AudioManager.instance.PlayUiSFX();
        titleText.text = title;

        yield return new WaitForSeconds(popupDuration);

        achievementPopup.SetActive(false);
    }
}
