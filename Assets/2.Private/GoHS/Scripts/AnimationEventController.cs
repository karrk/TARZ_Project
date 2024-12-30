using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimationEventController : MonoBehaviour
{
    [Inject][SerializeField] private ProjectPlayer player;
    
    public void LongRangeAttack()
    {
        player.longRangeAttackState.Attack();
    }

    public void DashMeleeAttack()
    {
        player.dashMeleeAttackState.DashMeleeAttackOn();
    }
}
