using UnityEngine;
using Zenject;

public class PlayerGarbageCollector : MonoBehaviour
{
    public GarbageQueue garbageQueue; // GarbageQueue 참조
    [Header("Layer Settings")]
    public LayerMask garbageLayer; // 아이템에 설정된 레이어

    /// <summary>
    /// 플레이어가 투척물에 닿으면 투척물을 축적
    /// </summary>

    [Inject]
    public void Construct(GarbageQueue garbageQueue, LayerMask garbageLayer)
    {
        this.garbageQueue = garbageQueue;
        this.garbageLayer = garbageLayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 아이템 레이어에 속하는지 확인
        if (IsInLayerMask(other.gameObject, garbageLayer))
        {
            Garbage garbage = other.GetComponent<Garbage>(); // 투척물 컴포넌트 가져오기
            if (garbage != null)
            {
                garbageQueue.AddItem(garbage.garbageIndex); // 투척물의 인덱스를 큐에 추가
                Destroy(other.gameObject); // 투척물 오브젝트 제거
            }
        }
    }

    /// <summary>
    /// 특정 GameObject가 주어진 LayerMask에 포함되어 있는지 확인
    /// </summary>
    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }
}