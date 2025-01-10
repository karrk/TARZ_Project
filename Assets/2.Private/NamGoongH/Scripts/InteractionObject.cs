using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private GameObject[] battleItemPrefab; // 배틀 아이템 배열

    public GameObject interationUI;

    private bool isPlayerInRange = false;
    [SerializeField] private bool isItemSpawned = false;

    [Inject] private GarbageQueue garbageQueue;

    // 애니메이션 변수
    [SerializeField] float moveDistance = 3.0f; // 이동 거리
    [SerializeField] float animationDuration = 1.0f; // 애니메이션 시간

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isItemSpawned == false)
        {
            interationUI.SetActive(true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interationUI.SetActive(false);
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
            interationUI.SetActive(false);
        }
    }

    public void Interact()
    {
        SpawnRandomItem();
        isItemSpawned = true;
    }

    private void SpawnRandomItem()
    {
        int random = Random.Range(0, battleItemPrefab.Length); // 0 = HP 아이템, 1 = 투척물 아이템
        GameObject prefabToSpawn = battleItemPrefab[random];

        // 상호작용한 물체의 앞쪽에 생성
        Vector3 spawnPosition = transform.position + transform.up * 1.5f + transform.forward * 1.0f;

        // 아이템 생성
        GameObject spawnedItem = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // 초기화 메서드 호출
        var itemComponent = spawnedItem.GetComponent<GarbageItem>();
        if (itemComponent != null)
        {
            itemComponent.Initialize(garbageQueue);
        }

        // DOTween 애니메이션 적용
        Vector3 targetPosition = spawnPosition + Vector3.up * moveDistance; // 위로 이동할 위치

        // 1. 위로 올라가기
        spawnedItem.transform.DOMove(targetPosition, animationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // 2. 원래 자리로 돌아오기
                spawnedItem.transform.DOMove(spawnPosition, animationDuration).SetEase(Ease.InQuad);
            });

        Debug.Log($"Spawned {prefabToSpawn.name}");
    }
}
