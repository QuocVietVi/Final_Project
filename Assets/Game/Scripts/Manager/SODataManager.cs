using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SODataManager : Singleton<SODataManager>
{
    public WeaponSO weaponSO;
    public ShieldSO shieldSO;
    public AllySO allySO;
    public SkillSO skillSO;
    private PlayerData playerData;
    private LevelDataLocal levelData;

    public PlayerData PlayerData { get => playerData; set => playerData = value; }
    public LevelDataLocal LevelData { get => levelData; set => levelData = value; }

    private void Awake()
    {
        GetPlayerData();
        GetLevelData();
    }

    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        return weaponSO.weapons[(int)weaponType];
    }

    public ShieldData GetShieldData(ShieldType shieldType)
    {
        return shieldSO.shields[(int)shieldType];
    }

    public AllyData GetAllyData(AllyType allyType)
    {
        return allySO.allies[(int)allyType];
    }

    public SkillData GetSkillData(SkillType skillType)
    {
        return skillSO.skills[(int)skillType];
    }

    public void GetPlayerData()
    {
        if (DataManager.Instance.HasData<PlayerData>())
        {
            PlayerData = DataManager.Instance.LoadData<PlayerData>();
            Debug.Log("Loaded User Data.");
        }
        else
        {
            PlayerData = new PlayerData();
            DataManager.Instance.SaveData(PlayerData);
            Debug.Log("Creating New User Data");
        }
    }

    public void GetLevelData()
    {
        if (DataManager.Instance.HasData<LevelDataLocal>())
        {
            LevelData = DataManager.Instance.LoadData<LevelDataLocal>();
            Debug.Log("Loaded User Data.");
        }
        else
        {
            LevelData = new LevelDataLocal();
            DataManager.Instance.SaveData(LevelData);
            Debug.Log("Creating New User Data");
        }
    }

}
