using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SODataManager : Singleton<SODataManager>
{
    public WeaponSO weaponSO;
    public ShieldSO shieldSO;
    public AllySO allySO;
    
    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        return weaponSO.weapons[(int)weaponType];
    }

    public ShieldData GetShieldData(ShieldType shieldType)
    {
        return shieldSO.shields[(int)shieldType];
    }
}
