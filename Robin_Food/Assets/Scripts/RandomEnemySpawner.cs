using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public float spawnCooldown = 5f;
    public float lastSpawn;
    private int randomEnemyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawn > spawnCooldown)
        {
            randomEnemyToSpawn = Random.Range(0, enemyPrefabs.Length);
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                Instantiate(enemyPrefabs[randomEnemyToSpawn], spawnPoints[i].position, Quaternion.identity);
            }
            lastSpawn = Time.time;
        }
    }
}
