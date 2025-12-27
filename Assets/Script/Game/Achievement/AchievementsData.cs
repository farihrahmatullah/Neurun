using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AchievementsData
{  
    public string id;
    public string title;
    public string description;
    public string type;
    public int target;

}

[System.Serializable]
public class AchievementList
{
    public List<AchievementsData> achievements;
}