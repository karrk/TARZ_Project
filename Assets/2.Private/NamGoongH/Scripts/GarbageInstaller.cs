using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GarbageInstaller : MonoInstaller
{
    //[Header("Garbage Settings")]
    //public GameObject[] garbagePrefabs; // 투척물 프리팹 배열
    //public LayerMask garbageLayer; // 투척물의 레이어 설정

    public override void InstallBindings()
    {
        //// GarbageQueue를 Singleton으로 바인딩
        //Container.Bind<GarbageQueue>().AsSingle();

        //// GarbagePrefabs 배열 바인딩
        //Container.BindInstance(garbagePrefabs).AsSingle();

        //// LayerMask 바인딩
        //Container.BindInstance(garbageLayer).AsSingle();

        // GarbageCollector와 Shooter 바인딩
        Container.Bind<PlayerGarbageCollector>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Shooter>().FromComponentInHierarchy().AsSingle();
    }
}
