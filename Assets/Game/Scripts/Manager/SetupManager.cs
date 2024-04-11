using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : Singleton<SetupManager>
{
    [SerializeField] private ButtonSetup itemSetup;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private GameObject weaponFocus, shieldFocus, allyFocus, skillFocus;
    [SerializeField] private Button weapon, shield, ally, skill;
    private List<ButtonSetup> items = new List<ButtonSetup>();
    private ButtonSetup item;

    public List<DropSlot> dropSlots = new List<DropSlot>();
    private void Start()
    {
        weapon.onClick.AddListener(SpawnWeapons);
        shield.onClick.AddListener(SpawnShields);
        ally.onClick.AddListener(SpawnAllies);
        skill.onClick.AddListener(SpawnSkills);

    }

    public void SpawnWeapons()
    {
        var data = SODataManager.Instance;
        DespawnAllItem();
        data.GetPlayerData();
        for (int i = 0; i < data.PlayerData.weaponsOwned.Count; i++)
        {
            item = LeanPool.Spawn(itemSetup, itemHolder);
            WeaponData wData = data.GetWeaponData((WeaponType)data.PlayerData.weaponsOwned[i]);
            item.image.sprite = wData.image;
            item.itemType = ItemType.Weapon;
            item.skillType = SkillType.Default;
            item.weaponType = wData.weaponType;
            items.Add(item);
        }
        DeactiveFocus();
        weaponFocus.SetActive(true);
        
    }

    public void SpawnShields()
    {
        var data = SODataManager.Instance;
        DespawnAllItem();
        for (int i = 0; i < data.PlayerData.shieldsOwned.Count; i++)
        {
            item = LeanPool.Spawn(itemSetup, itemHolder);
            ShieldData sData = data.GetShieldData((ShieldType)data.PlayerData.shieldsOwned[i]);
            item.image.sprite = sData.image;
            item.itemType = ItemType.Shield;
            item.shieldType = sData.shieldType;
            items.Add(item);
        }
        DeactiveFocus();
        shieldFocus.SetActive(true);
    }

    public void SpawnAllies()
    {
        var data = SODataManager.Instance;
        DespawnAllItem();
        for (int i = 0; i < data.PlayerData.alliesOwned.Count; i++)
        {
            item = LeanPool.Spawn(itemSetup, itemHolder);
            AllyData aData = data.GetAllyData((AllyType)data.PlayerData.alliesOwned[i]);
            item.image.sprite = aData.image;
            item.itemType = ItemType.Ally;
            item.allyType = aData.allyType;
            items.Add(item);
        }
        DeactiveFocus();
        allyFocus.SetActive(true);
    }

    public void SpawnSkills()
    {
        var data = SODataManager.Instance;
        DespawnAllItem();
        for (int i = 0; i < data.PlayerData.skillsOwned.Count; i++)
        {
            item = LeanPool.Spawn(itemSetup, itemHolder);
            SkillData sData = data.GetSkillData((SkillType)data.PlayerData.skillsOwned[i]);
            item.image.sprite = sData.image;
            item.itemType = ItemType.Skill;
            item.skillType = sData.skillType;
            item.weaponType = WeaponType.Default;
            items.Add(item);
        }
        DeactiveFocus();
        skillFocus.SetActive(true);
    }

    public void DespawnAllItem()
    {
        if (items.Count > 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                LeanPool.Despawn(items[i].gameObject);
            }
            items.Clear();
        }

    }

    private void DeactiveFocus()
    {
        weaponFocus.SetActive(false);
        shieldFocus.SetActive(false);
        allyFocus.SetActive(false) ;
        skillFocus.SetActive(false);
    }

    public void CheckSetup()
    {
        var items = dropSlots;
        if (items[0].itemImage != null || items[1].itemImage != null || items[2].itemImage != null)
        {
            HomeManager.Instance.canSpawnLevel = true;
        }
        else
        {
            HomeManager.Instance.canSpawnLevel = false;
            HomeManager.Instance.Popup();

        }
    }
}
