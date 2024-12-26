using Zenject;

public class EquipmentInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerEquipment>().FromComponentInHierarchy().AsSingle();
    }
}
