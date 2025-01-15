using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirePillar : MonoBehaviour
{
    public float damagePerSecond = 1f; // 초당 데미지
    private ProjectPlayer playerController;
    private Coroutine damageCoroutine;
    [SerializeField] int PillarStack = 0;

    public BaseMonster mob;
    public int pillarIndex; // 기둥 인덱스 추가

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Garbage"))
        {
            Garbage garbage = other.GetComponent<Garbage>();
            PillarStack++;
            if (PillarStack >= 3)
            {
                UpdatePillarState();
                Destroy(gameObject);
                //파괴 이펙트 추가 가능
            }
            garbage.Return();
        }

        if (other.CompareTag("Player"))
        {
            playerController = other.GetComponent<ProjectPlayer>();
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
                playerController.TakeHit(damagePerSecond); // 데미지 적용
            }
            yield return new WaitForSeconds(1f); // 1초 간격
        }
    }

   /* public void TakeHit(float value, bool chargable = false)
    {
        PillarStack++;
        if (PillarStack >= 3)
        {
            UpdatePillarState();
            Destroy(gameObject);
            //파괴 이펙트 추가 가능
        }
    }*/

    private void UpdatePillarState()
    {
        if (mob != null && mob.Stat != null && pillarIndex >= 0 && pillarIndex < mob.Stat.pillarStates.Count)
        {
            mob.Stat.pillarStates[pillarIndex] = 2; // 정확한 인덱스 업데이트
        }
    }
}
