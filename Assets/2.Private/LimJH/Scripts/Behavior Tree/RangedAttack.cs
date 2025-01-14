using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class rangedAttack : BaseAction
{
    //public SharedInt attackCount;

    //private MidBossMonster midBossMonster;

    //public SharedBool isDelay;


    public override void OnStart()
    {
        Debug.Log("원거리공격 테스트");
        // 원거리 공격 로직 추가
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        Debug.Log("원거리 특수 공격 완료");

        if (bossMob != null)
        {
            bossMob.StartCoroutine(StartRangedAttackDelay(bossMob));
        }

        //mob.attackCount = 0;
    }

    private System.Collections.IEnumerator StartRangedAttackDelay(MidBossMonster monster)
    {
        mob.Stat.isSpecialAttackDelay = true;
        yield return new WaitForSeconds(monster.RangedAttackDelay);
        mob.Stat.isSpecialAttackDelay = false;
        Debug.Log("원거리 특수 공격 쿨타임 종료");

        mob.attackCount = 0;
    }
}