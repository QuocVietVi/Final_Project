using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<AllyAndEnemy> enemies = new List<AllyAndEnemy>();
    private List<AllyAndEnemy> maxEnemies = new List<AllyAndEnemy>();
    [SerializeField] private Transform spawnPoint;
    private Vector3 spawnPos;

    private void Start()
    {
        //InvokeRepeating(nameof(SpawnEnemy1), 2f, 2f);
        //InvokeRepeating(nameof(SpawnEnemy2), 4f, 3f);
    }

    private void Update()
    {
        spawnPos = new Vector3(spawnPoint.position.x, Random.RandomRange(spawnPoint.position.y, spawnPoint.position.y + 0.7f), 0);
    }

    private void SpawnEnemies()
    {
        
    }

    private void SpawnEnemy1()
    {
        if (maxEnemies.Count <= 10)
        {
            AllyAndEnemy enemy = LeanPool.Spawn(enemies[0], spawnPos, spawnPoint.rotation);
            maxEnemies.Add(enemy);
        }
        
    }

    private void SpawnEnemy2()
    {
        AllyAndEnemy enemy = LeanPool.Spawn(enemies[1] , spawnPos, spawnPoint.rotation);
        maxEnemies.Add(enemy);

    }

    private void SpawnEnemy3()
    {
        AllyAndEnemy enemy = LeanPool.Spawn(enemies[2] , spawnPos, spawnPoint.rotation);
        maxEnemies.Add(enemy);

    }
}
