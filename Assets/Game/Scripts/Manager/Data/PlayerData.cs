using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public int golds;
    public int gems;
    public int stars;

    public float maxHp;
    public float maxMana;
    public float maxEnergy;

    public List<int> alliesOwned;
    public List<int> skillsOwned;
    public List<int> weaponsOwned;
    public List<int> shieldsOwned;

    public PlayerData()
    {
        golds = gems = stars = 0;
        maxHp = 1000;
        maxMana = maxEnergy = 100;
        alliesOwned = new List<int>();
        skillsOwned = new List<int>();
        weaponsOwned = new List<int>();
        shieldsOwned = new List<int>();
    }

    public PlayerData(int golds, int gems, int stars, float maxHp, float maxMana, float maxEnergy,
        List<int> alliesOwned, List<int> skillsOwned, List<int> weaponsOwned, List<int> shieldsOwned)
    {
        this.golds = golds;
        this.gems = gems;
        this.stars = stars;
        this.maxHp = maxHp;
        this.maxMana = maxMana;
        this.maxEnergy = maxEnergy;
        this.alliesOwned = alliesOwned;
        this.skillsOwned = skillsOwned;
        this.weaponsOwned = weaponsOwned;
        this.shieldsOwned = shieldsOwned;
    }

}