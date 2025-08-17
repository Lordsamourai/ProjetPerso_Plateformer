using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelIndex;    
    public string levelName;     
    public string sceneName;     
}
