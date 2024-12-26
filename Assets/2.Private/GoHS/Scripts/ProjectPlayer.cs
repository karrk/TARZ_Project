using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Zenject;
using Zenject.SpaceFighter;

public enum E_State { Idle, Move, Jump, Dash, LongRangeAttack, Drain, 
                      LongRangeSkill_1, LongRangeSkill_2, LongRangeSkill_3, Size }      // 우선적으로 선언한 상태

public class ProjectPlayer : MonoBehaviour
{
    [Header("State")]
    [SerializeField] protected E_State curState = E_State.Idle;

    // 상태들 추가해주기
    protected BaseState[] states = new BaseState[(int)E_State.Size];
    [Inject][SerializeField] private IdleState idleState;
    [Inject][SerializeField] private MoveState walkState;
    [Inject][SerializeField] private JumpState jumpState;
    [Inject][SerializeField] private DashState dashState;
    [Inject][SerializeField] private LongRangeAttackState longRangeAttackState;
    [Inject][SerializeField] private DrainState drainState;
    [Inject][SerializeField] private LongRangeSkill_1 longRangeSkill_1State;
    [Inject][SerializeField] private LongRangeSkill_2 longRangeSkill_2State;
    [Inject][SerializeField] private LongRangeSkill_3 longRangeSkill_3State;


    [Header("프로퍼티")]
    [SerializeField] private Rigidbody rigid;                                           // 리지드바디
    public Rigidbody Rigid { get { return rigid; } set { rigid = value; } }

    [SerializeField] private Camera cam;                                                // 카메라 변수
    public Camera Cam { get { return cam; } set { cam = value; } }

    [field: SerializeField] public Animator animator { get; protected set; }            // 애니메이터 변수

    [SerializeField] private float moveSpeed;                                           // 움직이는 속도
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }

    [SerializeField] public float dashSpeed;
    [SerializeField] public float dashduration;
    [SerializeField] public float dashCoolDown;
    [SerializeField] public bool candash;

    [SerializeField] public bool isGrounded { get; set; }                        // 현재 땅에 서있는지 여부

    [SerializeField] private float inputX;                                              // 좌, 우 입력값을 받아오기 위한 변수
    public float InputX { get { return inputX; } set { inputX = value; } }

    [SerializeField] private float inputZ;                                              // 앞, 뒤 입력값을 받아오기 위한 변수

    public float InputZ { get { return inputZ; } set { inputZ = value; } }

    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Inject] private InputManager inputManager;

    private Dictionary<E_State, List<E_State>> actionGraph = new Dictionary<E_State, List<E_State>>
    {
        { E_State.Idle, new List<E_State>(){ E_State.Move,E_State.Jump,E_State.Dash,E_State.LongRangeAttack,
            E_State.Drain,E_State.LongRangeSkill_1, E_State.LongRangeSkill_2, E_State.LongRangeSkill_3 } },

        {E_State.Move, new List<E_State>(){ E_State.Idle,E_State.Jump,E_State.Dash,E_State.LongRangeAttack }  },
        {E_State.Jump, new List<E_State>(){ E_State.Idle }  },
        {E_State.Dash, new List<E_State>(){ E_State.Idle }  },
        {E_State.Drain, new List<E_State>(){ E_State.Idle }  },
        {E_State.LongRangeAttack, new List<E_State>(){ E_State.Idle }  },
        {E_State.LongRangeSkill_1, new List<E_State>(){ E_State.Idle }  },
        {E_State.LongRangeSkill_2, new List<E_State>(){ E_State.Idle }  },
        {E_State.LongRangeSkill_3, new List<E_State>(){ E_State.Idle }  },

    };

    private bool ValidNextAction(E_State nextState)
    {
        return actionGraph[curState].Contains(nextState);
    }

    private void Awake()
    {
        cam = Camera.main;
        rigid = GetComponent<Rigidbody>();
        bulletSpawnPoint = transform.GetChild(1);                           // 임시 총알 발사 위치
        longRangeSkill_1State.HitBox = transform.GetChild(2).gameObject;    // 원거리 스킬 1번 히트박스 오브젝트
        longRangeSkill_3State.HitBox = transform.GetChild(3).gameObject;    // 원거리 스킬 3번 히트박스 오브젝트

        states[(int)E_State.Idle] = idleState;
        states[(int)E_State.Move] = walkState;
        states[(int)E_State.Jump] = jumpState;
        states[(int)E_State.Dash] = dashState;
        states[(int)E_State.LongRangeAttack] = longRangeAttackState;
        states[(int)E_State.Drain] = drainState;
        states[(int)E_State.LongRangeSkill_1] = longRangeSkill_1State;
        states[(int)E_State.LongRangeSkill_2] = longRangeSkill_2State;
        states[(int)E_State.LongRangeSkill_3] = longRangeSkill_3State;
    }

    private void Start()
    {
        states[(int)curState].Enter();
        inputManager.OnControlledLeftStick += Move;
        inputManager.PressedAKey += Drain;
        inputManager.OnUpAkey += StopDrain;
        inputManager.PressedL1Key += LongRangeSkill_3;
        inputManager.PressedR2Key += Fire;
        //inputManager.PressedBKey += Dash;
    }

    private void Fire()
    {
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
        ChangeState(E_State.Dash);
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

    private void GroundCheck()
    {
        // TODO : 바닥을 감지하는 방식을 레이케스트 방식으로 만들 필요가 있다.

        Vector3 rayStartPosition = transform.position + new Vector3(0, -0.8f, 0);

        Debug.DrawRay(rayStartPosition, Vector3.down, Color.yellow, 0.2f);
        //RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector2.down, 0.2f, LayerMask.GetMask("Ground"));
        RaycastHit[] hits = Physics.RaycastAll(rayStartPosition, Vector2.down, 0.2f);

        if (hits != null && hits.Length >= 1)
        {
            foreach (RaycastHit hit in hits)
            {
                //Debug.Log($"콜라이더 감지 {hit.collider.name}");

                if (hit.collider.gameObject.CompareTag("Ground"))  
                {
                    Debug.Log("콜라이더 감지 6번");
                    isGrounded = true;

                }
                else
                {
                    //Debug.Log("땅에 없음");
                    isGrounded = false;
                }
            }
        }
    }

    public void TakeDamage(float value)
    {
        // 데미지 받는 로직
        // 스탯을 통한
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))        // TODO : 추후에 태그 설정해야함
        {
            isGrounded = true;
        }
    }

    public void SpawnBullet()
    {
        if (bulletPrefab == null)
            return;

        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position + transform.forward * 6f;
        Gizmos.DrawWireSphere(center, 3f);
    }
}