using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] pages;
    private int index = 0;
    void OnEnable()
    {
        index = 0;
        ShowPage(index);
    }

    void ShowPage(int i)
    {
        for (int x = 0; x < pages.Length; x++)
            pages[x].SetActive(x == i);
    }

    public void Next()
    {
        index++;
        if (index >= pages.Length)
        {
            CloseTutorial();
            return;
        }
        ShowPage(index);
    }

    public void Prev()
    {
        index--;
        if (index < 0) index = 0;
        ShowPage(index);
    }

    public void Skip()
    {
        CloseTutorial();
    }

    void CloseTutorial()
    {
        PlayerPrefs.SetInt("TutorialDone", 1);
        gameObject.SetActive(false);
    }
}

