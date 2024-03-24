using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    MainMenu, GamePlay, MiddleStage, GameOver, GameWin, Restart
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Button resume, menu, replay;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject star1, star2, star3;
    [SerializeField] private GameObject victoryPanel;

    public GameState gameState;
    public Transform camera;
    public GameObject pausePanel;

    private bool isPaused;
    private Player player;

    private void Start()
    {
        resume.onClick.AddListener(Resume);
        menu.onClick.AddListener(BackToMenu);
        replay.onClick.AddListener(Replay);
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
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        Destroy(CameraFollow.Instance.target.gameObject);
        LevelManager.Instance.map.Despawn();
        menuPanel.SetActive(true);
        ChangeState(GameState.MainMenu);
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
    }
}
