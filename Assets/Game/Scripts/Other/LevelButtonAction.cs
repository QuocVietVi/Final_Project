using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonAction : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    public TextMeshProUGUI levelText;
    public GameObject levelPanel;
    public Map map;
    public void SetOnClick(int chapter, int level, int stars, int golds, int gems)
    {
        levelButton.onClick.AddListener(() =>
        {
            var levelManager = LevelManager.Instance;
            SpawnMap(chapter,level);
            levelManager.SetTextCurrentChapter(chapter,level);
            levelManager.currentLevel = level;
            levelManager.stars = stars;
            levelManager.golds = golds;
            levelManager.gems = gems;
            SettingManager.Instance.ButtonSoundClick();
            levelManager.ScreenTransition();
            GameManager.Instance.SetTextReward(golds,gems);
        });
    }

    private void SpawnMap(int chapter, int level)
    {
        map = Resources.Load<Map>(Constant.MAP_NAME + chapter + "-" + level);
        Instantiate(map);
        LevelManager.Instance.FindMap();
        levelPanel.SetActive(false);
        Invoke(nameof(SpawnPlayer), 0.5f);
    }

    private void SpawnPlayer()
    {
        LevelManager.Instance.SpawnPlayer();
    }


}
