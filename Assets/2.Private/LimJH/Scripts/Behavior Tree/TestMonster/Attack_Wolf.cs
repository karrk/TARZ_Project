using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;

public class Attack_Wolf : BaseAction
{
    public override void OnStart()
    {
        base.OnStart();

        // Animator 컴포넌트 가져오기
        if (mob != null)
        {
            mob.Reference.Anim.SetBool("isAttack", true);

            // 테스크 실행 => while 현재 애니메이션이 재생중인지 확인을해서 끝난시점을 잡고 OnEnd 내부 로직을 실행시키고 return TaskStatus.Success; 화이팅
            AttackRoutine().Forget();
        }
    }

    private async UniTask<TaskStatus> AttackRoutine()
    {
        //if (targetObject.Value == null)
        //{
        //    Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
        //    return TaskStatus.Failure;
        //}

        // 애니메이션이 종료될 때까지 대기
        while (mob.Reference.Anim != null)
        {
            AnimatorStateInfo stateInfo = mob.Reference.Anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Wolf_Attack") && stateInfo.normalizedTime >= 1.0f)
            {
                break;
            }
            await UniTask.Yield();
        }
        if (mob != null)
        {
            mob.Reference.Anim.SetBool("isAttack", false);
        }

        return TaskStatus.Success;
    }
}