using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUI : MonoBehaviour
{
    public Slider progressBar;

    public void UpdateProgress(float progress, string message)
    {
        if (progressBar) progressBar.value = progress;
    }
}
