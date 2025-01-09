using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller<ProjectInstaller>
{
    [Inject] private NormalPrefab prefabs;
    //public LayerMask garbageLayer;

    public override void InstallBindings()
    {
        InstallMisc();
        InstallSignal();
        InstallData();
        InstallManagers();
        InstallInventory();
    }

    private void InstallMisc()
    {
        SoundSource soundSources = new SoundSource();
        soundSources.AddSources().SetupSettings().SetParent(this.transform);
        Container.Bind<SoundSource>().AsSingle().NonLazy();

        Container.Bind<CoroutineHelper>().FromNewComponentOnRoot().AsSingle().NonLazy();
        Container.Bind<GarbageQueue>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerUIModel>().AsSingle().NonLazy();
    }
    
    private void InstallSignal()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<StageEndSignal>();
        Container.DeclareSignal<PlayerDeadSignal>();
    }

    private void InstallData()
    {
        Container.BindInterfacesAndSelfTo<CSVLoader>().AsSingle().NonLazy();
        Container.Bind<DataBase>().AsSingle().NonLazy();
        Container.Bind<DataParser>().AsSingle();
        Container.BindInterfacesAndSelfTo<DataSlots>().AsSingle().NonLazy();
    }

    private void InstallManagers()
    {
        Container.Bind<PoolManager>().FromComponentInNewPrefab(prefabs.PoolManager)
            .AsSingle().NonLazy();

        //Container.Bind<SkillManager>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<InputManager>().AsSingle().NonLazy();
    }

    private void InstallInventory()
    {
        Container.BindInterfacesAndSelfTo<ItemInventory>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<PlayerEquipment>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerStats>().AsSingle().NonLazy();
    }

    [Serializable]
    public class InventorySetting
    {
        public int ItemInventoryCount;
        public EquipmentSprite equipmentSprite;
    }

    #region 캐릭터 관련 설정

    [Serializable]
    public class PlayerBaseStats
    {
        public float CriticalChance;
        public float CriticalDamage;
        public float AttackPower;
        public float SkillAttackPower;
        public float ElementaAttackPower;
        public float BasicAttackPower;
        public float Luck;
        public int ThrowableItemCapacity;
        public float ExperienceGain;
        public float ManaAbsorption;
        public float StaminaRecoveryRate;
        public float MaxStamina;
        public float MaxHealth;
        public float MovementSpeed;       
    }

    [Serializable]
    public class PlayerSettings
    {
        public BasicSettings BasicSetting;
        public JumpSettings JumpSetting;
        public DashSettings DashSetting;
        public DrainSettings DrainSetting;
        public LongRangeAttack_Settings longRangeSetting;
        public Skill_1_Settings Skill1Setting;
        public Skill_2_Settings Skill2Setting;
        public Skill_3_Settings Skill3Setting;
        public Skill_4_Settings Skill4Setting;
        public Skill_5_Settings Skill5Setting;
        public DashMeleeAttack_Settings DashMeleeAttackSetting;
        public MeleeSkill_1_Settings MeleeSkill1Setting;
        public MeleeSkill_2_Settings MeleeSkill2Setting;


        [Serializable]
        public class BasicSettings
        {
            public float[] SkillAnchor;
            public float MaxMana;
            public float ThrowingSpeed;
            public float StaminaChargeWaitTime;
            public float BasicPower;
        }

        [Serializable]
        public class JumpSettings
        {
            public float JumpPower;
            public float UseStamina;
        }

        [Serializable]
        public class DashSettings
        {
            public float DashTime;
            public float DashSpeed;
            public float DashCoolTime;
            public float UseStamina;
        }

        [Serializable]
        public class DrainSettings
        {
            public float DrainSpeed;
            public float ViewArea;
            public float MaxViewArea;
            public float ViewSpeed;
            public float ViewAngle;
            public LayerMask TargetMask;
            public float DecreaseInterval;
            public float UseStamina;
        }

        [Serializable]
        public class LongRangeAttack_Settings
        {
            public float attackDelayTimer;
            public float stateDelayTimer;
        }

        [Serializable]
        public class Skill_1_Settings
        {
            public float Delay;
        }

        [Serializable]
        public class Skill_2_Settings
        {
            public float Damage;
            public float ViewArea;
            public float ViewAngle;
            public LayerMask TargetMask;
            public float Delay;
        }

        [Serializable]
        public class Skill_3_Settings
        {
            public float Delay;
        }

        [Serializable]
        public class Skill_4_Settings
        {
            public float Radius;
            public float zOffset;
            public float Delay;
            public float Damage;
            public LayerMask TargetMask;
        }

        [Serializable]
        public class Skill_5_Settings
        {
            public int ThrowCount;
            public float ThrowPower;
            public float StartDelay;
            public float EndDelay;
            public float RotateSpeed;
            public float RotateTime;
            public float Radius;
            public float Damage;
            public float ViewArea;
            public float ViewAngle;
            public LayerMask TargetMask;
            public float Delay;
        }

        [Serializable]
        public class DashMeleeAttack_Settings
        {
            public float Delay;
        }

        [Serializable]
        public class MeleeSkill_1_Settings
        {
            public float Damage;
            public float ViewArea;
            public float ViewAngle;
            public LayerMask TargetMask;
            public float Delay;
            public float Zoffset;
            public float CoolTime;
        }

        [Serializable]
        public class MeleeSkill_2_Settings
        {
            public float DashTime;
            public float DashSpeed;
            public float CoolTime;
        }
    }

    #endregion

    #region 게임 프리팹

    [Serializable]
    public class NormalPrefab
    {
        public GameObject PoolManager;
        public GameObject Player;
    }

    [Serializable]
    public class GarbagePrefab
    {
        public GameObject[] Garbages;
    }

    #endregion

    #region 카메라 설정

    [Serializable]
    public class CameraSetting
    {
        public float Height;
        public float Dist;

        public float RotationSpeed;
        public float SmoothSpeed;
    }

    [Serializable]
    public class LockOnSetting
    {
        public LayerMask targetLayer;
        public float viewArea;
        [Range(0, 360)] public float viewAngle; 
    }

    #endregion

    #region 오브젝트 풀 설정

    [Serializable]
    public class PooledPrefab
    {
        public Prefabs<E_Monster> Monster;
        public Prefabs<E_VFX> VFX;
        public Prefabs<E_Garbage> Garbages;
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

    #endregion

    #region 오디오 설정

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

    #endregion

    [Serializable]
    public class MonsterStats
    {
        public MonsterStat BaseMobStat;
        public MonsterStat RangeMobStat;
        public MonsterStat EliteMobStat;
    }

    [Serializable]
    public class MonsterStat
    {
        public float Health;
        public float DamageReducation;
        public float Damage;
        public float Angle;
        public float AttackRange;
        public float MoveSpeed;
        public float InAttackRange;
        public float DetectRange;
        public float RotateSpeed;
        public float MaxRotateTime;
        public float StopDist;
        public bool canJumpAttack;

        public virtual void SendToCopyStats<T>(ref T target) where T : MonsterStat, new()
        {
            if (target == null)
                target = new T();

            target.Health = this.Health;
            target.DamageReducation = this.DamageReducation;
            target.Damage = this.Damage;
            target.Angle = this.Angle;
            target.AttackRange = this.AttackRange;
            target.MoveSpeed = this.MoveSpeed;
            target.InAttackRange = this.InAttackRange;
            target.DetectRange = this.DetectRange;
            target.RotateSpeed = this.RotateSpeed;
            target.MaxRotateTime = this.MaxRotateTime;
            target.StopDist = this.StopDist;
            target.canJumpAttack = this.canJumpAttack;
        }
    }
}
