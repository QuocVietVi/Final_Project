using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu, GamePlay, MiddleStage, GameOver, GameWin
}

public class GameManager : Singleton<GameManager>
{
    public GameState gameState;


    public void ChangeState(GameState state)
    {
        gameState = state;
    }

    public bool IsState(GameState state)
    {
        return gameState == state;
    }
}
