using BehaviorDesigner.Runtime;
using UnityEngine;
using Zenject;

public class MonsterInstaller : MonoInstaller
{
    //[SerializeField] PlayerController player;
    // 컨테이너에 플레이어 등록

    [SerializeField] private ProjectPlayer player;

    public override void InstallBindings()
    {
        Container
            .Bind<ProjectPlayer>()
            .FromInstance(player);
    }
}