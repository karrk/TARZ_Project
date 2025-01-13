using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LobbyUIInstaller : MonoInstaller
{
    public LobbyUI lobbyUI;

    public override void InstallBindings()
    {
        Container.Bind<LobbyUI>().FromInstance(lobbyUI).AsSingle();



    }

}
