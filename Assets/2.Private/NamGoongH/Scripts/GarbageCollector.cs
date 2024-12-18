using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public GarbageQueue garbageQueue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Garbage garbage = other.GetComponent<Garbage>(); // 아이템 스크립트에서 인덱스 가져옴
            if (garbage != null)
            {
                garbageQueue.AddItem(garbage.garbageIndex); // 인덱스를 큐에 추가
                Destroy(other.gameObject); // 아이템 오브젝트 제거
            }
        }
    }
}
