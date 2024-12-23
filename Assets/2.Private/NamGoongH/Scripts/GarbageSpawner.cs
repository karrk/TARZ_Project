using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarbageSpawner : MonoBehaviour
{
    
    public GameObject[] garbagePrefabs;
    public Transform[] spawnPoints;

    [Inject]
    public void Construct(ProjectInstaller.GarbagePrefab garbagePrefabs)
    {
        this.garbagePrefabs = garbagePrefabs.Garbages;
    }

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
            int randomIndex = Random.Range(1, garbagePrefabs.Length);
            Instantiate(garbagePrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        }
    }
}