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

    public void LongRangeSkill_1()
    { 
        player.longRangeSkill_1State.LongRangeSkill_1_On();
    }

    public void LongRangeSkill_4()
    {
        player.longRangeSkill_4State.LongRangeSkill_4_On();
    }

    public void GameOver()
    {
        player.deadState.GameOver();
    }
}
