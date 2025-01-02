using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsKnockdown : Conditional
{
    public SharedInt attackType; // 공격 타입 (공유 변수로 설정)

    public override TaskStatus OnUpdate()
    {
        return attackType.Value == 3 ? TaskStatus.Success : TaskStatus.Failure;
    }
}