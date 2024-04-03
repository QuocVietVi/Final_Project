using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AllyData 
{
    public AllyType allyType;
    public AllyAndEnemy allyPrefab;
    public Sprite image;
    public int hp;
    public int damage;
    public int price;
}

public enum AllyType
{
    Default = 0,
    Elf_1 = 1,
    Elf_2 = 2,
    Fairy_1 = 3,
    Fairy_2 = 4,
    Knight_1 = 5,
    Knight_2 = 6,
    Knight_3 = 7,
    Warrior_1 = 8,
    Warrior_2 = 9,
    Warrior_3 = 10,
    Samurai_1 = 11,
    Samurai_2 = 12,
    Samurai_3 = 13,
    Viking_1 = 14,
    Viking_2 = 15,
    Viking_3 = 16
}
