using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private Button hp, mana, energy, damage;
    [SerializeField] private Image hpFill, manaFill, energyFill, damageFill;
    [SerializeField] private TextMeshProUGUI hpTxt, manaTxt, energyTxt, damageTxt, starsTxt;

    private float maxHpCanUpgrade;
    private float maxManaCanUpgrade;
    private float maxEnergyCanUpgrade;
    private float maxDamageCanUpgrade;
    private PlayerData playerData;
    private void Start()
    {
        maxHpCanUpgrade = 2200;
        maxManaCanUpgrade = 220;
        maxEnergyCanUpgrade = 220;
        maxDamageCanUpgrade = 33;
        playerData = SODataManager.Instance.PlayerData;

        hp.onClick.AddListener(UpgradeHp);
        mana.onClick.AddListener(UpgradeMana);
        energy.onClick.AddListener(UpgradeEnergy);
        damage.onClick.AddListener(UpgradeDamage);
    }

    private void Update()
    {
        hpFill.fillAmount = GetHpNormalized();
        manaFill.fillAmount = GetManaNormalized();
        energyFill.fillAmount = GetEnergyNormalized();
        damageFill.fillAmount = GetDamageNormalized();
        hpTxt.text = playerData.maxHp.ToString() + " / " + maxHpCanUpgrade;
        manaTxt.text = playerData.maxMana.ToString() + " / " + maxManaCanUpgrade;
        energyTxt.text = playerData.maxEnergy.ToString() + " / " + maxEnergyCanUpgrade;
        damageTxt.text = playerData.damage.ToString() + " / " + maxDamageCanUpgrade;
        starsTxt.text = playerData.stars.ToString();
    }

    private void UpgradeHp()
    {
        if (playerData.maxHp < maxHpCanUpgrade && playerData.stars > 0)
        {
            playerData.maxHp += 200;
            playerData.stars --;
            DataManager.Instance.SaveData(playerData);

        }
    }

    private void UpgradeMana()
    {
        if (playerData.maxMana < maxManaCanUpgrade && playerData.stars > 0)
        {
            playerData.maxMana += 20;
            playerData.manaRecovery += 0.3f;
            playerData.stars--;
            DataManager.Instance.SaveData(playerData);
        }
    }

    private void UpgradeEnergy()
    {
        if (playerData.maxEnergy < maxEnergyCanUpgrade && playerData.stars > 0)
        {
            playerData.maxEnergy += 20;
            playerData.energyRecovery += 0.3f;
            playerData.stars--;
            DataManager.Instance.SaveData(playerData);

        }
    }

    private void UpgradeDamage()
    {
        if (playerData.damage < maxDamageCanUpgrade && playerData.stars > 0)
        {
            playerData.damage += 3;
            playerData.stars--;
            DataManager.Instance.SaveData(playerData);

        }
    }

    private float GetManaNormalized()
    {
        return playerData.maxMana / maxManaCanUpgrade;
    }

    private float GetEnergyNormalized()
    {
        return playerData.maxEnergy / maxEnergyCanUpgrade;
    }

    private float GetHpNormalized()
    {
        return playerData.maxHp / maxHpCanUpgrade;
    }

    private float GetDamageNormalized()
    {
        return playerData.damage / maxDamageCanUpgrade;
    }

}
