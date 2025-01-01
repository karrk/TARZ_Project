using UnityEngine;
using Zenject;

public class UIConnector : IInitializable
{
    [Inject] private PlayerUIModel ui;

    [Inject] private GarbageQueue garbagePool;


    public void Initialize()
    {
        garbagePool.ChangedInventory += () => { ui.GarbageCount.Value = garbagePool.Count; };
    }
}
