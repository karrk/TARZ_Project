using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarbageQueue
{
    [SerializeField]
    private List<E_Garbage> garbageIndexList = new List<E_Garbage>(); // 인덱스를 저장하는 리스트 (FIFO)

    [Inject] private PlayerStats playerStats;

    public event Action ChangedInventoryCount;

    public int CurCount => garbageIndexList.Count;
    public int Size => garbageIndexList.Capacity;

    /// <summary>
    /// 리스트에 투척물 인덱스 추가
    /// </summary>
    public void AddItem(int garbageIndex)
    {
        if (garbageIndex >= 0 && garbageIndex < (int)E_Garbage.Size)
        {
            garbageIndexList.Add((E_Garbage)garbageIndex); // 리스트 끝에 인덱스 추가
            //garbageIndexQueue.Enqueue(garbageIndex); // 큐에 인덱스 추가
            Debug.Log($"Item index added: {garbageIndex}. Queue size: {garbageIndexList.Count}");
            PrintQueue();

            ChangedInventoryCount?.Invoke();
        }
        else
        {
            Debug.LogWarning($"Invalid item index: {garbageIndex}");
        }
    }

    /// <summary>
    /// 다음 투척물을 가져와서 프리팹을 반환
    /// </summary>
    public E_Garbage GetNextGarbageIndex()
    {
        if (garbageIndexList.Count > 0)
        {
            E_Garbage garbageIndex = garbageIndexList[0]; // 첫 번째 인덱스 가져오기
            garbageIndexList.RemoveAt(0); // 리스트에서 제거

            Debug.Log($"Item index removed: {garbageIndex}. Remaining size: {garbageIndexList.Count}");

            // 인덱스에 해당하는 프리팹 반환
            if (garbageIndex > E_Garbage.Basic && garbageIndex < E_Garbage.Size)
            {
                ChangedInventoryCount?.Invoke();
                return garbageIndex;
            }
            else
            {
                Debug.LogWarning($"Invalid index in list: {garbageIndex}");
            }
        }
        return E_Garbage.Basic;
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
