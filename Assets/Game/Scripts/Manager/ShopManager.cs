using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Shop weapon and shield
    [SerializeField] private Button weaponBtn, shieldBtn;
    [SerializeField] private ShopItemButtonAction shopItem;
    [SerializeField] private GameObject weaponBtnHolder, shieldBtnHolder;
    [SerializeField] private GameObject weaponChoose, shieldChoose, weaponPanel, shieldPanel;

    //Shop Ally
    [SerializeField] private ShopItemButtonAction allyBtn;
    [SerializeField] private GameObject allyBtnHolder;

    //Shop skill
    [SerializeField] private ShopItemButtonAction skillBtn;
    [SerializeField] private GameObject skillBtnHolder;
    

    private List<WeaponData> weapons;
    private List<ShieldData> shields;
    private List<AllyData> allies;
    private List<SkillData> skills;

    public WeaponType currentWeapon;
    public ShieldType currentShield;


    private void Start()
    {
        weapons = SODataManager.Instance.weaponSO.weapons;
        shields = SODataManager.Instance.shieldSO.shields;
        allies = SODataManager.Instance.allySO.allies;
        skills = SODataManager.Instance.skillSO.skills;

        SpawnWeaponItem();
        SpawnShieldItem();
        SpawnAllyButton();
        SpawnSkillButton();

        weaponBtn.onClick.AddListener(ActiveWeapon);
        shieldBtn.onClick.AddListener(ActiveShield);
    }


    private void SpawnWeaponItem()
    {
        for (int i = 1;  i < weapons.Count; i++)
        {
            ShopItemButtonAction item = Instantiate(shopItem, weaponBtnHolder.transform);
            item.image.sprite = weapons[i].image;
            item.weapon = weapons[i].weaponType;
            item.price = weapons[i].price;
        }

    }

    private void SpawnShieldItem()
    {
        for (int i = 1; i < shields.Count; i++)
        {
            ShopItemButtonAction item = Instantiate(shopItem, shieldBtnHolder.transform);
            item.image.sprite = shields[i].image;
            item.shield = shields[i].shieldType;
            item.price = shields[i].price;
        }

    }

    private void SpawnAllyButton()
    {
        for (int i = 1; i < allies.Count; i++)
        {
            ShopItemButtonAction item = Instantiate(allyBtn, allyBtnHolder.transform);
            item.image.sprite = allies[i].image;
            item.ally = allies[i].allyType;
            item.price = allies[i].price;
        }
    }

    private void SpawnSkillButton()
    {
        for (int i = 1; i< skills.Count; i++)
        {
            ShopItemButtonAction item = Instantiate (skillBtn, skillBtnHolder.transform);
            item.image.sprite = skills[i].image;
            item.skill = skills[i].skillType;
            item.price = skills[i].price;
        }
    }


    private void ActiveWeapon()
    {
        weaponChoose.SetActive(true);
        weaponPanel.SetActive(true);
        shieldChoose.SetActive(false);
        shieldPanel.SetActive(false);
    }

    private void ActiveShield()
    {
        weaponChoose.SetActive(false);
        weaponPanel.SetActive(false);
        shieldChoose.SetActive(true);
        shieldPanel.SetActive(true);
    }

    public void DeactivePanel(GameObject panel)
    {
        panel.SetActive(false);
        SettingManager.Instance.ButtonSoundClick();
    }
}
