using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu, GamePlay, MiddleStage, GameOver, GameWin
}

public class GameManager : Singleton<GameManager>
{
    public Map map;
}
