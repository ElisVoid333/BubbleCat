using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyController : MonoBehaviour
{
    [SerializeField] List<GameObject> SpawnPoint = new List<GameObject>();

    [SerializeField] List<GameObject> ListOfEnemies = new List<GameObject>();

    private List<int> spawnPointsUsed = new List<int>();

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        int numOfSpawnPoints = SpawnPoint.Count;
        int numOfEnemyTypes = ListOfEnemies.Count;

        for (int i = 0; i < numOfSpawnPoints; i++)
        {
            int randomEnemyType = Random.Range(0, numOfEnemyTypes);

            Instantiate(ListOfEnemies[randomEnemyType], SpawnPoint[i].transform.position, Quaternion.identity);
        }
    }
}
