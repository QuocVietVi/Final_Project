using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField] private Button play;
    [SerializeField] private Button tapToStart;
    [SerializeField] private Button setting;
    [SerializeField] private GameObject startScene, homeScene;
    [SerializeField] private GameObject settingPanel;


    public GameObject homePanel;
    public TextMeshProUGUI stars;
    public TextMeshProUGUI gems;
    public TextMeshProUGUI golds;
    private void Start()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        play.onClick.AddListener(SpawnLevelButton);
        tapToStart.onClick.AddListener(CloseStartPanel);
        setting.onClick.AddListener(ActiveSetting);
    }

    private void Update()
    {
        stars.text = SODataManager.Instance.PlayerData.stars.ToString();
        golds.text = SODataManager.Instance.PlayerData.golds.ToString();
        gems.text = SODataManager.Instance.PlayerData.gems.ToString();
    }

    private void SpawnLevelButton()
    {
        LevelManager.Instance.levelPanel.SetActive(true);
        homePanel.SetActive(false);
        SettingManager.Instance.PlayMusic(ConstantSound.THEME);
        ButtonSoundClick();
    }

    private void CloseStartPanel()
    {
        startScene.SetActive(false);
        homeScene.SetActive(true);
        ButtonSoundClick();
        SettingManager.Instance.PlayMusic(ConstantSound.THEME);
    }

    private void ActiveSetting()
    {
        settingPanel.SetActive(true);
        ButtonSoundClick();
    }

    private void ButtonSoundClick()
    {
        SettingManager.Instance.ButtonSoundClick();

    }

    public void ActivePanel(GameObject panel)
    {
        panel.SetActive(true);
        SettingManager.Instance.ButtonSoundClick();

    }

}
