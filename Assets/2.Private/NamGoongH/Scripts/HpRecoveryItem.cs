using UnityEngine;

public class HpRecoveryItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어의 PlayerStats 컴포넌트를 가져옴
            ProjectPlayer player = other.GetComponent<ProjectPlayer>();
            if (player != null)
            {
                // 플레이어의 체력을 회복
                player.TakeHit(-healAmount);
            }

            // 아이템 파괴
            Destroy(gameObject);
        }
    }
}


