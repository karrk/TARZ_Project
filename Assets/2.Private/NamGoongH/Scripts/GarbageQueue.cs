using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarbageQueue
{

    [Header("Available Item Prefabs")]
    private GameObject[] garbagePrefabs; // 투척물 프리팹 배열

    [SerializeField]
    private List<int> garbageIndexList = new List<int>(); // 인덱스를 저장하는 리스트 (FIFO)
    //private Queue<int> garbageIndexQueue = new Queue<int>(); // 투척물 인덱스를 저장하는 큐

    public event Action ChangedInventory;
    public int Count => garbageIndexList.Count;

    [Inject]
    public GarbageQueue(ProjectInstaller.GarbagePrefab garbagePrefabs)
    {
        this.garbagePrefabs = garbagePrefabs.Garbages;
    }

    /// <summary>
    /// 리스트에 투척물 인덱스 추가
    /// </summary>
    public void AddItem(int garbageIndex)
    {
        if (garbagePrefabs == null || garbagePrefabs.Length == 0)
        {
            Debug.LogWarning("Garbage prefabs array is not set or empty.");
            return;
        }

        if (garbageIndex >= 0 && garbageIndex < garbagePrefabs.Length)
        {
            garbageIndexList.Add(garbageIndex); // 리스트 끝에 인덱스 추가
            //garbageIndexQueue.Enqueue(garbageIndex); // 큐에 인덱스 추가
            Debug.Log($"Item index added: {garbageIndex}. Queue size: {garbageIndexList.Count}");
            PrintQueue();

            ChangedInventory?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Invalid item index: {garbageIndex}");
        }
    }

    /// <summary>
    /// 다음 투척물을 가져와서 프리팹을 반환
    /// </summary>
    public GameObject GetNextGarbagePrefab()
    {
        if (garbagePrefabs == null || garbagePrefabs.Length == 0)
        {
            Debug.LogWarning("Garbage prefabs array is not set or empty.");
            return null;
        }

        if (garbageIndexList.Count > 0)
        {
            int garbageIndex = garbageIndexList[0]; // 첫 번째 인덱스 가져오기
            garbageIndexList.RemoveAt(0); // 리스트에서 제거
            //int garbageIndex = garbageIndexQueue.Dequeue(); // FIFO 방식으로 인덱스 가져오기
            //Debug.Log($"Garbage index dequeued: {garbageIndex}. Remaining size: {garbageIndexQueue.Count}");

            Debug.Log($"Item index removed: {garbageIndex}. Remaining size: {garbageIndexList.Count}");

            // 인덱스에 해당하는 프리팹 반환
            if (garbageIndex >= 0 && garbageIndex < garbagePrefabs.Length)
            {
                return garbagePrefabs[garbageIndex];
            }
            else
            {
                Debug.LogWarning($"Invalid index in list: {garbageIndex}");
            }

            ChangedInventory?.Invoke();
        }
        else
        {
            Debug.LogWarning("Queue is empty.");
        }
        return null;
    }

    /// <summary>
    /// 디버깅용 리스트 출력
    /// </summary>
    #if UNITY_EDITOR
    private void PrintQueue()
    {
        string queueContent = "Current Queue: ";
        foreach (var index in garbageIndexList)
        {
            queueContent += index + " -> ";
        }
        Debug.Log(queueContent + "END");
    }
    #endif
}
