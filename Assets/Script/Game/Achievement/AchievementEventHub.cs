using System;
using UnityEngine;

public static class AchievementEventHub
{
    // Event yang bisa didengar oleh AchievementManager
    public static event Action<string, int> OnProgressReported;

    // Fungsi publik untuk mengirim event
    public static void Report(string type, int value)
    {
        OnProgressReported?.Invoke(type, value);
    }
}
