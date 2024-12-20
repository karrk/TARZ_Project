using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsCheckGroggy : Conditional
{
    public SharedBool isGroggyActive;   // 그로기 상태 플래그
    public SharedGameObjectList pillars; // 생성된 기둥 리스트

    private bool isChekcDestoryPillar = false;

    public override TaskStatus OnUpdate()
    {
        // 기둥이 모두 파괴되었는지 확인
        for (int i = 0; i < pillars.Value.Count; i++)
        {
            if (pillars.Value[i] == null)
            {
                isChekcDestoryPillar = true;
                //return TaskStatus.Failure;
            }
        }


        // 모든 기둥이 파괴되었고 그로기 상태가 활성화되어 있지 않으면 True 반환
        if (!isGroggyActive.Value)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}