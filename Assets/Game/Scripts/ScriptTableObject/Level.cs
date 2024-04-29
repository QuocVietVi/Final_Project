using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Chapter
{
    Chapter1 = 0, Chapter2 = 1, Chapter3 = 2
}
[Serializable]
public class Level 
{
    public Chapter chapter;
    //public List<int> level;
    //public List<int> stars;
    public List<LevelInfo> levelInfo;
}
[Serializable]
public class LevelInfo
{
    public int level;
    public int stars;
    public int golds;
    public int gems;
}
