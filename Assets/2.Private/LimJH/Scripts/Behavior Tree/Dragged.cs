using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

public class Dragged : BaseCondition
{
    private float elapsedTime;              // 경과 시간

    public override TaskStatus OnUpdate()
    {
        if (mob.SkillType != E_SkillType.MeleeSkill1)
        {
            return TaskStatus.Failure;
        }

        elapsedTime += Time.deltaTime;

        // 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            mob.SkillPos,
            mob.Drag.GatherSpeed * Time.deltaTime
        );

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, mob.SkillPos) <= mob.Drag.GatherRad || elapsedTime >= mob.Drag.MaxDuration)
        {
            mob.ResetSkillType();
            elapsedTime = 0;
            return TaskStatus.Success; // 목표 지점에 도달하면 성공
        }

        return TaskStatus.Running; // 목표 지점으로 이동 중
    }
}