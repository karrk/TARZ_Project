using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;

public class rangedAttack : BaseAction
{
    //public SharedInt attackCount;

    //private MidBossMonster midBossMonster;

    //public SharedBool isDelay;


    public override void OnStart()
    {
        base.OnStart();
        //Debug.Log("원거리공격 테스트");
        // 원거리 공격 로직 추가
        mob.Reference.Anim.Play("Boss_Skill2");
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    
}