using System.Collections;
using UnityEngine;
using Zenject;

public class SkillManager
{
    [Inject] private ProjectInstaller.PlayerSettings setting;
    [Inject] private PlayerUIModel playerModel;
    [Inject] private CoroutineHelper helper;

    private float skillGauge = 0;

    private bool usedStamina;

    private Coroutine staminaChargeRoutine;

    public int UseSkill()
    {
        int count = 0;
        float rate = skillGauge / 100f;

        for (int i = 0; i < setting.BasicSetting.SkillAnchor.Length; i++)
        {
            if(rate < setting.BasicSetting.SkillAnchor[i])
            {
                break;
            }

            count++;
        }

        RemoveSkillGauge(count);

        return count;
    }

    public bool UseStamina(float stamina)
    {
        if (playerModel.Stamina.Value < stamina)
            return false;

        playerModel.Stamina.Value -= stamina;

        usedStamina = true;

        if (staminaChargeRoutine != null)
            helper.StopCoroutine(staminaChargeRoutine);

        staminaChargeRoutine = helper.StartCoroutine(WaitStaminaCharge());

        return true;
    }

    public void UpdateSkillGauge()
    {
        this.skillGauge += setting.BasicSetting.GaugeValue;
        this.skillGauge = Mathf.Clamp(skillGauge, 0, 100);
        playerModel.SkillGauge.Value = skillGauge;
    }

    private void RemoveSkillGauge(int skillNumber)
    {
        if (skillNumber == 0)
            return;

        float needPoint = setting.BasicSetting.SkillAnchor[skillNumber-1];
        skillGauge -= needPoint * 100;
        playerModel.SkillGauge.Value = skillGauge;
    }

    private IEnumerator WaitStaminaCharge()
    {
        usedStamina = false;
        yield return new WaitForSeconds(setting.BasicSetting.StaminaChargeWaitTime);

        while (true)
        {
            if (usedStamina == true || playerModel.Stamina.Value >= playerModel.MaxStamina.Value)
                break;

            playerModel.Stamina.Value += setting.BasicSetting.StaminaChargeValue;

            yield return null;
        }
    }

    
}
