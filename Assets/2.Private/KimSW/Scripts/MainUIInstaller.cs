using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MainSceneUI>().FromComponentInHierarchy().AsSingle();
       

    }

}
