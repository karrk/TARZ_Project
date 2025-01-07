using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum E_State
{
    Idle, Move, Jump, Dash, LongRangeAttack, Drain,
    LongRangeSkill_1, LongRangeSkill_2, LongRangeSkill_3, LongRangeSkill_4, LongRangeSkill_5,
    DashMeleeAttack, MeleeSkill_1, MeleeSkill_2, Dead, Size
}      // 우선적으로 선언한 상태

public enum E_SkillType { None, MeleeSkill1 = 1, MeleeSkill2, LongRangeSkill5 }

[Serializable]
public class PlayerReferences
{
    public Shooter Shooter;
    public Rigidbody Rigid;
    public Animator Animator;
    public Transform MuzzlePoint;
    public GameObject Skill1_ArmUnit;
    public GameObject Skill1HitBox;
    public GameObject Skill3HitBox;
    public GameObject Skill4_ArmUnit;
    public GameObject Skill5Garbages;
    public GameObject DashMeleeAttackHitBox;
    public GameObject MeleeSkill2HitBox;
    public CapsuleCollider Coll;
    public EffectController EffectController;
}

public class ProjectPlayer : MonoBehaviour, IDamagable
{
    [Header("State")]
    [SerializeField] E_State curState = E_State.Idle;
    public E_State CurState { get { return curState; } }

    [Inject] private ProjectInstaller.PlayerSettings setting;
    public ProjectInstaller.PlayerSettings Setting => setting;

    // 상태들 추가해주기
    protected BaseState[] states = new BaseState[(int)E_State.Size];
    private IdleState idleState;
    private MoveState walkState;
    private JumpState jumpState;
    private DashState dashState;
    public LongRangeAttackState longRangeAttackState;
    private DrainState drainState;
    public LongRangeSkill_1 longRangeSkill_1State;
    private LongRangeSkill_2 longRangeSkill_2State;
    private LongRangeSkill_3 longRangeSkill_3State;
    public LongRangeSkill_4 longRangeSkill_4State;
    private LongRangeSkill_5 longRangeSkill_5State;
    public DashMeleeAttack dashMeleeAttackState;
    public MeleeSkill_1 meleeSkill_1State;
    public MeleeSkill_2 meleeSkill_2State;
    public DeadState deadState;

    [Header("프로퍼티")]
    [SerializeField] private Camera cam;                                                // 카메라 변수
    public Camera Cam { get { return cam; } set { cam = value; } }

    [SerializeField] public bool candash;

    [SerializeField] public bool IsGrounded { get; set; } = true;                     // 현재 땅에 서있는지 여부

    [SerializeField] private float inputX;                                              // 좌, 우 입력값을 받아오기 위한 변수
    public float InputX { get { return inputX; } set { inputX = value; } }

    [SerializeField] private float inputZ;                                              // 앞, 뒤 입력값을 받아오기 위한 변수

    public float InputZ { get { return inputZ; } set { inputZ = value; } }

    public float MoveSpeed => stats.MovementSpeed;

    [SerializeField] private float groundBoxHeight;

    [Inject] private InputManager inputManager;
    [Inject] public PlayerStats stats { get; private set; }
    [Inject] private Shooter shooter;
    [Inject] private SignalBus signal;
    public SignalBus Signal { get { return signal; } }
 
    [SerializeField] public PlayerReferences Refernece;

    private Coroutine CheckGroundRoutine;

    [HideInInspector] public bool IsJumpAttack = false;

    private Dictionary<E_State, List<E_State>> actionGraph = new Dictionary<E_State, List<E_State>>
    {
        // Idle 상태
        { E_State.Idle, new List<E_State>(){ E_State.Move,E_State.Jump,E_State.Dash,E_State.LongRangeAttack,
            E_State.Drain,E_State.LongRangeSkill_1, E_State.LongRangeSkill_2, E_State.LongRangeSkill_3, E_State.LongRangeSkill_4, E_State.LongRangeSkill_5,
            E_State.MeleeSkill_1, E_State.MeleeSkill_2,
            E_State.Dead} },

        // Move 상태
        {E_State.Move, new List<E_State>(){ E_State.Idle,E_State.Jump,E_State.Dash,E_State.LongRangeAttack,
            E_State.Drain,E_State.LongRangeSkill_1, E_State.LongRangeSkill_2, E_State.LongRangeSkill_3, E_State.LongRangeSkill_4, E_State.LongRangeSkill_5,
            E_State.MeleeSkill_1, E_State.MeleeSkill_2,
            E_State.Dead}  },

        // Jump 상태
        {E_State.Jump, new List<E_State>(){ E_State.Idle,E_State.LongRangeAttack,
            E_State.Dead}  },

        // Dash 상태
        {E_State.Dash, new List<E_State>(){ E_State.Idle ,E_State.DashMeleeAttack,
            E_State.Dead}  },

        // Drain 상태
        {E_State.Drain, new List<E_State>(){ E_State.Idle,
            E_State.Dead}  }, 

        // 원거리 공격 상태
        {E_State.LongRangeAttack, new List<E_State>(){ E_State.Idle,E_State.Move,E_State.Jump,E_State.Dash, E_State.LongRangeAttack,
            E_State.Drain,E_State.LongRangeSkill_1, E_State.LongRangeSkill_2, E_State.LongRangeSkill_3, E_State.LongRangeSkill_4, E_State.LongRangeSkill_5,
            E_State.MeleeSkill_1, E_State.MeleeSkill_2,
            E_State.Dead}  },

        // 원거리 스킬 1번 상태
        {E_State.LongRangeSkill_1, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 원거리 스킬 2번 상태
        {E_State.LongRangeSkill_2, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 원거리 스킬 3번 상태
        {E_State.LongRangeSkill_3, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 원거리 스킬 4번 상태
        {E_State.LongRangeSkill_4, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 원거리 스킬 5번 상태
        {E_State.LongRangeSkill_5, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 대쉬 근접 공격 상태
        {E_State.DashMeleeAttack, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 근접 스킬 1번 상태
        {E_State.MeleeSkill_1, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

        // 근접 스킬 2번 상태
        {E_State.MeleeSkill_2, new List<E_State>(){ E_State.Idle,
        E_State.Dead}  },

    };

    private bool ValidNextAction(E_State nextState)
    {
        return actionGraph[curState].Contains(nextState);
    }

    private void Awake()
    {
        cam = Camera.main;
        Refernece.Shooter = this.shooter;

        idleState = new IdleState(this);
        walkState = new MoveState(this);
        jumpState = new JumpState(this);
        dashState = new DashState(this);
        longRangeAttackState = new LongRangeAttackState(this);
        drainState = new DrainState(this);
        longRangeSkill_1State = new LongRangeSkill_1(this);
        longRangeSkill_2State = new LongRangeSkill_2(this);
        longRangeSkill_3State = new LongRangeSkill_3(this);
        longRangeSkill_4State = new LongRangeSkill_4(this);
        longRangeSkill_5State = new LongRangeSkill_5(this);
        dashMeleeAttackState = new DashMeleeAttack(this);
        meleeSkill_1State = new MeleeSkill_1(this);
        meleeSkill_2State = new MeleeSkill_2(this);
        deadState = new DeadState(this);

        states[(int)E_State.Idle] = idleState;
        states[(int)E_State.Move] = walkState;
        states[(int)E_State.Jump] = jumpState;
        states[(int)E_State.Dash] = dashState;
        states[(int)E_State.LongRangeAttack] = longRangeAttackState;
        states[(int)E_State.Drain] = drainState;
        states[(int)E_State.LongRangeSkill_1] = longRangeSkill_1State;
        states[(int)E_State.LongRangeSkill_2] = longRangeSkill_2State;
        states[(int)E_State.LongRangeSkill_3] = longRangeSkill_3State;
        states[(int)E_State.LongRangeSkill_4] = longRangeSkill_4State;
        states[(int)E_State.LongRangeSkill_5] = longRangeSkill_5State;
        states[(int)E_State.DashMeleeAttack] = dashMeleeAttackState;
        states[(int)E_State.MeleeSkill_1] = meleeSkill_1State;
        states[(int)E_State.MeleeSkill_2] = meleeSkill_2State;
        states[(int)E_State.Dead] = deadState; 


    }

    private void Start()
    {
        states[(int)curState].Enter();
        inputManager.OnControlledLeftStick += Move;
        inputManager.PressedAKey += Drain;
        inputManager.OnUpAkey += StopDrain;
        inputManager.PressedR1Key += UseLongRangeSkill;
        inputManager.PressedR2Key += Fire;
        inputManager.PressedBKey += Dash;
        inputManager.PressedXKey += Jump;

        inputManager.PressedL1Key += MeleeSkill_1;
        inputManager.OnControlledDPAD += MeleeSkill_2;
    }

    private IEnumerator CheckGround()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 halfSize = Vector3.one * (Refernece.Coll.radius * Mathf.Sqrt(2)) / 2;
        halfSize.y = groundBoxHeight;

        while (true)
        {
            if (Physics.BoxCast(transform.position + Vector3.up * 0.05f, halfSize, Vector3.down, Quaternion.identity, groundBoxHeight, 1 << 6))
            {
                IsGrounded = true;
                jumpState.ResetJump();
                longRangeAttackState.ResetJumpAttack();
                IsJumpAttack = false;
                break;
            }

            yield return null;
        }
    }

    private void Jump()
    {
        //if (IsGrounded == false || skillManager.UseStamina(setting.JumpSetting.UseStamina) == false)
        if (IsGrounded == false || stats.UseStamina(setting.JumpSetting.UseStamina) == false)
            return;

        ChangeState(E_State.Jump);

        if (CheckGroundRoutine != null)
            StopCoroutine(CheckGroundRoutine);

        CheckGroundRoutine = StartCoroutine(CheckGround());
    }

    private void Fire()
    {
        if (curState == E_State.Dash)
        {
            ChangeState(E_State.DashMeleeAttack);
            return;
        }
        else if (curState == E_State.Jump)
        {
            IsJumpAttack = true;
        }

        ChangeState(E_State.LongRangeAttack);
    }

    private void Move(Vector3 vector3)
    {
        InputX = vector3.x;
        InputZ = vector3.z;
    }

    /// <summary>
    /// 드레인 실행 함수
    /// </summary>
    private void Drain()
    {
        if (curState == E_State.Idle || curState == E_State.Move || curState == E_State.LongRangeAttack)
        {
            ChangeState(E_State.Drain);

            stats.UseStamina(10, StopDrain);
        }

    }

    /// <summary>
    /// 드레인 멈춤 함수
    /// </summary>
    private void StopDrain()
    {
        stats.SetForceStopUseStamina();
        drainState.StopDrain();
    }

    /// <summary>
    /// 대쉬 사용 함수
    /// </summary>
    private void Dash()
    {
        if (IsGrounded == false)
            return;

        //bool useAccept = skillManager.UseStamina(setting.DashSetting.UseStamina);
        bool useAccept = stats.UseStamina(setting.DashSetting.UseStamina);

        if (useAccept == false)
            return;

        ChangeState(E_State.Dash);
    }


    /// <summary>
    /// 원거리 스킬 사용 함수
    /// </summary>
    private void UseLongRangeSkill()
    {
        if (curState == E_State.Idle || curState == E_State.Move || curState == E_State.LongRangeAttack)
        {
            int skillNumber = stats.UseSkill();

            if (skillNumber == 0)
                return;



            switch (skillNumber)
            {
                case 1:
                    LongRangeSkill_1();
                    break;
                case 2:
                    LongRangeSkill_2();
                    break;
                case 3:
                    LongRangeSkill_3();
                    break;
                case 4:
                    LongRangeSkill_4();
                    break;
                case 5:
                    LongRangeSkill_5();
                    break;

            }
        }


    }

    private void MeleeSkill_1()
    {
        // TODO : 스킬을 사용할 수 있는 조건을 여기에 달아야 할까? 우선적으로 생각중
        if(meleeSkill_1State.CanSkill == true)
        {
            ChangeState(E_State.MeleeSkill_1);
        }
        else
        {
            Debug.Log("근접 스킬 1번 쿨타임중입니다.");
        }
    }

    private void MeleeSkill_2(Vector3 vector)
    {
        if(meleeSkill_2State.CanSkill == true)
        {
            ChangeState(E_State.MeleeSkill_2);
        }
        else
        {
            Debug.Log("근접 스킬 2번 쿨타임중입니다.");
        }
    }

    private void LongRangeSkill_1()
    {

        // TODO : 스킬을 사용할 수 있는 조건을 여기에 달아야 할까? 우선적으로 생각중


        ChangeState(E_State.LongRangeSkill_1);
    }

    private void LongRangeSkill_2()
    {
        // TODO : 스킬을 사용할 수 있는 조건을 여기에 달아야 할까? 우선적으로 생각중


        ChangeState(E_State.LongRangeSkill_2);
    }

    private void LongRangeSkill_3()
    {
        // TODO : 스킬을 사용할 수 있는 조건을 여기에 달아야 할까? 우선적으로 생각중


        ChangeState(E_State.LongRangeSkill_3);
    }

    private void LongRangeSkill_4()
    {
        // TODO : 스킬을 사용할 수 있는 조건을 여기에 달아야 할까? 우선적으로 생각중


        ChangeState(E_State.LongRangeSkill_4);
    }

    private void LongRangeSkill_5()
    {
        // TODO : 스킬을 사용할 수 있는 조건을 여기에 달아야 할까? 우선적으로 생각중


        ChangeState(E_State.LongRangeSkill_5);
    }

    private void Update()
    {

        states[(int)curState].Update();
    }

    private void FixedUpdate()
    {
        states[(int)curState].FixedUpdate();
    }

    public void ChangeState(E_State nextState)
    {
        if (ValidNextAction(nextState) == false)
            return;

        states[(int)curState].Exit();
        curState = nextState;
        states[(int)curState].Enter();
    }

    public void TakeDamage(float value)
    {
        // 데미지 받는 로직
        // 스탯을 통한
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position + transform.forward * 6f;
        Gizmos.DrawWireSphere(center, 3f);

        Gizmos.color = Color.red;
        if (setting != null)
        {
            Gizmos.DrawWireSphere(Refernece.Skill5Garbages.transform.position, setting.Skill5Setting.Radius);
        }

        DrawGroundBox();
    }

    private void DrawGroundBox()
    {
        if (Application.isPlaying == false)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawCube(transform.position + Vector3.down * groundBoxHeight / 2 + Vector3.up * 0.05f,
                new Vector3(Refernece.Coll.radius * Mathf.Sqrt(2), groundBoxHeight, Refernece.Coll.radius * Mathf.Sqrt(2)));
        }
        else if (IsGrounded == false)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawCube(transform.position + Vector3.down * groundBoxHeight / 2 + Vector3.up * 0.05f,
                new Vector3(Refernece.Coll.radius * Mathf.Sqrt(2), groundBoxHeight, Refernece.Coll.radius * Mathf.Sqrt(2)));
        }
    }

    public void TakeHit(float value, bool chargable = false)
    {
        if(stats.AddHP(-value))
        {
            ChangeState(E_State.Dead);
        }
        else
        {
            return;
        }
    }
}