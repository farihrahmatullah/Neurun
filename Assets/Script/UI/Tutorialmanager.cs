using UnityEngine;

public class Tutorialmanager : MonoBehaviour
{
    public GameObject tutorialPanel;

    void Start()
    {
        if (PlayerPrefs.GetInt("TutorialDone", 0) == 0)
        {
            tutorialPanel.SetActive(true);
            PlayerPrefs.SetInt("TutorialDone", 1);
        }
    }

    public void OpenFromMenu()
    {
        tutorialPanel.SetActive(true);
    }
}
