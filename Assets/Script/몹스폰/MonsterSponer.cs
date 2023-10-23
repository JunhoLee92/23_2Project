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
    //private int totalMonstersToSpawnThisRound = 0;
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
        float angle = Random.Range(0, 180) * Mathf.Deg2Rad;
        Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;

        // Using the correct monster prefab from the list
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






