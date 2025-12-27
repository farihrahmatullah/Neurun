using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AchievementListUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject achievementItemPrefab;   // Prefab item (UIprefabachivment-01)
    public Transform contentParent;            // Tempat spawn semua item (misalnya ScrollView Content)
    public Color unlockedColor = Color.white;  // Warna normal untuk sukses
    public Color lockedColor = new Color(0.4f, 0.4f, 0.4f, 0.8f); // Warna gelap untuk belum

    private AchievementList achievementList;
    private HashSet<string> unlockedAchievements = new HashSet<string>();

    void Start()
    {
        LoadAchievements();
        LoadUnlockedAchievements();
        PopulateUI();
    }

    private void LoadAchievements()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("achievements");
        if (jsonFile != null)
        {
            achievementList = JsonUtility.FromJson<AchievementList>(jsonFile.text);
        }
        else
        {
            achievementList = new AchievementList { achievements = new List<AchievementsData>() };
        }
    }

    private void LoadUnlockedAchievements()
    {
        string data = PlayerPrefs.GetString("UnlockedAchievements", "");
        if (!string.IsNullOrEmpty(data))
        {
            string[] ids = data.Split(',');
            foreach (string id in ids)
            {
                if (!string.IsNullOrEmpty(id))
                    unlockedAchievements.Add(id);
            }
        }
    }

    private void PopulateUI()
    {
        // Bersihkan isi lama
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        foreach (var ach in achievementList.achievements)
        {
            GameObject item = Instantiate(achievementItemPrefab, contentParent);

            // Ambil semua komponen TMP di dalam prefab
            TMP_Text[] texts = item.GetComponentsInChildren<TMP_Text>();
            TMP_Text titleText = null;
            TMP_Text descText = null;
            TMP_Text statusText = null;

            foreach (TMP_Text t in texts)
            {
                if (t.name.ToLower().Contains("title")) titleText = t;
                else if (t.name.ToLower().Contains("desc")) descText = t;
                else if (t.name.ToLower().Contains("status")) statusText = t;
            }

            if (titleText) titleText.text = ach.title;
            if (descText) descText.text = ach.description;

            bool unlocked = unlockedAchievements.Contains(ach.id);

            if (statusText)
            {
                statusText.text = unlocked ? "Sukses" : "Belum";
            }

            // Ubah warna seluruh item jadi gelap kalau belum tercapai
            Image[] bgImages = item.GetComponentsInChildren<Image>();
            foreach (Image img in bgImages)
            {
                img.color = unlocked ? unlockedColor : lockedColor;
            }
        }
    }
}
