using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossSpawner : MonoBehaviour
{
    public Transform westPosition;
    public Transform eastPosition;
    public Transform northPosition;

    public GameObject bossPrefab;  

    private Vector3 spawnPosition;

    //Method For boss Spawn
    public void SpawnBoss()
    {
            if (SceneManager.GetActiveScene().name.Contains("InGame"))
            {
            spawnPosition = new Vector3(-8.0f,0f,0f);
            }
            else if (SceneManager.GetActiveScene().name.Contains("Chapter1"))
            {
            spawnPosition = new Vector3(-6.3f,0f,0f);
            }
       
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    
}
