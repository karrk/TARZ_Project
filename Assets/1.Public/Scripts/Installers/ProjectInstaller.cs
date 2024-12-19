using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    [Inject] private NormalPrefab prefabs;

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
        Container.Bind<PoolManager>().FromComponentInNewPrefab(prefabs.PoolManager)
            .AsSingle().NonLazy();
    }

    [Serializable]
    public class NormalPrefab
    {
        public GameObject PoolManager;
        public GameObject Player;
    }

    [Serializable]
    public class PooledPrefab
    {
        public Prefabs<E_Monster> Monster;
        public Prefabs<E_VFX> VFX;
    }

    [Serializable]
    public class CameraSetting
    {
        public float Height;
        public float Dist;

        public float RotationSpeed;
        public float SmoothSpeed;
    }

    [Serializable]
    public class Prefabs<T> where T : Enum
    {
        [SerializeField] private PrefabList<T> list;

        public GameObject this[T idx]
        { get { return list[idx]; } }

        public Dictionary<Enum, GameObject> GetPairTable()
        {
            Dictionary<Enum, GameObject> pairTable = new Dictionary<Enum, GameObject>();
            
            foreach (var item in list.Table)
            {
                pairTable.Add(item.Key, item.Value);
            }

            return pairTable;
        }

        [Serializable]
        private class PrefabList<DetailType> where DetailType : Enum
        {
            [SerializeField] private List<PrefabItem<DetailType>> pairInfo;
            private Dictionary<DetailType, GameObject> table = new Dictionary<DetailType, GameObject>();
            
            public Dictionary<DetailType, GameObject> Table
            {
                get
                {
                    if (pairInfo.Count == table.Count)
                        return table;

                    RegistAllPrefabs();
                    return table;
                }
            }

            public GameObject this[DetailType type]
            {
                get { return Table[type]; }
            }

            public void RegistAllPrefabs()
            {
                DetailType type;
                GameObject prefabObj;

                for (int i = 0; i < pairInfo.Count; i++)
                {
                    type = pairInfo[i].Type;
                    prefabObj = pairInfo[i].Prefab;

                    table.Add(type, prefabObj);
                }
            }
        }

        [Serializable]
        private class PrefabItem<DetailType> where DetailType : Enum
        {
            public DetailType Type;
            public GameObject Prefab;
        }
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
