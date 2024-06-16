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
    public int numberOfInstruction;

    public float maxHp;
    public float maxMana;
    public float maxEnergy;
    public float damage;
    public float manaRecovery;
    public float energyRecovery;

    public List<int> alliesOwned;
    public List<int> skillsOwned;
    public List<int> weaponsOwned;
    public List<int> shieldsOwned;

    public PlayerData()
    {
        numberOfInstruction= golds = gems = stars = 0;
        maxHp = 1000;
        maxMana = maxEnergy = 100;
        damage = 15;
        manaRecovery = 4.6f;
        energyRecovery = 4.6f;
        alliesOwned = new List<int> {1};
        skillsOwned = new List<int>();
        weaponsOwned = new List<int> {1};
        shieldsOwned = new List<int>();
    }

    public PlayerData(int golds, int gems, int stars, int numberOfInstruction, float maxHp, float maxMana, float maxEnergy,
        List<int> alliesOwned, List<int> skillsOwned, List<int> weaponsOwned, List<int> shieldsOwned)
    {
        this.golds = golds;
        this.gems = gems;
        this.stars = stars;
        this.numberOfInstruction = numberOfInstruction;
        this.maxHp = maxHp;
        this.maxMana = maxMana;
        this.maxEnergy = maxEnergy;
        this.alliesOwned = alliesOwned;
        this.skillsOwned = skillsOwned;
        this.weaponsOwned = weaponsOwned;
        this.shieldsOwned = shieldsOwned;
    }

}
