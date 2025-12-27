using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mode
{
    None = 0,
    Latin = 1,
    Inggris = 2,
    Hiragana = 3,
    Katakana = 4
}

public class GameManagerNeurun : MonoBehaviour
{
    public static Mode SelectedMode;  // Ini tempat menyimpan mod
}

