using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : Singleton<ShopManager>
{
    //Shop weapon and shield
    [Header("Shop weapon and shield")]
    [SerializeField] private Button weaponBtn, shieldBtn;
    [SerializeField] private ShopItemButtonAction shopItem;
    [SerializeField] private GameObject weaponBtnHolder, shieldBtnHolder;
    [SerializeField] private GameObject weaponChoose, shieldChoose, weaponPanel, shieldPanel;
    public Image wsImage;
    public GameObject itemInfo;
    public TextMeshProUGUI wsTitle;
    public TextMeshProUGUI wsDameAndHp;

    [Space(10)]
    //Shop Ally
    [Header("Shop Ally")]
    [SerializeField] private ShopItemButtonAction allyBtn;
    [SerializeField] private GameObject allyBtnHolder;
    public Image allyImage;
    public GameObject allyInfo;
    public TextMeshProUGUI allyHp;
    public TextMeshProUGUI allyDame;

    [Space(10)]
    //Shop skill
    [Header("Shop skill")]
    [SerializeField] private ShopItemButtonAction skillBtn;
    [SerializeField] private GameObject skillBtnHolder;
    public Image skillImage;
    public GameObject skillInfo;
    public TextMeshProUGUI skillDame;

    private List<WeaponData> weapons;
    private List<ShieldData> shields;
    private List<AllyData> allies;
    private List<SkillData> skills;

    [Space(5)]
    [Header("Other")]
    public WeaponType currentWeapon;
    public ShieldType currentShield;
    public AllyType currentAlly;
    public SkillType currentSkill;
    public bool isWeaponShop, isShieldShop;
    public TextMeshProUGUI priceTxt;
    public TextMeshProUGUI gemTxt;
    public TextMeshProUGUI itemName;
    public Button buyBtn;
    public Button buyBtnGem;
    public GameObject ownBtn;
    public int price;

    [Space(5)]
    [Header("Status")]
    public TextMeshProUGUI stars;
    public TextMeshProUGUI gems;
    public TextMeshProUGUI golds;

    private PlayerData playerData;  

    private void Start()
    {
        var data = SODataManager.Instance;

        weapons = data.weaponSO.weapons;
        shields = data.shieldSO.shields;
        allies = data.allySO.allies;
        skills = data.skillSO.skills;



        SpawnWeaponItem();
        SpawnShieldItem();
        SpawnAllyButton();
        SpawnSkillButton();

        weaponBtn.onClick.AddListener(ActiveWeapon);
        shieldBtn.onClick.AddListener(ActiveShield);
        buyBtn.onClick.AddListener(() =>
        {
            BuyWithGold(price);
        });
        buyBtnGem.onClick.AddListener(() =>
        {
            BuyWithGem(price);
        });
        isWeaponShop = true;
    }

    private void Update()
    {
        playerData = SODataManager.Instance.PlayerData;
        stars.text = playerData.stars.ToString();
        golds.text = playerData.golds.ToString();
        gems.text = playerData.gems.ToString();
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
        itemInfo.SetActive(false);
        isWeaponShop = true;
        isShieldShop = false;
        SetItemTypeDefault();
    }

    private void ActiveShield()
    {
        weaponChoose.SetActive(false);
        weaponPanel.SetActive(false);
        shieldChoose.SetActive(true);
        shieldPanel.SetActive(true);
        itemInfo.SetActive(false);
        isShieldShop = true;
        isWeaponShop = false;
        SetItemTypeDefault();
    }

    public void DeactivePanel(GameObject panel)
    {
        panel.SetActive(false);
        SettingManager.Instance.ButtonSoundClick();
    }

    public void SetItemTypeDefault()
    {
        currentShield = ShieldType.Shield_0;
        currentWeapon = WeaponType.Default;
        currentAlly = AllyType.Default;
        currentSkill = SkillType.Default;
    }

    public void ChangeWSText(string title, string dameAndHp)
    {
        wsTitle.text = title;
        wsDameAndHp.text = dameAndHp;
    }

    public void ChangeAllyText(string hp, string dame)
    {
        allyHp.text = hp;
        allyDame.text = dame;
    }

    public void ChangeSkillText(string dame)
    {
        skillDame.text = dame;
    }

    private void BuyWithGold(int price)
    {

        if(playerData.golds >= this.price)
        {
            if (currentWeapon != WeaponType.Default)
            {
                playerData.weaponsOwned.Add((int)currentWeapon);
                GoldDeduction();
            }
            if (currentShield != ShieldType.Shield_0)
            {
                playerData.shieldsOwned.Add((int)currentShield);
                GoldDeduction();
            }
            if (currentAlly != AllyType.Default)
            {
                playerData.alliesOwned.Add((int)currentAlly);
                GoldDeduction();
            }
            ActiveButtonOwn();
        }
    }

    private void BuyWithGem(int price)
    {

        if (playerData.gems >= this.price && currentSkill != SkillType.Default)
        {
            playerData.gems -= price;
            playerData.skillsOwned.Add((int)currentSkill);
            DataManager.Instance.SaveData(playerData);
            ActiveButtonOwn();
        }
    }

    private void GoldDeduction()
    {
        playerData.golds -= price;
        DataManager.Instance.SaveData(playerData);
    }

    public void CheckOwned()
    {
        var playerData = SODataManager.Instance.PlayerData;
        var weapons = playerData.weaponsOwned;
        var shields = playerData.shieldsOwned;
        var allies = playerData.alliesOwned;
        var skills = playerData.skillsOwned;

        if (weapons.Contains((int)currentWeapon) || shields.Contains((int)currentShield)
                || allies.Contains((int)currentAlly) || skills.Contains((int)currentSkill))
        {
            ActiveButtonOwn();
        }
        else
        {
            DeactiveButtonOwn();
        }

    }

    private void ActiveButtonOwn()
    {
        ownBtn.SetActive(true);
    }

    private void DeactiveButtonOwn()
    {
        ownBtn.SetActive(false);
    }


}
