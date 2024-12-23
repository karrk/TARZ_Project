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

        for (int i = 0; i < setting.SkillAnchor.Length; i++)
        {
            if(rate < setting.SkillAnchor[i])
            {
                break;
            }

            count++;
        }

        return count;
    }

    public void UpdateGauge()
    {
        this.gauge += setting.GaugeValue;
        this.gauge = Mathf.Clamp(gauge, 0, 100);
        playerModel.SkillGauge.Value = gauge;
    }
}
