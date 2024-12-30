using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

public enum E_State { Idle, Move, Jump, Dash, LongRangeAttack, Drain, 
                      LongRangeSkill_1, LongRangeSkill_2, LongRangeSkill_3,LongRangeSkill_4 , LongRangeSkill_5,
                      DashMeleeAttack, Size }      // 우선적으로 선언한 상태

[Serializable]
public class PlayerReferences
{
    public Shooter Shooter;
    public Rigidbody Rigid;
    public Animator Animator;
    public Transform MuzzlePoint;
    public GameObject Skill1HitBox;
    public GameObject Skill3HitBox;
    public GameObject Skill5Garbages;
    public GameObject DashMeleeAttackHitBox;
    public CapsuleCollider Coll;
}

public class ProjectPlayer : MonoBehaviour
{
    [Header("State")]
    [SerializeField] protected E_State curState = E_State.Idle;

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
    private LongRangeSkill_1 longRangeSkill_1State;
    private LongRangeSkill_2 longRangeSkill_2State;
    private LongRangeSkill_3 longRangeSkill_3State;
    private LongRangeSkill_4 longRangeSkill_4State;
    private LongRangeSkill_5 longRangeSkill_5State;
    public DashMeleeAttack dashMeleeAttackState;

    [Header("프로퍼티")]
    [SerializeField] private Camera cam;                                                // 카메라 변수
    public Camera Cam { get { return cam; } set { cam = value; } }

    [SerializeField] public bool candash;

    [SerializeField] public bool IsGrounded { get; set; } = true;                     // 현재 땅에 서있는지 여부

    [SerializeField] private float inputX;                                              // 좌, 우 입력값을 받아오기 위한 변수
    public float InputX { get { return inputX; } set { inputX = value; } }

    [SerializeField] private float inputZ;                                              // 앞, 뒤 입력값을 받아오기 위한 변수

    public float InputZ { get { return inputZ; } set { inputZ = value; } }

    [SerializeField] private float groundBoxHeight;

    [Inject] private InputManager inputManager;
    [Inject] private SkillManager skillManager;

    [SerializeField] public PlayerReferences Refernece;

    private Coroutine CheckGroundRoutine;

    private Dictionary<E_State, List<E_State>> actionGraph = new Dictionary<E_State, List<E_State>>
    {
        // Idle 상태
        { E_State.Idle, new List<E_State>(){ E_State.Move,E_State.Jump,E_State.Dash,E_State.LongRangeAttack,
            E_State.Drain,E_State.LongRangeSkill_1, E_State.LongRangeSkill_2, E_State.LongRangeSkill_3, E_State.LongRangeSkill_4, E_State.LongRangeSkill_5  } },

        // Move 상태
        {E_State.Move, new List<E_State>(){ E_State.Idle,E_State.Jump,E_State.Dash,E_State.LongRangeAttack,
            E_State.Drain,E_State.LongRangeSkill_1, E_State.LongRangeSkill_2, E_State.LongRangeSkill_3, E_State.LongRangeSkill_4, E_State.LongRangeSkill_5}  },

        // Jump 상태
        {E_State.Jump, new List<E_State>(){ E_State.Idle,E_State.LongRangeAttack }  },

        // Dash 상태
        {E_State.Dash, new List<E_State>(){ E_State.Idle ,E_State.DashMeleeAttack}  },

        // Drain 상태
        {E_State.Drain, new List<E_State>(){ E_State.Idle }  }, 

        // 원거리 공격 상태
        {E_State.LongRangeAttack, new List<E_State>(){ E_State.Idle,E_State.Move,E_State.Jump,E_State.Dash, E_State.LongRangeAttack }  },

        // 원거리 스킬 1번 상태
        {E_State.LongRangeSkill_1, new List<E_State>(){ E_State.Idle }  },

        // 원거리 스킬 2번 상태
        {E_State.LongRangeSkill_2, new List<E_State>(){ E_State.Idle }  },

        // 원거리 스킬 3번 상태
        {E_State.LongRangeSkill_3, new List<E_State>(){ E_State.Idle }  },

        // 원거리 스킬 4번 상태
        {E_State.LongRangeSkill_4, new List<E_State>(){ E_State.Idle }  },

        // 원거리 스킬 5번 상태
        {E_State.LongRangeSkill_5, new List<E_State>(){ E_State.Idle }  },

        // 대쉬 근접 공격 상태
        {E_State.DashMeleeAttack, new List<E_State>(){ E_State.Idle }  },

    };

    private bool ValidNextAction(E_State nextState)
    {
        return actionGraph[curState].Contains(nextState);
    }

    private void Awake()
    {
        cam = Camera.main;

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

    }

    private void Start()
    {
        states[(int)curState].Enter();
        inputManager.OnControlledLeftStick += Move;
        inputManager.PressedAKey += Drain;
        inputManager.OnUpAkey += StopDrain;
        inputManager.PressedL1Key += UseSkill;
        inputManager.PressedR2Key += Fire;
        inputManager.PressedBKey += Dash;
        inputManager.PressedXKey += Jump;
    }

    private IEnumerator CheckGround()
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 halfSize = Vector3.one * (Refernece.Coll.radius * Mathf.Sqrt(2))/2;
        halfSize.y = groundBoxHeight;

        while (true)
        {
            if (Physics.BoxCast(transform.position + Vector3.up * 0.05f, halfSize, Vector3.down, Quaternion.identity, groundBoxHeight, 1<<6))
            {
                IsGrounded = true;
                Debug.Log("바닥감지");
                break;
            }

            yield return null;
        }
    }

    private void Jump()
    {
        if (IsGrounded == false)
            return;

        IsGrounded = false;
        
        ChangeState(E_State.Jump);

        if (CheckGroundRoutine != null)
            StopCoroutine(CheckGroundRoutine);

        CheckGroundRoutine = StartCoroutine(CheckGround());
    }

    private void Fire()
    {
        if(curState == E_State.Dash)
        {
            ChangeState(E_State.DashMeleeAttack);
            return;
        }

        ChangeState(E_State.LongRangeAttack);
    }

    private void Move(Vector3 vector3)
    {
        InputX = vector3.x;
        InputZ = vector3.z;
    }
    private void Drain()
    {
        ChangeState(E_State.Drain);
    }

    private void StopDrain()
    {
        drainState.StopDrain();
    }

    private void Dash()
    {
        bool useAccept = skillManager.UseStamina(setting.DashSetting.UseStamina);

        if (useAccept == false)
            return;

        if (IsGrounded == false)
            return;

        ChangeState(E_State.Dash);
    }

    private void UseSkill()
    {
        int skillNumber = skillManager.UseSkill();

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
        // 움직임 로직을 위한 변수 입력받기
        //inputX = Input.GetAxisRaw("Horizontal");
        //inputZ = Input.GetAxisRaw("Vertical");

        //GroundCheck();
        //if (curState == E_State.Drain)
        //{
        //    drainState.OnDrawGizmos();
        //}

        Refernece.Animator.SetFloat("VelocityX", InputX);
        Refernece.Animator.SetFloat("VelocityZ", InputZ);

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

    //private void GroundCheck()
    //{
    //    // TODO : 바닥을 감지하는 방식을 레이케스트 방식으로 만들 필요가 있다.

    //    Vector3 rayStartPosition = transform.position + new Vector3(0, -0.8f, 0);

    //    Debug.DrawRay(rayStartPosition, Vector3.down, Color.yellow, 0.2f);
    //    //RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
    //    RaycastHit[] hits = Physics.RaycastAll(rayStartPosition, Vector2.down, 0.2f);

    //    if (hits != null && hits.Length >= 1)
    //    {
    //        foreach (RaycastHit hit in hits)
    //        {
    //            //Debug.Log($"콜라이더 감지 {hit.collider.name}");

    //            if (hit.collider.gameObject.CompareTag("Ground"))  
    //            {
    //                Debug.Log("콜라이더 감지 6번");
    //                IsGrounded = true;

    //            }
    //            else
    //            {
    //                //Debug.Log("땅에 없음");
    //                IsGrounded = false;
    //            }
    //        }
    //    }
    //}

    public void TakeDamage(float value)
    {
        // 데미지 받는 로직
        // 스탯을 통한
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Ground"))        // TODO : 추후에 태그 설정해야함
    //    {
    //        IsGrounded = true;
    //    }
    //}

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
        if(Application.isPlaying == false)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawCube(transform.position + Vector3.down * groundBoxHeight / 2 + Vector3.up * 0.05f,
                new Vector3(Refernece.Coll.radius * Mathf.Sqrt(2), groundBoxHeight, Refernece.Coll.radius * Mathf.Sqrt(2)));
        }
        else if(IsGrounded == false)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawCube(transform.position + Vector3.down * groundBoxHeight / 2 + Vector3.up * 0.05f,
                new Vector3(Refernece.Coll.radius * Mathf.Sqrt(2), groundBoxHeight, Refernece.Coll.radius * Mathf.Sqrt(2)));
        }
    }

}