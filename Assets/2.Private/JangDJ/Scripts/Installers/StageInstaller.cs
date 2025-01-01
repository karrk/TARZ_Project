using ModestTree;
using UnityEngine;
using Zenject;

public class StageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CheatManager>().AsSingle().NonLazy();
    }

}
