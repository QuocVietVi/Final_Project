using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private LevelSO levelSO;
    [SerializeField] private LevelButtonAction levelButton;
    [SerializeField] private Transform buttonHolder1, buttonHolder2, buttonHolder3;
    [SerializeField] private Chapter currentChapter;
    [SerializeField] private Button prev, next;
    [SerializeField] private List<GameObject> panelChapter;

    private List<LevelButtonAction> buttons = new List<LevelButtonAction>();
    public GameObject levelPanel;
    public Map map;
    public Player player;

    private void Start()
    {
        currentChapter = Chapter.Chapter1;
        next.onClick.AddListener(Next);
        prev.onClick.AddListener(Prev);
        SpawnButton(Chapter.Chapter1, buttonHolder1);
        SpawnButton(Chapter.Chapter2, buttonHolder2);
        SpawnButton(Chapter.Chapter3, buttonHolder3);
    }

    public void SpawnButton(Chapter chapter, Transform buttonHolder)
    {
        if (levelSO.levels[(int)chapter].chapter == chapter) // tim chapter hien tai trong SO
        {
            for (int i = 0; i < levelSO.levels[(int)chapter].level.Count; i++) //duyet list level cua chapter do
            {
                LevelButtonAction button = LeanPool.Spawn(levelButton, buttonHolder);
                button.levelText.text = levelSO.levels[(int)chapter].level[i].ToString();
                button.SetOnClick((int)levelSO.levels[(int)chapter].chapter, levelSO.levels[(int)chapter].level[i]);
                button.levelPanel = this.levelPanel;
                buttons.Add(button);
            }
        }
    }

    private void Next()
    {
        DeActive(panelChapter[(int)currentChapter]);
        currentChapter =(Chapter)(int)currentChapter + 1;
        Active(panelChapter[(int)currentChapter]);
        SetArrowActive();
    }

    private void Prev()
    {
        DeActive(panelChapter[(int)currentChapter]);
        currentChapter = (Chapter)(int)currentChapter - 1;
        Active(panelChapter[(int)currentChapter]);
        SetArrowActive();
    }

    public void Despawn()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            LeanPool.Despawn(buttons[i].gameObject);
        }
        buttons.Clear();
    }

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
}
