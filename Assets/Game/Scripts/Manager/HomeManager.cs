using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField] private Button play;
    [SerializeField] private Button tapToStart;
    [SerializeField] private GameObject startScene, homeScene;
    private void Start()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        play.onClick.AddListener(SpawnPlayer);
        tapToStart.onClick.AddListener(CloseStartPanel);
    }

    private void SpawnPlayer()
    {
        LevelManager.Instance.SpawnPlayer();
        homeScene.SetActive(false);
    }

    private void CloseStartPanel()
    {
        startScene.SetActive(false);
        homeScene.SetActive(true);
    }

}