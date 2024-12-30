using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private GameObject[] battleItemPrefab; // 배틀 아이템 배열

    public void Interact()
    {
        SpawnRandomItem();
    }

    private void SpawnRandomItem()
    {
        int random = Random.Range(0, battleItemPrefab.Length); // 0 = HP 아이템, 1 = 투척물 아이템
        GameObject prefabToSpawn = battleItemPrefab[random];

        // 상호작용한 물체의 앞쪽에 생성
        Vector3 spawnPosition = transform.position + transform.forward * 1.5f;

        // 아이템 생성
        GameObject spawnedItem = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log($"Spawned {prefabToSpawn.name}");
    }
}
