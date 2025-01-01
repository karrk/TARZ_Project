using UnityEngine;
using Zenject;

public class UIConnector : IInitializable
{
    [Inject] private PlayerUIModel ui;

    [Inject] private PlayerStats stats;

    public void Initialize()
    {
        stats.OnChangedCurThrowCount += (value) => { ui.GarbageCount.Value = (int)value; };
        stats.OnChangedMaxThrowCount += SetMaxGarbageCapacity;

        stats.OnChangedCurMana += SetCurManaGauge;

        stats.OnChangedCurStamina += (value) => { ui.Stamina.Value = value; };

        stats.OnChangedMaxStamina += SetMaxStaminaGauge;

        stats.OnChangedMaxHP += SetMaxHP;
        stats.OnChangedCurHP += SetCurHP;

        ManualUpdate();
    }

    private void ManualUpdate()
    {
        SetMaxStaminaGauge(stats.MaxStamina);
        SetMaxGarbageCapacity(stats.ThrowCapacity);
        SetMaxHP(stats.MaxHealth);
        SetCurHP(stats.CurHealth);
        SetCurManaGauge(stats.CurMana);
    }

    private void SetCurManaGauge(float value)
    {
        ui.SkillGauge.Value = value;
    }

    private void SetMaxStaminaGauge(float value)
    {
        ui.MaxStamina.Value = value;
    }

    private void SetMaxGarbageCapacity(float value)
    {
        ui.MaxGarbageCount.Value = (int)value;
    }

    private void SetCurHP(float value)
    {
        ui.Hp.Value = (int)value;
    }

    private void SetMaxHP(float value)
    {
        ui.MaxHp.Value = (int)value;
    }

    //private void Set
}
