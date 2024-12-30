using UnityEngine;

public class HpRecoveryItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어의 체력을 회복하는 기능

            Destroy(gameObject);
            
        }
    }
}