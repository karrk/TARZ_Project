using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Die : Action
{
    public SharedFloat health; // 몬스터의 체력
    public SharedGameObject selfObject;


    private Animator animator;

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

                // Collider 비활성화
                /*Collider collider = selfObject.Value.GetComponent<Collider>();
                if (collider != null)
                {
                    collider.enabled = false;
                }*/


                // 애니메이션이 끝났으면 게임 오브젝트 삭제
                GameObject.Destroy(selfObject.Value);
                return TaskStatus.Success; // 죽음 처리 완료

                /*if (IsDieAnimationFinished())
                {
                    // 애니메이션이 끝났으면 게임 오브젝트 삭제
                    GameObject.Destroy(selfObject.Value);
                    return TaskStatus.Success; // 죽음 처리 완료
                }*/
            }
        }

        return TaskStatus.Running;
    }

    /*private bool IsDieAnimationFinished()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName("Jake_Die") && stateInfo.normalizedTime >= 1.0f; // normalizedTime이 1 이상이면 애니메이션 종료
        }
        return false;
    }*/
}