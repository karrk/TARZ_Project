using UnityEngine;

public class Garbage : MonoBehaviour
{
    public int garbageIndex; // 투척물의 인덱스

    private void Start()
    {
        // 플레이어 콜라이더 가져오기
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Collider playerCollider = player.GetComponent<Collider>();
            Collider garbageCollider = GetComponent<Collider>();

            // 플레이어와 투척물 간의 충돌 무시
            if (playerCollider != null && garbageCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, garbageCollider);
            }
        }
    }
}
