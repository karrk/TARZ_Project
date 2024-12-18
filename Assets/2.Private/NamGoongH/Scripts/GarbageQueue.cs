using System.Collections.Generic;
using UnityEngine;

public class GarbageQueue : MonoBehaviour
{
    [Header("Available Item Prefabs")]
    public GameObject[] garbagePrefabs; // 투척물 프리팹 배열

    [SerializeField]
    private List<int> garbageIndexList = new List<int>(); // 인덱스를 저장하는 리스트 (FIFO)
    //private Queue<int> garbageIndexQueue = new Queue<int>(); // 아이템 인덱스를 저장하는 큐

    // 아이템 인덱스 추가
    public void AddItem(int garbageIndex)
    {
        if (garbageIndex >= 0 && garbageIndex < garbagePrefabs.Length)
        {
            garbageIndexList.Add(garbageIndex); // 리스트 끝에 인덱스 추가
            //garbageIndexQueue.Enqueue(garbageIndex); // 큐에 인덱스 추가
            Debug.Log($"Item index added: {garbageIndex}. Queue size: {garbageIndexList.Count}");
            PrintQueue();
        }
        else
        {
            Debug.LogWarning($"Invalid item index: {garbageIndex}");
        }
    }

    // 다음 아이템을 가져와서 프리팹을 반환
    public GameObject GetNextItemPrefab()
    {
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
        }
        else
        {
            Debug.LogWarning("Queue is empty.");
        }
        return null;
    }

    // 디버깅용 리스트 출력
    private void PrintQueue()
    {
        string queueContent = "Current Queue: ";
        foreach (var index in garbageIndexList)
        {
            queueContent += index + " -> ";
        }
        Debug.Log(queueContent + "END");
    }
}
