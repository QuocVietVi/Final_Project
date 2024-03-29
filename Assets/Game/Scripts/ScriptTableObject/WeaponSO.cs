using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Weapon")]
public class WeaponSO : ScriptableObject
{
    public List<WeaponData> weapons;
}
