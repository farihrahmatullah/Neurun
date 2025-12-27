using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AtlasSpriteData", menuName = "GameData/Sprite Atlas Huruf")]
public class AtlasSpriteData : ScriptableObject
{
    [System.Serializable]
    public class LetterSpritePair
    {
        public char letter;
        public Sprite sprite;
    }

    public List<LetterSpritePair> letterSprites = new List<LetterSpritePair>();

    private Dictionary<char, Sprite> lookup;

    private void OnEnable()
    {
        lookup = new Dictionary<char, Sprite>();
        foreach (var pair in letterSprites)
        {
            char upper = char.ToUpper(pair.letter);
            if (!lookup.ContainsKey(upper))
                lookup.Add(upper, pair.sprite);
        }
    }

    public Sprite GetSpriteForLetter(char letter)
    {
        char upper = char.ToUpper(letter);
        if (lookup != null && lookup.ContainsKey(upper))
            return lookup[upper];
        return null;
    }
}
