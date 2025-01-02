using System;
using UnityEngine;
using Zenject;

public class UIConnector : IInitializable, IDisposable
{
    [Inject] private PlayerUIModel ui;

    [Inject] private PlayerStats stats;

    public void Initialize()
    {
        Debug.Log("UI 커넥터 이닛");

        stats.OnChangedCurThrowCount += (value) => { ui.GarbageCount.Value = (int)value; };
        stats.OnChangedMaxThrowCount += SetMaxGarbageCapacity;

        stats.OnChangedCurMana += SetCurManaGauge;

        stats.OnChangedCurStamina += (value) => { ui.Stamina.Value = value; };
        // TODO 함수로 제작

        stats.OnChangedMaxStamina += SetMaxStaminaGauge;

        stats.OnChangedMaxHP += SetMaxHP;
        stats.OnChangedCurHP += SetCurHP;

        ManualUpdate();
    }

    public void Dispose()
    {
        stats.OnChangedCurThrowCount -= (value) => { ui.GarbageCount.Value = (int)value; };
        stats.OnChangedMaxThrowCount -= SetMaxGarbageCapacity;

        stats.OnChangedCurMana -= SetCurManaGauge;

        stats.OnChangedCurStamina -= (value) => { ui.Stamina.Value = value; };

        stats.OnChangedMaxStamina -= SetMaxStaminaGauge;

        stats.OnChangedMaxHP -= SetMaxHP;
        stats.OnChangedCurHP -= SetCurHP;
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
