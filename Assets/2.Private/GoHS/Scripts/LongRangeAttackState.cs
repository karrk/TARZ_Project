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


    public override void Enter()
    {
        canAttack = true;   // 공격 진입하면 공격 가능 상태
        Debug.Log("원거리공격 진입");
        Attack();   // 공격 진행
    }

    public override void Update()
    {
        Debug.Log(attackStack);
        //Debug.Log(stateDelayTimer);

        if (canAttack && Input.GetMouseButtonDown(0))  // 현재 공격 가능한 상태일때, 버튼을 입력했을때
        {
            Attack();   // 공격 진행
        }
        else
        {
            attackDelayTimer -= Time.deltaTime;     // 공격 딜레이 진행
            if (attackDelayTimer <= 0f)             // 공격 딜레이만큼 시간이 지났을때
            {
                canAttack = true;                   // 다시 공격 할 수 있음을 알림
            }
        }

        if (stateDelayTimer > 0f)       // 상태 딜레이가 0보다 크다면
        {
            stateDelayTimer -= Time.deltaTime;      // 상태 딜레이 진행
        }
        // 다 진행됐으면
        else
        {
            player.ChangeState(E_State.Idle);   // idle 상태로 넘어가기
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

    private void Attack()
    {
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

    }

}
