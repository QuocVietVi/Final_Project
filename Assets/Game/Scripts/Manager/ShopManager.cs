using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button weaponBtn, shieldBtn;
    [SerializeField] private ShopItemButtonAction shopItem;
    [SerializeField] private GameObject weaponBtnHolder, shieldBtnHolder;
    [SerializeField] private GameObject weaponChoose, shieldChoose, weaponPanel, shieldPanel;

    private List<WeaponData> weapons;
    private List<ShieldData> shields;

    public WeaponType currentWeapon;
    public ShieldType currentShield;


    private void Start()
    {
        weapons = SODataManager.Instance.weaponSO.weapons;
        shields = SODataManager.Instance.shieldSO.shields;
        SpawnWeaponItem();
        SpawnShieldItem();

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
