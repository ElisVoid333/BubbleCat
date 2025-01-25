using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class EnemySpawnLocation : MonoBehaviour
{
    [SerializeField] List<GameObject> PotentialSpawn=new List<GameObject>();

    [SerializeField] int NumberSpawn = 1;

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {

        for (int i = 0; i < NumberSpawn; i++)
        {
            int spawnable = Random.Range(1, PotentialSpawn.Count);
            Instantiate(PotentialSpawn[spawnable - 1],this.gameObject.transform);
        }
        
    }

    public void ResetLevel()
    {
        DestroyChildren();
        SpawnEnemies();
    }


    public void DestroyChildren()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child);
        }

    }

}
