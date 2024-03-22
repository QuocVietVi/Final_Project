using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<AllyAndEnemy> enemies = new List<AllyAndEnemy>();
    [SerializeField] private Transform spawnPoint;
    private List<AllyAndEnemy> maxEnemies = new List<AllyAndEnemy>();
    private Vector3 spawnPos;

    public Transform playerSpawnPoint;
    public Transform allySpawnPoint;
    public Tower tower;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy1), 2f, 4f);
        //InvokeRepeating(nameof(SpawnEnemy2), 4f, 3f);
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
        if (maxEnemies.Count <= 4 && GameManager.Instance.IsState(GameState.GamePlay))
        {
            AllyAndEnemy enemy = Instantiate(enemies[0], spawnPos, spawnPoint.rotation);
            enemy.OnInit();
            maxEnemies.Add(enemy);

        }
        
    }

    private void SpawnEnemy2()
    {
        AllyAndEnemy enemy = Instantiate(enemies[1] , spawnPos, spawnPoint.rotation);
        enemy.OnInit();
        maxEnemies.Add(enemy);

    }

    private void SpawnEnemy3()
    {
        AllyAndEnemy enemy = Instantiate(enemies[2] , spawnPos, spawnPoint.rotation);
        enemy.OnInit();
        maxEnemies.Add(enemy);

    }

    public void DespawnAllEnemy()
    {
        for (int i = 0; i < maxEnemies.Count; i++)
        {
            Destroy(maxEnemies[i].gameObject);
            maxEnemies.Clear();
        }
    }

    public void Despawn()
    {
        Destroy(this.gameObject);
    }
}
