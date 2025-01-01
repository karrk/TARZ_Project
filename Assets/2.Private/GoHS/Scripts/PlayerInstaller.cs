using ModestTree;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [Inject] private ProjectInstaller.NormalPrefab prefabs;

    public override void InstallBindings()
    {
        InstallPlayer();
        InstallCamera();
    }

    private void InstallPlayer()
    {
        Assert.That(FindObjectOfType<CharacterSpawner>() != null);

        Vector3 pos = FindFirstObjectByType<CharacterSpawner>().transform.position;

        Container.Bind<ProjectPlayer>().FromComponentsInNewPrefab(prefabs.Player).AsSingle()
            .OnInstantiated<ProjectPlayer>((_, obj) => { obj.transform.position = pos; })
            .NonLazy();

        Container.Bind<Shooter>().AsSingle().NonLazy();
    }

    private void InstallCamera()
    {
        CameraController controller = Camera.main.AddComponent<CameraController>();
        Container.Inject(controller);
    }
}