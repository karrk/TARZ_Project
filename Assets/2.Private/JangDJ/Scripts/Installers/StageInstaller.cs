using UnityEngine;
using Zenject;

public class StageInstaller : MonoInstaller
{
    [Inject] private ProjectInstaller.NormalPrefab prefabs;

    public override void InstallBindings()
    {
        InstallPlayer();
    }

    private void InstallPlayer()
    {
        Container.Bind<ProjectPlayer>().
            FromComponentInNewPrefab(prefabs.Player)
            .AsSingle().NonLazy();
    }
}
