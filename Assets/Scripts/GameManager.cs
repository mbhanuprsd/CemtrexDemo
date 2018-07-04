using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject blueEnemy;
    public GameObject redEnemy;

    List<GameObject> enemies;

    public bool spawnEnemy = true;
    private int globalSpawnIndex;

    private void Awake()
    {
        DontDestroyOnLoad(FindObjectOfType<Light>());
    }

    private void Start()
    {
        enemies = new List<GameObject>();
        globalSpawnIndex = 0;
    }

    void Update()
    {
        if (spawnEnemy)
        {
            SpawnEnemy();
        }
        spawnEnemy = enemies.Count < 4;
        if (Math.Abs(Time.timeScale) <= 0 && Input.GetMouseButtonDown(0))
        {
            Time.timeScale = 1;
        }
    }

    private void SpawnEnemy()
    {
        int spawnIndex = RandomSpawnIndex();
        Transform spawnPoint = spawnPoints[spawnIndex];
        System.Random random = new System.Random();
        int randomEnemy = random.Next(2);
        Debug.Log(randomEnemy == 0 ? "Blue enemy spawned" : "Red enemy spawned");
        GameObject enemy = Instantiate(randomEnemy == 0 ? blueEnemy : redEnemy, spawnPoint.position, spawnPoint.rotation, null);
        enemy.GetComponent<EnemyMovement>().spawnIndex = spawnIndex;
        enemies.Add(enemy);

    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    private int RandomSpawnIndex()
    {
        return globalSpawnIndex++ < 8 ? globalSpawnIndex : 0;
    }

    public void ResetLevel()
    {
        Time.timeScale = 0;
    }
}
