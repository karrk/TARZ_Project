using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEditor.Experimental.GraphView;

public class GimmickFire : Action
{
    public SharedGameObject targetObject;       // 타겟 위치
    public SharedBool isGimmickActive;         // 기믹 활성화 상태 플래그
    public GameObject firePrefab;              // 불장판 프리팹
    public GameObject pillarPrefab;            // 기둥 프리팹
    public float pillarSpawnRadius = 5f; // 기둥 생성 반경

    private float originalDamageReduction = 1f; // 기존 피해 감소 비율

    public GameObject fire;

    public SharedGameObjectList pillarList;  // 기둥 리스트

    public override void OnStart()
    {
        // 기믹 활성화 상태로 전환
        isGimmickActive.Value = true;

        // 피해 감소 설정 (90% 감소)
        originalDamageReduction = 0.1f; // 90% 감소

        // 타겟 위치에 불장판 생성
        if (firePrefab != null && targetObject.Value != null)
        {
            Vector3 spawnPosition = targetObject.Value.transform.position;
            spawnPosition.y = 0;

            fire = GameObject.Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        }


        // 기둥 3개를 랜덤 위치에 생성
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = targetObject.Value.transform.position + new Vector3(
                Random.Range(-pillarSpawnRadius, pillarSpawnRadius),
                0,
                Random.Range(-pillarSpawnRadius, pillarSpawnRadius)
            );

            if (pillarPrefab != null)
            {
                GameObject pillar = GameObject.Instantiate(pillarPrefab, randomPosition, Quaternion.identity);
                pillarList.Value.Add(pillar);
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
        //GameObject.Destroy(fire);
        isGimmickActive.Value = true;
    }
}