using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Threading;

public class IsTakeDamage : BaseCondition
{
    public float timerDuration = 5f; // 일정 시간 (초 단위)
    private float damageStartTime = -1f; // 데미지를 받은 시점 (초기값: -1, 데미지를 받지 않은 상태)

    public override TaskStatus OnUpdate()
    {
        if (mob.IsOnDamaged)
        {
            if (damageStartTime < 0)
            {
                // 데미지를 처음 받은 시점 기록
                damageStartTime = Time.time;
            }

            // 데미지를 받고 지정된 시간이 지나면 성공 상태 반환
            if (Time.time - damageStartTime >= timerDuration)
            {
                Debug.Log("시간지남");
                return TaskStatus.Success;
            }
        }
        else
        {
            // 데미지를 받지 않으면 타이머 초기화
            damageStartTime = -1f;
        }

        return TaskStatus.Failure;
    }
}