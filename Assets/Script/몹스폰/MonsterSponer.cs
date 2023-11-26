using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static GameManager;

public class MonsterSponer : MonoBehaviour
{

   
    public float spawnRadius = 10f;
    public float spawnInterval;
    public int monstersPerSpawn;
    public List<GameObject> monsterPrefabs;

    private float nextSpawnTime;
    public bool spawnStarted = false;
    private GameManager gameManager;
    public int totalSpawnedCount = 0;
    public int currentMonsters = 0;
   
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        
        if (spawnStarted && Time.time >= nextSpawnTime)
        {
            RoundConfig currentConfig = gameManager.GetCurrentRoundConfig();

            // Here, we get the next spawn info to spawn the monster from
            MonsterSpawnInfo spawnInfo = GetNextSpawnInfo(currentConfig);
            if (spawnInfo != null)
            {
                SpawnMonster(spawnInfo);
                nextSpawnTime = Time.time + spawnInterval;
            }
            else
            {
                // If no spawnInfo is returned, it means all monsters are spawned for this round
                spawnStarted = false;
            }
        }



    }



    void SpawnMonster(MonsterSpawnInfo spawnInfo)
    {
        
        // Calculate the boundaries of the screen in world coordinates
        float topBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0)).y;
        float leftBoundary = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).x;
        float rightBoundary = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        float middleVertical = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)).y;

        Vector3 spawnPosition;

        // Randomly choose a boundary (0: top, 1: left, 2: right)
        int chosenBoundary = Random.Range(0, 3);

        switch (chosenBoundary)
        {
            case 0: // Top boundary
                spawnPosition = new Vector3(Random.Range(leftBoundary + 0.5f, rightBoundary - 0.5f), topBoundary - 0.5f, 0);
                break;
            case 1: // Left boundary (upper half)
                spawnPosition = new Vector3(leftBoundary + 0.5f, Random.Range(middleVertical, topBoundary - 0.5f), 0);
                break;
            case 2: // Right boundary (upper half)
                spawnPosition = new Vector3(rightBoundary - 0.5f, Random.Range(middleVertical, topBoundary - 0.5f), 0);
                break;
            default:
                spawnPosition = Vector3.zero;
                break;
        }

        GameObject spawnedMonster = Instantiate(monsterPrefabs[(int)spawnInfo.monsterType], spawnPosition, Quaternion.identity);

        spawnedMonster.GetComponent<MonsterController>().Hp = spawnInfo.Hp;
        spawnedMonster.GetComponent<MonsterController>().speed = spawnInfo.speed;
        gameManager.OnMonsterSpawned();
        
    }
    MonsterSpawnInfo GetNextSpawnInfo(RoundConfig config)
    {
        List<MonsterSpawnInfo> availableSpawnInfos = new List<MonsterSpawnInfo>();
        
        // Gather all available spawn infos
        foreach (MonsterSpawnInfo spawnInfo in config.spawnInfos)
        {
            if (spawnInfo.count > 0)
            {
                availableSpawnInfos.Add(spawnInfo);
            }
        }

        if (availableSpawnInfos.Count == 0)
        {
            return null; // Return null if all spawnInfos are exhausted
        }

        // Select a random spawn info from the available ones
        MonsterSpawnInfo selectedSpawnInfo = availableSpawnInfos[Random.Range(0, availableSpawnInfos.Count)];
        selectedSpawnInfo.count--;

        return selectedSpawnInfo;
    }


}






