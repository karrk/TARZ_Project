using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;

public class Attack : Action
{
    /*public SharedFloat attackDamage;
    public SharedInt attackCount;
    public SharedBool canAttack;

    public float attackRange;
    public float angle;*/

    public SharedGameObject selfObject;
    public SharedGameObject targetObject;

    private Animator animator; // Animator 컴포넌트

    private BaseMonster baseMonster;

    public override void OnStart()
    {

        if (baseMonster == null)
        {
            baseMonster = gameObject.GetComponent<BaseMonster>();
        }

        // Animator 컴포넌트 가져오기
        if (selfObject.Value != null)
        {
            animator = selfObject.Value.GetComponent<Animator>();
            animator.SetBool("isAttack", true);

            // 테스크 실행 => while 현재 애니메이션이 재생중인지 확인을해서 끝난시점을 잡고 OnEnd 내부 로직을 실행시키고 return TaskStatus.Success; 화이팅
            AttackRoutine(baseMonster).Forget();
        }
    }

    private async UniTask<TaskStatus> AttackRoutine(BaseMonster baseMonster)
    {
        if (targetObject.Value == null)
        {
            Debug.LogWarning("타겟 오브젝트가 존재하지 않습니다.");
            return TaskStatus.Failure;
        }

        // 애니메이션이 종료될 때까지 대기
        while (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Jake_Attack") && stateInfo.normalizedTime >= 1.0f)
            {
                break;            
            }
            await UniTask.Yield();
        }

        animator.SetBool("isAttack", false);

        return TaskStatus.Success;
    }
}