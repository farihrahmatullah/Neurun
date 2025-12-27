using UnityEngine;

[CreateAssetMenu(fileName = "SkinData", menuName = "Skin/SkinData")]
public class SkinData : ScriptableObject
{
    public int skinID;
    public string skinName;

    public Sprite bodySprite;
    public Sprite mataKananSprite;
    public Sprite mataKiriSprite;
    public Sprite mulutSprite;

    public int hargaSkin;
    public bool unlockedByDefault;
}
