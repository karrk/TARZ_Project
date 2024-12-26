using UnityEngine;
using Zenject;

public class SkillManager
{
    [Inject] private ProjectInstaller.PlayerSettings setting;
    [Inject] private PlayerUIModel playerModel;

    private float gauge = 0;

    public int UseSkill()
    {
        int count = 0;
        float rate = gauge / 100f;

        for (int i = 0; i < setting.BasicSetting.SkillAnchor.Length; i++)
        {
            if(rate < setting.BasicSetting.SkillAnchor[i])
            {
                break;
            }

            count++;
        }

        RemoveGauge(count);

        return count;
    }

    public void UpdateGauge()
    {
        this.gauge += setting.BasicSetting.GaugeValue;
        this.gauge = Mathf.Clamp(gauge, 0, 100);
        playerModel.SkillGauge.Value = gauge;
    }

    private void RemoveGauge(int skillNumber)
    {
        if (skillNumber == 0)
            return;

        float needPoint = setting.BasicSetting.SkillAnchor[skillNumber-1];
        gauge -= needPoint * 100;
        playerModel.SkillGauge.Value = gauge;
    }
}
