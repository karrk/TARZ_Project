using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BattleItemInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InteractionObject>().FromComponentInHierarchy().AsTransient();
    }
}
