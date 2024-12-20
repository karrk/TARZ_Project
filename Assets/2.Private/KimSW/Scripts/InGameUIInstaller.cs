
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Zenject.SpaceFighter;

public class InGameUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ItemInventory>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InGameUI>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayerUIModel>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TargetIndicator>().FromComponentInHierarchy().AsSingle();
        

    }

   
}
