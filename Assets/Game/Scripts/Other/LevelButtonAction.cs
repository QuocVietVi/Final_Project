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
    public void SetOnClick(int chapter, int level)
    {
        levelButton.onClick.AddListener(() =>
        {
            SpawnMap(chapter,level);
        });
    }

    private void SpawnMap(int chapter, int level)
    {
        map = Resources.Load<Map>(Constant.MAP_NAME + chapter + "-" + level);
        Instantiate(map);
        levelPanel.SetActive(false);
        Invoke(nameof(SpawnPlayer), 0.5f);
    }

    private void SpawnPlayer()
    {
        LevelManager.Instance.SpawnPlayer();
    }
}
