using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Dragged : BaseAction
{
    //public SharedVector3 targetObject;   // 몬스터를 모을 포인트
    //public SharedInt skillType;

    //public float maxDuration = 1f;          // 최대 지속 시간
    //public float gatherSpeed = 10f;          // 모이는 속도
    //public float gatherRadius = 1f;         // 목표 지점에 도달했다고 간주하는 거리
    private float elapsedTime;              // 경과 시간

    public override TaskStatus OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        //if (targetObject.Value == null)
        //{
        //    return TaskStatus.Failure;
        //}

        // 목표 지점으로 이동
        transform.position = Vector3.MoveTowards(
            transform.position,
            mob.SkillPos,
            mob.Drag.GatherSpeed * Time.deltaTime
        );

        // 목표 지점에 도달했는지 확인
        if (Vector3.Distance(transform.position, mob.SkillPos) 
            <= mob.Drag.GatherRad || elapsedTime >= mob.Drag.MaxDuration)
        {
            mob.ResetSkillType();
            elapsedTime = 0;
            return TaskStatus.Success; // 목표 지점에 도달하면 성공
        }

        return TaskStatus.Running; // 목표 지점으로 이동 중
    }
}

//using UnityEngine;
//using BehaviorDesigner.Runtime.Tasks;

//public class Dragged : BaseCondition
//{
//    private float elapsedTime;              // 경과 시간

//    public override TaskStatus OnUpdate()
//    {
//        if (mob.SkillType != E_SkillType.MeleeSkill1)
//        {
//            return TaskStatus.Failure;
//        }

//        elapsedTime += Time.deltaTime;

//        // 목표 지점으로 이동
//        transform.position = Vector3.MoveTowards(
//            transform.position,
//            mob.SkillPos,
//            mob.Drag.GatherSpeed * Time.deltaTime
//        );

//        // 목표 지점에 도달했는지 확인
//        if (Vector3.Distance(transform.position, mob.SkillPos) <= mob.Drag.GatherRad || elapsedTime >= mob.Drag.MaxDuration)
//        {
//            mob.ResetSkillType();
//            elapsedTime = 0;
//            return TaskStatus.Success; // 목표 지점에 도달하면 성공
//        }

//        return TaskStatus.Running; // 목표 지점으로 이동 중
//    }
//}