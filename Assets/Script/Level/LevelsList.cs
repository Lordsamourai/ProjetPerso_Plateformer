using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Game/Levels List")]
public class LevelsList : ScriptableObject
{
    public LevelData[] levels;
}