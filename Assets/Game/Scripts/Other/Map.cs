using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private List<AllyAndEnemy> enemies = new List<AllyAndEnemy>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxEnemiesInGame;
    [SerializeField] private Boss boss;
    private List<AllyAndEnemy> maxEnemies = new List<AllyAndEnemy>();
    private List<AllyAndEnemy> maxEnemiesLongRange = new List<AllyAndEnemy>();

    private Vector3 spawnPos;
    private float timeDelay1, timeDelay2, timeDelay3;
    private bool bossSpawned;

    public Transform playerSpawnPoint;
    public Transform allySpawnPoint;
    public Tower tower;
    public Portal portal;
    public int maxAllies;
    public int gold, gem;

    private void Start()
    {
        timeDelay1 = 4;
        InvokeRepeating(nameof(SpawnEnemy1), 4f, 3f); // time 1: start sau bao lau thi goi, time 2: delay repeat 
        InvokeRepeating(nameof(SpawnEnemy2), 6f, 6f);
        InvokeRepeating(nameof(SpawnEnemy3), 6f, 7f);
        bossSpawned = false;
        //SpawnAllEnemies()
        //StartCoroutine(SpawnEnemiesRepeatedly());
    }

    private void Update()
    {
        if(spawnPoint != null)
        {
            spawnPos = new Vector3(spawnPoint.position.x, Random.RandomRange(spawnPoint.position.y, spawnPoint.position.y + 0.7f), 0);
        }
        RemoveEnemy();
        if (boss != null)
        {
            if (tower.CanSpawnBoss() == true && bossSpawned == false)
            {
                SpawnBoss();
                //GameManager.Instance.ChangeState(GameState.Boss);
                bossSpawned = true;
                tower.Despawn();
            }
        }
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
        if (maxEnemiesLongRange.Count > 0)
        {
            for (int i = 0; i < maxEnemiesLongRange.Count; i++)
            {
                if (maxEnemiesLongRange[i] == null)
                {
                    maxEnemiesLongRange.RemoveAt(i);
                }
            }
        }
        
    }

    private void SpawnEnemy1()
    {
        //int index = maxEnemies.Count - 1;
        if (maxEnemies.Count < maxEnemiesInGame && GameManager.Instance.IsState(GameState.GamePlay) && enemies[0] != null && spawnPoint != null)
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
            if (maxEnemies.Count >= maxEnemiesInGame / 3 && spawnPoint != null)
            {
                AllyAndEnemy enemy = Instantiate(enemies[1], spawnPos, spawnPoint.rotation);
                enemy.OnInit();
                maxEnemies.Add(enemy);
            }
        }

    }

    private void SpawnEnemy3()
    {
        if (maxEnemies.Count <= maxEnemiesInGame && GameManager.Instance.IsState(GameState.GamePlay) && enemies[2] != null)
        {
            if (maxEnemiesLongRange.Count < 2 && spawnPoint != null)
            {
                AllyAndEnemy enemy = Instantiate(enemies[2], spawnPos, spawnPoint.rotation);
                enemy.OnInit();
                maxEnemies.Add(enemy);
                maxEnemiesLongRange.Add(enemy);
            }

        }

    }

    public void SpawnBoss()
    {
        Instantiate(boss, spawnPos, spawnPoint.rotation);
        
    }

    //IEnumerator SpawnEnemiesRepeatedly()
    //{
    //    while (true)
    //    {
    //        SpawnEnemy1();
    //        yield return new WaitForSeconds(4f);

    //        SpawnEnemy2();
    //        yield return new WaitForSeconds(5f);

    //        SpawnEnemy3();
    //        yield return new WaitForSeconds(6f);
    //    }
    //}

    //public void SpawnAllEnemies()
    //{
    //    Invoke(nameof(SpawnEnemy1), 4f);
    //    Invoke(nameof(SpawnEnemy2), 5f);
    //    Invoke(nameof(SpawnEnemy3), 4f);
    //}

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
        //this.gameObject.SetActive(false);
    }

    public void DeActive()
    {
        this.gameObject.SetActive(false);

    }
}
