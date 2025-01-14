using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class MonsterGroggy : BaseAction
{
    /*public SharedBool isGroggyActive;   // 그로기 상태 플래그
    public float groggyDuration = 5f;  // 그로기 지속 시간*/
    private float timer;               // 내부 타이머

    public override void OnStart()
    {
        base.OnStart();
        // 그로기 상태 활성화
        mob.Stat.isGroggyActive = true;

        mob.Reference.Anim.SetTrigger("GroggyStart");

        mob.Reference.Anim.SetBool("IsGroggy", true);

        // 타이머 초기화
        timer = mob.Stat.groggyDuration;

        /*// 그로기 애니메이션 또는 효과를 시작
        mob.Reference.Anim.SetBool("IsGroggy", true);
        mob.Reference.Anim.SetTrigger("GroggyStart");*/

    }

    public override TaskStatus OnUpdate()
    {
        // 타이머를 감소시킴
        timer -= Time.deltaTime;

        // 지속 시간이 끝나면 그로기 상태를 비활성화하고 종료
        if (timer <= 0)
        {
            mob.Stat.isGroggyActive = false;
            mob.Reference.Anim.SetTrigger("GroggyEnd");
            return TaskStatus.Success;
        }

        // 그로기 상태 유지
        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        // 그로기 상태 비활성화
        mob.Stat.isGroggyActive = false;

        // 그로기 효과를 중지
        mob.Reference.Anim.SetBool("IsGroggy", false);
    }

}