using UnityEngine;

public class GarbageItem : MonoBehaviour
{
    [SerializeField] private int garbageIndex; // 투척물 인덱스
    [SerializeField] private int addCount = 10;     // 추가할 개수

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GarbageQueue garbageQueue = other.GetComponent<GarbageQueue>();
            if (garbageQueue != null)
            {
                for (int i = 0; i < addCount; i++)
                {
                    garbageQueue.AddItem(garbageIndex);
                }
                Destroy(gameObject);
            }
        }
    }
}