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
        button.onClick.AddListener(() => 
        {
            PreviewInfo();
            ShopManager.Instance.CheckOwned();
        });
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
                shop.currentWeapon = weapon;
            }
            if (shop.isShieldShop)
            {
                ShieldData sData = SOData.GetShieldData(shield);
                shop.ChangeWSText("Hp bonus :", sData.bonusHp.ToString());
                shop.priceTxt.text = sData.price.ToString();
                shop.itemName.text = shield.ToString();
                shop.price =sData.price;
                shop.currentShield = shield;
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
            shop.currentAlly = ally;
        }
        if (button.gameObject.name.Contains("Skill"))
        {
            shop.skillInfo.SetActive(true);
            shop.skillImage.sprite = this.image.sprite;
            SkillData sData = SOData.GetSkillData(skill);
            shop.ChangeSkillText(sData.damage.ToString());
            shop.gemTxt.text = sData.price.ToString();
            shop.itemName.text = skill.ToString();
            shop.price = sData.price;
            shop.currentSkill = skill;
        }
        
    }

    private void CheckOwned()
    {
        var playerData = SODataManager.Instance.PlayerData;
        var weapons = playerData.weaponsOwned;
        var shields = playerData.shieldsOwned;
        var allies = playerData.alliesOwned;
        var skills = playerData.skillsOwned;
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weapons[i] == (int)weapon)
            {
                ActiveButtonOwn();
            }
            else
            {
                DeactiveButtonOwn();
            }
        }
        for (int i = 0; i < shields.Count; i++)
        {
            if (shields[i] == (int)shield)
            {
                ActiveButtonOwn();
            }
            else
            {
                DeactiveButtonOwn();
            }
        }
        for (int i = 0; i < allies.Count; i++)
        {
            if (allies[i] == (int)ally)
            {
                ActiveButtonOwn();
            }
            else
            {
                DeactiveButtonOwn();
            }
        }
        for (int i = 0; i < skills.Count; i++)
        {
            if (skills[i] == (int)skill)
            {
                ActiveButtonOwn();
            }
            else
            {
                DeactiveButtonOwn();
            }
        }
    }

    private void ActiveButtonOwn()
    {
        ShopManager.Instance.ownBtn.SetActive(true);
    }

    private void DeactiveButtonOwn()
    {
        ShopManager.Instance.ownBtn.SetActive(false);
    }


}
