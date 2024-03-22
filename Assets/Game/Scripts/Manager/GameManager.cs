using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    MainMenu, GamePlay, MiddleStage, GameOver, GameWin
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Button resume, menu;
    [SerializeField] private GameObject menuPanel;

    public GameState gameState;
    public Transform camera;
    public GameObject pausePanel;

    private bool isPaused;

    private void Start()
    {
        resume.onClick.AddListener(Resume);
        menu.onClick.AddListener(BackToMenu);
    }

    private void Update()
    {
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
        LevelManager.Instance.map.DespawnAllEnemy();
        LevelManager.Instance.map.Despawn();
        menuPanel.SetActive(true);
        ChangeState(GameState.MainMenu);
    }

}
