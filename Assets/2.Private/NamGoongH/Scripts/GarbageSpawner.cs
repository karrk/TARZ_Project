using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarbageSpawner : MonoBehaviour
{
    [Inject] private PoolManager manager;
    public Transform[] spawnPoints;

    private void Start()
    {
        StartCoroutine(WaitSpawn());
    }

    private IEnumerator WaitSpawn()
    {
        while (true)
        {
            if (manager.IsInited == true)
                break;

            yield return new WaitForSeconds(0.2f);
        }

        SpawnGarbages();
    }

    /// <summary>
    /// 투척물 소환
    /// </summary>
    private void SpawnGarbages()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            int randomIndex = Random.Range((int)E_Garbage.Test2, (int)E_Garbage.Size);
            GameObject obj = manager.GetObject((E_Garbage)randomIndex);
            obj.transform.position = spawnPoint.position;
        }
    }
}