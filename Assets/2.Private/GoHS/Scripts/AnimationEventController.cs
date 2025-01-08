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

    public void Dash_EffectOn()
    {
        player.Refernece.EffectController.DashEffect();
    }

    public void LongRangeSkill_4_EffectOn()
    {
        player.Refernece.EffectController.LongRangeSkill_4Effect();
    }

    //public void LongRangeSkill_5_Effect_Start()
    //{
    //    player.Refernece.EffectController.LongRangeSkill_5Effect_Start();
    //}

    //public void LongRangeSkill_5_Effect_End()
    //{
    //    player.Refernece.EffectController.LongRangeSkill_5Effect_End();
    //}
}
