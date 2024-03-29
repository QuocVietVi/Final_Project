using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponData 
{
    public WeaponType weaponType;
    public float damage;
    public GameObject weaponPrefab;
    public int price;
}

public enum WeaponType
{
    Default = 0,
    Hammer_1 = 1,
    Hammer_2 = 2,
    Axe_1 = 3,
    Axe_2 = 4,
    Axe_3 = 5,
    Axe_4 = 6,
    Sword_1 = 7,
    Sword_2 = 8,
    Trident = 9
}
