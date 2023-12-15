using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public Transform westPosition;
    public Transform eastPosition;
    public Transform northPosition;

    public GameObject bossPrefab;  

    //Method For boss Spawn
    public void SpawnBoss()
    {
        Vector3 spawnPosition = new Vector3(-8.0f,0f,0f);
       
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    
}
