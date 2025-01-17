using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsCheckGroggy : BaseCondition
{
    /*public SharedBool isGroggyActive;   // 그로기 상태 플래그
    public SharedIntList pillarStates; // 기둥 상태 리스트 (0: 없음, 1: 생성됨, 2: 파괴됨)*/

    public override TaskStatus OnUpdate()
    {
        // 모든 기둥이 파괴되었는지 확인
        bool allPillarsDestroyed = true;
        for (int i = 0; i < mob.Stat.pillarStates.Count; i++)
        {
            if (mob.Stat.pillarStates[i] == 0 || mob.Stat.pillarStates[i] == 1) // 아직 생성된 상태의 기둥이 있으면
            {
                allPillarsDestroyed = false;
                break;
            }
        }

        // 모든 기둥이 파괴되었고 그로기 상태가 활성화되어 있지 않으면 True 반환
        if (allPillarsDestroyed && !mob.Stat.isGroggyActive)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}