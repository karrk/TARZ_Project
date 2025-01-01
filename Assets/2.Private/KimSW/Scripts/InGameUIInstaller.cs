using Zenject;

public class InGameUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<ItemInventory>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<InGameUI>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<PlayerUIModel>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<TargetIndicator>().FromComponentInHierarchy().AsSingle();
        
        Container.BindInterfacesTo<PlayerUIPresenter>().AsSingle().NonLazy();
    }

   
}
