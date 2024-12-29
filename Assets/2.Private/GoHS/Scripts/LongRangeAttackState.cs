using UnityEngine.EventSystems;
using UnityEngine;
using Zenject;
using System.Text;

[System.Serializable]

public class LongRangeAttackState : BaseState
{
    public LongRangeAttackState(ProjectPlayer player) : base(player)
    {
    }

    private Vector3 moveDirection;
    private float attackDelayTimer = 0f;    // 공격 딜레이 타이머
    private float stateDelayTimer = 0f;     // 상태 딜레이 타이머
    private int attackStack = 0;            // 현재 공격 스택        
    private const int MAXSTACK = 4;         // 최종 공격 스택
    private bool canAttack = true;          // 공격 가능 여부
    private int jumpAttackNum = 1;     // 점프하고 공격할 수 있는 횟수


    public override void Enter()
    {
        canAttack = true;   // 공격 진입하면 공격 가능 상태
        jumpAttackNum = 1;  // 점프하고 공격횟수 1회 충전
        player.Refernece.Animator.SetBool("LongRangeAttack", true); // 원거리공격 Upper레이어 활성화를 위한 bool ture
        player.Refernece.Animator.SetTrigger("AttackTrigger");      // 공격 트리거 실행

        Debug.Log("원거리공격 진입");
        attackDelayTimer = player.Setting.longRangeSetting.attackDelayTimer;    // 공격 딜레이 재설정
        stateDelayTimer = player.Setting.longRangeSetting.stateDelayTimer;      // 상태 딜레이 재설정
        //Attack();   // 공격 진행
    }

    public override void Update()
    {
        // 카메라 컴포넌트 찾기
        CameraController cameraController = player.Cam.GetComponent<CameraController>();

        // 카메라를 찾았다면
        if (cameraController != null)
        {

            Vector3 cameraEuler = player.Cam.transform.rotation.eulerAngles;    // 카메라의 각도
            Vector3 playerEuler = player.transform.rotation.eulerAngles;        // 플레이어의 각도

            player.transform.rotation = Quaternion.Euler(playerEuler.x, cameraEuler.y, cameraEuler.z);  // 카메라 방향으로 플레이어 방향설정

        }

        Debug.Log(attackStack);         // 현재 스택이 제대로 쌓이고 있는지 파악하기 위한 디버그
        //Debug.Log(stateDelayTimer);

        // 현재 공격 가능한 상태일때, 버튼을 입력했을때, 땅에 있을때
        // TODO : 입력 바꾸어야함
        if (canAttack && Input.GetMouseButtonDown(0) && player.isGrounded)  
        {
            player.Refernece.Animator.SetTrigger("AttackTrigger");      // 공격 트리거 실행
        }
        else
        {
            attackDelayTimer -= Time.deltaTime;     // 공격 딜레이 진행
            if (attackDelayTimer <= 0f)             // 공격 딜레이만큼 시간이 지났을때
            {
                canAttack = true;                   // 다시 공격 할 수 있음을 알림
            }
        }


        // 공격 딜레이 후 상태 전환까지의 예외처리
        if (stateDelayTimer > 0f)       // 상태 딜레이가 0보다 크다면
        {
            stateDelayTimer -= Time.deltaTime;      // 상태 딜레이 진행
        }

        // 상태 딜레이가 0보다 작아진다면
        else
        {
            player.ChangeState(E_State.Idle);   // idle 상태로 넘어가기

            if(!player.isGrounded)  // 땅에 있지 않다면 jump상태로
            {
                player.ChangeState(E_State.Jump);
            }
        }


        // 점프 진행했을때
        // TODO : 입력 바꾸어야함
        if (Input.GetKeyDown(KeyCode.Space))     
        {
            Debug.Log("점프 진입!");
            player.Refernece.Animator.SetBool("Jump", true);
            player.Refernece.Rigid.AddForce(Vector3.up * player.Setting.JumpSetting.JumpPower, ForceMode.Impulse);
            player.isGrounded = false;
            jumpAttackNum = 1;
        }


        // 공격횟수가 0이 아닐때, 현재 공격 가능한 상태일때, 버튼을 입력했을때, 땅에 있지 않을때
        // TODO : 입력 바꾸어야함
        if (jumpAttackNum != 0 && canAttack && Input.GetMouseButtonDown(0) && !player.isGrounded)    
        {
            player.Refernece.Animator.SetTrigger("AttackTrigger");
            jumpAttackNum--;    // 점프 중 공격 1회만 진행하기 위해 1 차감
        }


        // 플레이어가 땅에 있다면
        if (player.isGrounded)
        {
            player.Refernece.Animator.SetBool("Jump", false);
            jumpAttackNum = 1;
        }

    }

    public override void FixedUpdate()
    {
        //player.transform.Translate(Vector3.forward * player.InputZ * player.MoveSpeed * Time.deltaTime);
        //player.transform.Translate(Vector3.right * player.InputX * player.MoveSpeed * Time.deltaTime);

        Vector3 forward = player.Cam.transform.forward;
        Vector3 right = player.Cam.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        moveDirection = forward * player.InputZ + right * player.InputX;
        //moveDirection.Normalize();

        if (moveDirection.magnitude < 1)
            moveDirection *= moveDirection.sqrMagnitude;

        player.transform.Translate(moveDirection * player.Setting.BasicSetting.MoveSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rotation, 30f * Time.deltaTime);
        }

    }

    public void Attack()
    {
        int curHand = player.Refernece.Animator.GetInteger("ThrowHand");
        int nextHand = (curHand + 1) % 2;
        player.Refernece.Animator.SetInteger("ThrowHand", nextHand);



        player.Refernece.Shooter.FireItem();    // 총알 발사

        attackDelayTimer = player.Setting.longRangeSetting.attackDelayTimer;    // 공격 딜레이 재설정
        stateDelayTimer = player.Setting.longRangeSetting.stateDelayTimer;      // 상태 딜레이 재설정

        if(attackStack < MAXSTACK)  // 현재 스택이 최대 스택보다 낮다면
        {
            attackStack++;  // 스택 1 추가
        }

        canAttack = false;  // 공격 불가 상태로 만들기
    }

    public override void Exit()
    {
        Debug.Log("@@@@@@@@@@@@@@@@@@@@@@원거리 공격상태 해제됨");
        attackStack = 0;    // 공격 스택 초기화
        player.Refernece.Animator.SetBool("LongRangeAttack", false);

    }

}
