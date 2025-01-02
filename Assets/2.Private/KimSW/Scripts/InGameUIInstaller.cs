using Zenject;

public class InGameUIInstaller : MonoInstaller
{
    public InGameUI inGameUI;

    public override void InstallBindings()
    {
        //Container.Bind<ItemInventory>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<InGameUI>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<PlayerUIModel>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<TargetIndicator>().FromComponentInHierarchy().AsSingle();

        Container.Bind<InGameUI>().FromInstance(inGameUI).AsSingle();

        //Container.Bind<PlayerUIPresenter>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesTo<PlayerUIPresenter>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<UIConnector>().AsSingle().NonLazy();
    }

   
}
