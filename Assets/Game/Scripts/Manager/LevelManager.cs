using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Map map;
    public Player player;

    public void SpawnPlayer()
    {
        Instantiate(player, map.playerSpawnPoint.position, Quaternion.Euler(Vector3.zero));
        GameManager.Instance.ChangeState(GameState.GamePlay);
        CameraFollow.Instance.FindPlayer();

    }
}
