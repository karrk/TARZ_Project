using System.Collections;
using UnityEngine;
using Zenject;

public class SkillManager
{
    //[Inject] private ProjectInstaller.PlayerSettings setting;
    //[Inject] private PlayerUIModel playerModel;
    //[Inject] private CoroutineHelper helper;
    //[Inject] private PlayerStats playerStats;

    //private bool usedStamina;

    //private Coroutine staminaChargeRoutine;

    //public bool UseStamina(float stamina)
    //{
    //    if (playerModel.Stamina.Value < stamina)
    //        return false;

    //    playerModel.Stamina.Value -= stamina;

    //    usedStamina = true;

    //    if (staminaChargeRoutine != null)
    //        helper.StopCoroutine(staminaChargeRoutine);

    //    staminaChargeRoutine = helper.StartCoroutine(WaitStaminaCharge());

    //    return true;
    //}

    //private IEnumerator WaitStaminaCharge()
    //{
    //    usedStamina = false;
    //    yield return new WaitForSeconds(setting.BasicSetting.StaminaChargeWaitTime);

    //    while (true)
    //    {
    //        if (usedStamina == true || playerModel.Stamina.Value >= playerModel.MaxStamina.Value)
    //            break;

    //        playerModel.Stamina.Value += playerStats.StaminaRecoveryRate;

    //        yield return null;
    //    }
    //}

    
}
