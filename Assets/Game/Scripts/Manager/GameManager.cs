using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    MainMenu, GamePlay, MiddleStage, GameOver, GameWin, Restart
}

public class GameManager : Singleton<GameManager>
{
    [Header("Pause")]
    [SerializeField] private Button resume, menu, replay, setting;
    [SerializeField] private GameObject menuPanel;

    [Space(10)]
    [Header("Victory, Gameover")]
    [SerializeField] private GameObject star1, star2, star3;
    [SerializeField] private GameObject victoryPanel, gameOverPanel, settingPanel;
    //victory
    [SerializeField] private Button claim;
    //game over
    [SerializeField] private Button continueGame, backToMenu;

    [Space(10)]
    [Header("In game")]
    public List<Image> allySlots;
    public List<Image> skillSlots;
    public Sprite isLokingPic;
    public TextMeshProUGUI playerHp;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI energy;
    public TextMeshProUGUI enemyTowerHp;
    public TextMeshProUGUI portalHp;
    public List<Image> allySlotsFill;
    public TextMeshProUGUI numberAllies;
    public TextMeshProUGUI maxAllies;
    public List<TextMeshProUGUI> alliesEnergy;

    [Space(10)]
    [Header("Other")]
    public GameState gameState;
    public Transform camera;
    public GameObject pausePanel;

    public ScreenTransition screenTransition;
    public Transform screenHolder;
    public TextMeshProUGUI chapterTitle;
    private bool isPaused;
    private Player player;

    private void Start()
    {
        resume.onClick.AddListener(Resume);
        menu.onClick.AddListener(BackToMenu);
        replay.onClick.AddListener(Replay);
        claim.onClick.AddListener(Claim);
        continueGame.onClick.AddListener(Continue);
        setting.onClick.AddListener(Setting);
        backToMenu.onClick.AddListener(() =>
        {
            BackToMenu();
            gameOverPanel.SetActive(false);
        });
    }

    private void Update()
    {
        player = CameraFollow.Instance.target;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        var map = LevelManager.Instance.map;
        if (map != null)
        {
            maxAllies.text = map.maxAllies.ToString();
        }
        if (player != null)
        {
            player.SetTextAlly(numberAllies);
        }
    }

    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state)
    {
        return gameState == state;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        ButtonSoundClick();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        Destroy(CameraFollow.Instance.target.gameObject);
        LevelManager.Instance.map.Despawn();
        menuPanel.SetActive(true);
        ChangeState(GameState.MainMenu);
        ButtonSoundClick();
    }

    public void Claim()
    {
        victoryPanel.SetActive(false);
        BackToMenu();
        ButtonSoundClick();
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        ChangeState(GameState.Restart);
        Destroy(CameraFollow.Instance.target.gameObject);
        LevelManager.Instance.map.Despawn();
        Invoke(nameof(SpawnMap), 0.5f);
        Invoke(nameof(SpawnPlayer),1f);
        ButtonSoundClick();
    }

    private void SpawnPlayer()
    {
        LevelManager.Instance.SpawnPlayer();
    }

    private void SpawnMap()
    {
        LevelManager.Instance.ReplayLevel();

    }

    public void Victory()
    {
        victoryPanel.SetActive(true);
        player.StarLevel(star1, star2, star3);
        ChangeState(GameState.GameWin);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        ChangeState(GameState.GamePlay);
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        player.OnInit();
        player.Revive();
        ButtonSoundClick();
    }

    private void Setting()
    {
        settingPanel.SetActive(true); 
    }

    private void ButtonSoundClick()
    {
        SettingManager.Instance.ButtonSoundClick();
    }

    public void ImageExist()
    {
        for (int i = 0; i < allySlots.Count; i++)
        {
            if (allySlots[i] == null)
            {
                allySlots[i].sprite = isLokingPic;
            }
        }
    }

    public void SetText(TextMeshProUGUI Tmp ,float? a, float? b)
    {
        if (a != null && b != null)
        {
            Tmp.text = a + " / " + b;
        }
    }

    public void SpawnScreenTransition()
    {
        ScreenTransition screen = Instantiate(screenTransition, screenHolder);
        Destroy(screen.gameObject, 4f);
    }
}
