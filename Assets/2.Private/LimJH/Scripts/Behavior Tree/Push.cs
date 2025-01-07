using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Push : BaseCondition
{
    private Vector3 pushPos;         // 몬스터가 밀려날 목표 위치
    private float elapsedTime;              // 경과 시간

    public override void OnStart()
    {
        base.OnStart();

        Vector3 direction = (transform.position - mob.SkillPos).normalized;
        
        // 목표 위치 계산
        pushPos = transform.position + direction * mob.Pushed.PushDist;
    }

    public override TaskStatus OnUpdate()
    {
        if (mob.SkillType != E_SkillType.MeleeSkill2)
            return TaskStatus.Failure;

        elapsedTime += Time.deltaTime;

        // 몬스터를 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, pushPos, mob.Pushed.PushSpeed * Time.deltaTime);

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, pushPos) < 0.5f || elapsedTime >= mob.Pushed.MaxDuration)
        {
            mob.ResetSkillType();
            elapsedTime = 0;
            return TaskStatus.Success;
        }

        return TaskStatus.Running; // 목표 위치에 도달할 때까지 실행
    }
}