using BehaviorDesigner.Runtime;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] PlayerController player;
    [SerializeField] SharedFloat health;

    public override void InstallBindings()
    {
        Container
            .Bind<PlayerController>()
            .FromInstance(player);
    }
}