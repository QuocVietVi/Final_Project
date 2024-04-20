using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<AllyAndEnemy> enemies = new List<AllyAndEnemy>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxEnemiesInGame;
    private List<AllyAndEnemy> maxEnemies = new List<AllyAndEnemy>();
    private Vector3 spawnPos;
    private float timeDelay1, timeDelay2, timeDelay3;

    public Transform playerSpawnPoint;
    public Transform allySpawnPoint;
    public Tower tower;
    public Portal portal;
    public int maxAllies;
    public int gold, gem;

    private void Start()
    {
        timeDelay1 = 3;
        InvokeRepeating(nameof(SpawnEnemy1), 4f, timeDelay1); // time 1: start sau bao lau thi goi, time 2: delay repeat 
        InvokeRepeating(nameof(SpawnEnemy2), 6f, 5f);
    }

    private void Update()
    {
        spawnPos = new Vector3(spawnPoint.position.x, Random.RandomRange(spawnPoint.position.y, spawnPoint.position.y + 0.7f), 0);
        RemoveEnemy();
        
    }

    private void RemoveEnemy()
    {
        if (maxEnemies.Count > 0)
        {
            for (int i = 0; i < maxEnemies.Count; i++)
            {
                if (maxEnemies[i] == null)
                {
                    maxEnemies.RemoveAt(i);
                    Debug.Log("remove");
                }
            }
        }
        
    }

    private void SpawnEnemy1()
    {
        //int index = maxEnemies.Count - 1;
        if (maxEnemies.Count < maxEnemiesInGame && GameManager.Instance.IsState(GameState.GamePlay) && enemies[0] != null)
        {
            //if (maxEnemies.Count < maxEnemiesInGame / 3)
            //{
            //    timeDelay1 = 3.5f;
            //}
            //else
            timeDelay1 = 4;
            AllyAndEnemy enemy = Instantiate(enemies[0], spawnPos, spawnPoint.rotation);
            enemy.OnInit();
            maxEnemies.Add(enemy);

        }
        
    }

    private void SpawnEnemy2()
    {
        if (maxEnemies.Count <= maxEnemiesInGame && GameManager.Instance.IsState(GameState.GamePlay) && enemies[1] != null)
        {
            AllyAndEnemy enemy = Instantiate(enemies[1], spawnPos, spawnPoint.rotation);
            enemy.OnInit();
            maxEnemies.Add(enemy);
        }

    }

    private void SpawnEnemy3()
    {
        if (maxEnemies.Count <= maxEnemiesInGame && GameManager.Instance.IsState(GameState.GamePlay) && enemies[2] != null)
        {
            AllyAndEnemy enemy = Instantiate(enemies[2], spawnPos, spawnPoint.rotation);
            enemy.OnInit();
            maxEnemies.Add(enemy);
        }

    }

    public void SpawnBoss()
    {

    }

    //public void DespawnAllEnemy()
    //{
    //    for (int i = 0; i < maxEnemies.Count; i++)
    //    {
    //        Destroy(maxEnemies[i].gameObject);
    //        maxEnemies.Clear();
    //    }
    //}

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
