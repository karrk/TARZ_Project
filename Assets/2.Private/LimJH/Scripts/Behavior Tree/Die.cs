using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Die : Action
{
    public SharedFloat health; // 몬스터의 체력
    public SharedGameObject selfObject;

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

                // 몬스터를 삭제하려면
                GameObject.Destroy(selfObject.Value);
            }
        }

        return TaskStatus.Running;
    }
}