using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;

public class GimmickFire : BaseAction
{
    public GameObject firePrefab;              // 불장판 프리팹
    public GameObject pillarPrefab;            // 기둥 프리팹
    private GameObject fire;

    public override void OnStart()
    {
        base.OnStart();
        // 기믹 활성화 상태로 전환
        mob.Stat.isGimmickActive = true;

        // 피해 감소 설정 (90% 감소)
        mob.Stat.DamageReducation = 0.1f;

        // 타겟 위치에 불장판 생성
        if (firePrefab != null && mob != null)
        {
            Vector3 spawnPosition = mob.transform.position;
            spawnPosition.y = 0;

            fire = GameObject.Instantiate(firePrefab, spawnPosition, Quaternion.identity);
        }

        mob.Stat.pillarStates = new List<int> { 0, 0, 0 };

        // 기둥 3개를 랜덤 위치에 생성
        for (int i = 0; i < 3; i++)
        {
            Vector3 randomPosition = mob.transform.position + new Vector3(
                Random.Range(-mob.Stat.pillarSpawnRadius, mob.Stat.pillarSpawnRadius),
                0,
                Random.Range(-mob.Stat.pillarSpawnRadius, mob.Stat.pillarSpawnRadius)
            );

            if (pillarPrefab != null)
            {
                GameObject pillar = GameObject.Instantiate(pillarPrefab, randomPosition, Quaternion.identity);

                // FirePillar 스크립트에서 mob 참조 설정
                FirePillar firePillar = pillar.GetComponent<FirePillar>();
                if (firePillar != null)
                {
                    firePillar.mob = mob; // mob 객체 전달
                    firePillar.pillarIndex = i; // 인덱스 설정
                }

                mob.Stat.pillarStates[i] = 1;
            }
        }

    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        /*if (fire != null)
        {
            GameObject.Destroy(fire);
        }*/
        //mob.Stat.DamageReducation = 1f; // 데미지 감소율 정상화
    }
}