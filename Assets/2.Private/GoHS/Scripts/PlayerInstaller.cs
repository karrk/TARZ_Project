using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] ProjectPlayer player;
    [SerializeField] GameObject longRangeSkill_1;

    public override void InstallBindings()
    {
        InstallPlayerComponent();
        InstallPlayerStates();
        InstallCamera();
    }

    private void InstallPlayerComponent()
    {
        Container
            .Bind<ProjectPlayer>()
            .FromInstance(player)
            .AsSingle()
            .NonLazy();

        Container.Bind<GameObject>().FromInstance(longRangeSkill_1).AsSingle().NonLazy();
    }

    private void InstallPlayerStates()
    {
        Container.Bind<IdleState>().AsSingle();
        Container.Bind<MoveState>().AsSingle();
        Container.Bind<JumpState>().AsSingle();
        Container.Bind<DashState>().AsSingle();
        Container.Bind<LongRangeAttackState>().AsSingle();
        Container.Bind<DrainState>().AsSingle();
        Container.Bind<LongRangeSkill_1>().AsSingle();
    }

    private void InstallCamera()
    {

    }
}