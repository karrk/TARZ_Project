using UnityEngine;
using Zenject;

public class GarbageItem : MonoBehaviour
{
    [SerializeField] private int addCount = 10;     // 추가할 개수

    public GarbageQueue garbageQueue; // GarbageQueue 참조

    public void Initialize(GarbageQueue garbageQueue)
    {
        this.garbageQueue = garbageQueue;

        if (garbageQueue == null)
        {
            Debug.LogError("GarbageQueue is null");
        }
        else
        {
            Debug.Log("GarbageQueue is initialized successfully");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (garbageQueue != null)
            {
                for (int i = 0; i < addCount; i++)
                {
                    garbageQueue.AddItem(Random.Range(1, (int)E_Garbage.Size));
                }
                Destroy(gameObject);
            }
        }
    }
}