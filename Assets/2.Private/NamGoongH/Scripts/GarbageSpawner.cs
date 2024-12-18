using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    public GameObject[] garbagePrefabs;
    public Transform[] spawnPoints;

    private void Start()
    {
        SpawnGarbages();
    }

    /// <summary>
    /// 투척물 소환
    /// </summary>
    private void SpawnGarbages()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range(0, garbagePrefabs.Length);
            Instantiate(garbagePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        }
    }
}