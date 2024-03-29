using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShieldData 
{
    public ShieldType shieldType;
    public float bonusHp;
    public Sprite image;
    public int price;
}

public enum ShieldType
{
    
    Shield_0 = 0,
    Shield_1 = 1,
    Shield_2 = 2,
    Shield_3 = 3,
    Shield_4 = 4,
    Shield_5 = 5,
    Shield_6 = 6,    
    Shield_7 = 7,
    Shield_8 = 8,
}