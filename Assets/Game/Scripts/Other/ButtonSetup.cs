using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon,
    Shield,
    Ally,
    Skill
}

public class ButtonSetup : MonoBehaviour
{
    public Image image;
    public ItemType itemType;
    public WeaponType weaponType;
    public ShieldType shieldType;
    public AllyType allyType;
    public SkillType skillType;
}
