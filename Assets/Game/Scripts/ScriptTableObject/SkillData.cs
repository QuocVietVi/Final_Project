using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData 
{
    public SkillType skillType;
    public SkillCollider skillPrefab;
    public Sprite image;
    public int damage;
    public int price;
}

public enum SkillType
{
    Default = 0,
    Flame_1 = 1,
    Flame_2 = 2,
    Water_1 = 3,
    Water_2 = 4,
    Thunder = 5,
}
