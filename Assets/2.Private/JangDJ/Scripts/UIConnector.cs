using UnityEngine;
using Zenject;

public class UIConnector : IInitializable
{
    [Inject] private PlayerUIModel ui;

    [Inject] private GarbageQueue garbagePool;

    [Inject] private PlayerStats stats;

    public void Initialize()
    {
        garbagePool.ChangedInventoryCount += () => { ui.GarbageCount.Value = garbagePool.CurCount; };

        stats.OnChangedCurMana += (value) => { ui.SkillGauge.Value = value; };

        stats.OnChangedCurStamina += (value) => { ui.Stamina.Value = value; };

        stats.OnChangedMaxStamina += (value) => { ui.MaxStamina.Value = value; };

    }

    //private void Set
}
