using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlatform : MonoBehaviour
{
    public float damagePerSecond = 1f; // 초당 데미지
    private PlayerController playerController;
    private Coroutine damageCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // 데미지 코루틴 시작
                damageCoroutine = StartCoroutine(ApplyDamage());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 데미지 코루틴 중지
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            playerController = null; // 플레이어 참조 해제
        }
    }

    private IEnumerator ApplyDamage()
    {
        while (true)
        {
            if (playerController != null)
            {
                playerController.TakeDamage(damagePerSecond); // 데미지 적용
            }
            yield return new WaitForSeconds(1f); // 1초 간격
        }
    }
}
