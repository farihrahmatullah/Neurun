using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WordData
{
    public string word;
    public string latin;
    public string kategori;
    public string arti;

    internal string ToUpper()
    {
        throw new NotImplementedException();
    }
}

[System.Serializable]
public class WordList
{
    public List<WordData> words;
}
