using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Die : Action
{
    public SharedFloat health; // 몬스터의 체력
    public SharedGameObject selfObject;

    private Animator animator; // Animator 컴포넌트

    public override void OnStart()
    {
        // 애니메이터 컴포넌트 가져오기
        if (selfObject.Value != null)
        {
            animator = selfObject.Value.GetComponent<Animator>();
        }
    }

    public override TaskStatus OnUpdate()
    {
        // 체력이 0 이하일 경우 죽음 처리
        if (health.Value <= 0)
        {
            if (selfObject != null && selfObject.Value != null)
            {
                Debug.Log($"{selfObject.Value.name}이 죽었습니다!");

                // 몬스터를 비활성화하려면
                //selfObject.Value.SetActive(false);

                if (animator != null)
                {
                    animator.SetTrigger("Die"); // Die 애니메이션을 트리거로 설정
                }

                if (IsDieAnimationFinished())
                {
                    // 애니메이션이 끝났으면 게임 오브젝트 삭제
                    GameObject.Destroy(selfObject.Value);
                    return TaskStatus.Success; // 죽음 처리 완료
                }
            }
        }

        return TaskStatus.Running;
    }

    private bool IsDieAnimationFinished()
    {
        if (animator != null)
        {
            // "Die" 애니메이션의 길이가 끝났는지 확인
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName("Die") && stateInfo.normalizedTime >= 1.0f; // normalizedTime이 1 이상이면 애니메이션 종료
        }
        return false;
    }
}