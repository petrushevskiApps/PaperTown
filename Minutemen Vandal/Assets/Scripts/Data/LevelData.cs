using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int pointsToEarnInLevel;
    public Color levelReward;
    public List<Color> unlockedColors;
}
