using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Push : BaseAction
{
    //public SharedVector3 playerVector;
    //public SharedInt skillType;

    //public float pushDistance = 5f;         // 밀려날 거리
    //public float pushSpeed = 10f;            // 밀려나는 속도
    //public float maxDuration = 1f;          // 최대 지속 시간

    private Vector3 pushPos;         // 몬스터가 밀려날 목표 위치
    private float elapsedTime;              // 경과 시간

    public override void OnStart()
    {
        base.OnStart();
        // 플레이어 위치와 몬스터 위치를 기준으로 방향 계산
        Vector3 monsterPosition = transform.position;//selfObject로 수정(?)
        Vector3 direction = (monsterPosition - mob.SkillPos).normalized;
        //Vector3 direction = (monsterPosition - targetObject.Value.transform.position).normalized;

        // 목표 위치 계산
        pushPos = monsterPosition + direction * mob.Pushed.PushDist;
    }

    public override TaskStatus OnUpdate()
    {
        elapsedTime += Time.deltaTime;

        // 몬스터를 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, pushPos,
            mob.Pushed.PushSpeed * Time.deltaTime);

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

//using UnityEngine;
//using BehaviorDesigner.Runtime;
//using BehaviorDesigner.Runtime.Tasks;

//public class Push : BaseCondition
//{
//    private Vector3 pushPos;         // 몬스터가 밀려날 목표 위치
//    private float elapsedTime;              // 경과 시간

//    public override void OnStart()
//    {
//        base.OnStart();

//        Vector3 direction = (transform.position - mob.SkillPos).normalized;

//        // 목표 위치 계산
//        pushPos = transform.position + direction * mob.Pushed.PushDist;
//    }

//    public override TaskStatus OnUpdate()
//    {
//        if (mob.SkillType != E_SkillType.MeleeSkill2)
//            return TaskStatus.Failure;

//        elapsedTime += Time.deltaTime;

//        // 몬스터를 목표 위치로 이동
//        transform.position = Vector3.MoveTowards(transform.position, pushPos, mob.Pushed.PushSpeed * Time.deltaTime);

//        // 목표 지점에 도달했는지 확인
//        if (Vector3.Distance(transform.position, pushPos) < 0.5f || elapsedTime >= mob.Pushed.MaxDuration)
//        {
//            mob.ResetSkillType();
//            elapsedTime = 0;
//            return TaskStatus.Success;
//        }

//        return TaskStatus.Running; // 목표 위치에 도달할 때까지 실행
//    }
//}