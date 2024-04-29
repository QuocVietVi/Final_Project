using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelSO levelSO;
    [SerializeField] private LevelButtonAction levelButton;
    [SerializeField] private Transform buttonHolder1, buttonHolder2, buttonHolder3;
    [SerializeField] private Button prev, next;
    [SerializeField] private Button back;
    [SerializeField] private List<GameObject> panelChapter;
    [SerializeField] private GameObject homePanel;

    private List<LevelButtonAction> buttons = new List<LevelButtonAction>();
    public GameObject levelPanel;
    public Map map;
    public Player player;
    public int currentLevel;
    public Chapter currentChapter;
    public int stars;
    public int starsWin;
    public int golds;
    public int gems;

    private void Start()
    {
        currentChapter = Chapter.Chapter1;
        // Button onclick

        next.onClick.AddListener(Next);
        prev.onClick.AddListener(Prev);
        back.onClick.AddListener(BackToMenu);

        // End button onclick

        // Spawn button
        //SpawnAllButton();
    }

    public void SpawnButton(Chapter chapter, Transform buttonHolder)
    {
        if (levelSO.levels[(int)chapter].chapter == chapter) // tim chapter hien tai trong SO
        {
            
            for (int i = 0; i < levelSO.levels[(int)chapter].levelInfo.Count; i++) //duyet list level cua chapter do
            {
                LevelButtonAction button = LeanPool.Spawn(levelButton, buttonHolder);
                var levelInfo = levelSO.levels[(int)chapter].levelInfo[i];
                button.levelText.text = levelSO.levels[(int)chapter].levelInfo[i].level.ToString();
                button.SetOnClick((int)levelSO.levels[(int)chapter].chapter +1,
                    levelInfo.level, levelInfo.stars, levelInfo.golds, levelInfo.gems);
                button.levelPanel = this.levelPanel;
                buttons.Add(button);
            }
        }
    }
    public void SpawnAllButton()
    {
        SpawnButton(Chapter.Chapter1, buttonHolder1);
        SpawnButton(Chapter.Chapter2, buttonHolder2);
        SpawnButton(Chapter.Chapter3, buttonHolder3);
    }
    public void DespawnAllButton()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i].gameObject);
        }
        buttons.Clear();
    }
    public void SetResources(Chapter chapter)
    {
        if (levelSO.levels[(int)chapter].chapter == chapter) 
        {
            //levelSO.levels[(int)chapter].stars[currentLevel-1] = this.stars;
            var levelData = levelSO.levels[(int)chapter].levelInfo[currentLevel - 1];
            levelData.stars = this.stars;
            levelData.gems = this.gems;
            levelData.golds = this.golds;
        }
    }

    private void Next()
    {
        DeActive(panelChapter[(int)currentChapter]);
        currentChapter =(Chapter)(int)currentChapter + 1;
        Active(panelChapter[(int)currentChapter]);
        SetArrowActive();
        ButtonSoundClick();
    }

    private void Prev()
    {
        DeActive(panelChapter[(int)currentChapter]);
        currentChapter = (Chapter)(int)currentChapter - 1;
        Active(panelChapter[(int)currentChapter]);
        SetArrowActive();
        ButtonSoundClick();
    }
    //public void Despawn()
    //{
    //    for (int i = 0; i < buttons.Count; i++)
    //    {
    //        LeanPool.Despawn(buttons[i].gameObject);
    //    }
    //    buttons.Clear();
    //}
    public void Active(GameObject chapter)
    {
        chapter.SetActive(true);
    }

    public void DeActive(GameObject chapter)
    {
        chapter.SetActive(false);
    }

    public void SpawnButton1()
    {
        if (currentChapter == Chapter.Chapter1)
        {
            SpawnButton(currentChapter, buttonHolder1);
            
        }
    }

    public void SpawnButton2()
    {
        if (currentChapter == Chapter.Chapter2)
        {
            SpawnButton(currentChapter, buttonHolder2);
            //prev.gameObject.SetActive(true);
            //next.gameObject.SetActive(true);
        }
    }

    public void SpawnButton3()
    {
        if (currentChapter == Chapter.Chapter3)
        {
            SpawnButton(currentChapter, buttonHolder3);
            //next.gameObject.SetActive(false);
        }
    }

    public void SpawnPlayer()
    {
        Instantiate(player, map.playerSpawnPoint.position, Quaternion.Euler(Vector3.zero));
        GameManager.Instance.ChangeState(GameState.GamePlay);
        CameraFollow.Instance.FindPlayer();

    }

    private void SetArrowActive()
    {
        prev.gameObject.SetActive((int)currentChapter > 0);
        next.gameObject.SetActive((int)currentChapter < panelChapter.Count - 1);
    }

    public void FindMap()
    {
        map = FindObjectOfType<Map>();
    }

    private void Replay(int chapter, int level)
    {
        map = Instantiate(Resources.Load<Map>(Constant.MAP_NAME + chapter + "-" + level));
    }

    public void ReplayLevel()
    {
        Replay((int)currentChapter + 1, currentLevel);
    }

    private void BackToMenu()
    {
        levelPanel.SetActive(false);
        homePanel.SetActive(true);
        ButtonSoundClick();
        DespawnAllButton();
    }

    private void ButtonSoundClick()
    {
        SettingManager.Instance.ButtonSoundClick();
    }

    public void SetTextCurrentChapter(int chapter, int level)
    {
        GameManager.Instance.screenTransition.chapter.text = "Chapter " + chapter.ToString() + "-" + level.ToString();
    }

    public void ScreenTransition()
    {
        var game = GameManager.Instance;
        game.SpawnScreenTransition();
    }
}
