using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemButtonAction : MonoBehaviour
{
    public Image image;
    public WeaponType weapon;
    public ShieldType shield;
    public AllyType ally;
    public SkillType skill;
    public int price;
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(PreviewInfo);
    }

    private void PreviewInfo()
    {
        var shop = ShopManager.Instance;
        var SOData = SODataManager.Instance;
        if (button.gameObject.name.Contains("Item"))
        {
            shop.itemInfo.SetActive(true);
            shop.wsImage.sprite = this.image.sprite;
            if (shop.isWeaponShop)
            {
                WeaponData wData = SOData.GetWeaponData(weapon);
                shop.ChangeWSText("Damage :", wData.damage.ToString());
                shop.priceTxt.text = wData.price.ToString();
                shop.itemName.text = weapon.ToString();
                shop.price = wData.price;
            }
            if (shop.isShieldShop)
            {
                ShieldData sData = SOData.GetShieldData(shield);
                shop.ChangeWSText("Hp bonus :", sData.bonusHp.ToString());
                shop.priceTxt.text = sData.price.ToString();
                shop.itemName.text = shield.ToString();
                shop.price =sData.price;
            }
        }
        if (button.gameObject.name.Contains("Ally"))
        {
            shop.allyInfo.SetActive(true);
            shop.allyImage.sprite = this.image.sprite;
            AllyData aData = SOData.GetAllyData(ally);
            shop.ChangeAllyText(aData.hp.ToString(), aData.damage.ToString());
            shop.priceTxt.text = aData.price.ToString();
            shop.itemName.text = ally.ToString();
            shop.price = aData.price;
        }
        if (button.gameObject.name.Contains("Skill"))
        {
            shop.skillInfo.SetActive(true);
            shop.skillImage.sprite = this.image.sprite;
            SkillData sData = SOData.GetSkillData(skill);
            shop.ChangeSkillText(sData.damage.ToString());
            shop.priceTxt.text = sData.price.ToString();
            shop.itemName.text = skill.ToString();
            shop.price = sData.price;
        }
    }
}
