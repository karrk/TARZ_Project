using UnityEngine;
using Zenject;

public class GameSceanInstaller : MonoInstaller
{
    [SerializeField] ProjectPlayer player;

    public override void InstallBindings()
    {
        Container
            .Bind<ProjectPlayer>()
            .FromInstance(player)
            .AsSingle();


        Container.Bind<IdleState>().AsSingle();
        Container.Bind<MoveState>().AsSingle();
        Container.Bind<JumpState>().AsSingle();
        Container.Bind<DashState>().AsSingle();
        Container.Bind<LongRangeAttackState>().AsSingle();
    }
}