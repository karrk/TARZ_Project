using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class rangedAttack : Action
{
    public SharedInt attackCount;

    private MidBossMonster midBossMonster;

    public SharedBool isDelay;


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
        // 점프 및 공격 종료 작업
        Debug.Log("원거리 특수 공격 완료");

        // 점프 공격 쿨타임 시작
        if (midBossMonster != null)
        {
            midBossMonster.StartCoroutine(StartRangedAttackDelay(midBossMonster));
        }

        //attackCount.Value = 0;
    }

    private System.Collections.IEnumerator StartRangedAttackDelay(MidBossMonster monster)
    {

        isDelay.Value = true;
        yield return new WaitForSeconds(monster.RangedAttackDelay);
        isDelay.Value = false;
        Debug.Log("원거리 특수 공격 쿨타임 종료");

        attackCount.Value = 0;
    }
}