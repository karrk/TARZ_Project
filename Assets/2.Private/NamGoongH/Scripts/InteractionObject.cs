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

        // 아이템 생성
        GameObject spawnedItem = Instantiate(prefabToSpawn, gameObject.transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        Debug.Log($"Spawned {prefabToSpawn.name}");
    }
}
