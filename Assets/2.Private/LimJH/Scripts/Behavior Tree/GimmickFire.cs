using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GimmickFire : Action
{
    /*public SharedTransform targetObject;       // 타겟 위치
    public SharedBool isGimmickActive;         // 기믹 활성화 상태 플래그
    public GameObject firePrefab;              // 불장판 프리팹
    public GameObject pillarPrefab;            // 기둥 프리팹
    public float pillarSpawnRadius = 5f; // 기둥 생성 반경

    private float originalDamageReduction = 1f; // 기존 피해 감소 비율

    public override void OnStart()
    {
        // 기믹 활성화 상태로 전환
        isGimmickActive.Value = true;

        // 피해 감소 설정 (90% 감소)
        originalDamageReduction = 0.1f; // 90% 감소

        // 타겟 위치에 불장판 생성
        if (firePrefab != null && targetObject.Value != null)
        {
            GameObject fire = Instantiate(firePrefab, targetObject.Value.position, Quaternion.identity);
            Destroy(fire, fireDuration); // 지정된 시간 후 불장판 제거
        }

        // 기둥 3개를 랜덤 위치에 생성
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = targetObject.Value.position + new Vector3(
                Random.Range(-pillarSpawnRadius, pillarSpawnRadius),
                0,
                Random.Range(-pillarSpawnRadius, pillarSpawnRadius)
            );

            if (pillarPrefab != null)
            {
                Instantiate(pillarPrefab, randomPosition, Quaternion.identity);
            }
        }
    }

    public override TaskStatus OnUpdate()
    {
        // 기믹 실행 완료
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        originalDamageReduction = 1f;
    }*/
}