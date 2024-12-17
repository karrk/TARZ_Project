using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    [Inject] private Prefabs prefabs;

    public override void InstallBindings()
    {
        InstallMisc();
        InstallInput();
        InstallSignal();
        InstallData();
        InstallPools();
    }

    private void InstallMisc()
    {
        SoundSource soundSources = new SoundSource();
        soundSources.AddSources().SetupSettings().SetParent(this.transform);
        Container.Bind<SoundSource>().AsSingle().NonLazy();

        Container.Bind<CoroutineHelper>().FromNewComponentOnRoot().AsSingle().NonLazy();
    }
    
    private void InstallInput()
    {
        Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
    }

    private void InstallSignal()
    {
        SignalBusInstaller.Install(Container);
    }

    private void InstallData()
    {
        Container.BindInterfacesAndSelfTo<CSVLoader>().AsSingle().NonLazy();
        Container.Bind<DataBase>().AsSingle().NonLazy();
        Container.Bind<DataParser>().AsSingle();
        Container.BindInterfacesAndSelfTo<DataSlots>().AsSingle().NonLazy();
    }

    private void InstallPools()
    {
        Container.Bind<PoolManager>().FromNewComponentOnNewPrefab(prefabs.PoolPrefab)
            .AsSingle().NonLazy();
    }

    [Serializable]
    public class Prefabs
    {
        public GameObject PoolPrefab;

        public List<GameObject> PoolObjects;
    }

    /// <summary>
    /// 프로젝트내에서 사용할 오디오소스 목록
    /// </summary>
    public class SoundSource
    {
        public AudioSource BGMPlayer;
        public AudioSource SFXPlayer;

        public SoundSource AddSources()
        {
            BGMPlayer = new GameObject().AddComponent<AudioSource>();
            SFXPlayer = new GameObject().AddComponent<AudioSource>();

            BGMPlayer.name = "BGM";
            SFXPlayer.name = "SFX";

            return this;
        }

        /// <summary>
        /// 각 오디오 소스 설정을 진행합니다.
        /// </summary>
        public SoundSource SetupSettings()
        {
            BGMPlayer.loop = false;
            SFXPlayer.loop = false;

            return this;
        }

        /// <summary>
        /// 오디오 소스들의 경로를 지정합니다.
        /// </summary>
        public void SetParent(Transform parent)
        {
            Transform tempDir = new GameObject().transform;
            tempDir.SetParent(parent);
            tempDir.name = "FixedAudioSorces";

            BGMPlayer.transform.SetParent(tempDir);
            SFXPlayer.transform.SetParent(tempDir);
        }
    }
}
