using UnityEngine;
using System.Collections;
using UnityEngine.Windows;

[System.Serializable]

public class LongRangeAttackState : BaseState
{
    public LongRangeAttackState(ProjectPlayer player) : base(player)
    {
    }

    private Vector3 moveDirection;
    private float attackDelayTimer = 0f;    // 공격 딜레이 타이머           TODO : 사용 안해도 될 수 있음. 
    private float stateDelayTimer = 0f;     // 상태 딜레이 타이머
    private int attackStack = 0;            // 현재 공격 스택        
    private const int MAXSTACK = 4;         // 최종 공격 스택
    private bool isAttackDelaing;
    private Coroutine waitRoutine;
    private bool usedJumpAttack = false;

    public override void Enter()
    {
        if (usedJumpAttack == true)
            return;

        AlignCamForward();

        stateDelayTimer = player.Setting.longRangeSetting.stateDelayTimer;
        
        if(player.IsJumpAttack == true)
        {
            usedJumpAttack = true;
            attackStack = 0;
        }
        else
        {
            if (waitRoutine != null)
            {
                player.StopCoroutine(waitRoutine);
            }
            waitRoutine = player.StartCoroutine(ReduceAttackStateDelay());
        }

        player.Refernece.Animator.SetBool("LongRangeAttack", true);
        player.Refernece.Animator.SetTrigger("AttackTrigger");
    }

    public void ResetJumpAttack()
    {
        usedJumpAttack = false;
    }

    private void AlignCamForward()
    {
        Vector3 cameraEuler = player.Cam.transform.rotation.eulerAngles;    // 카메라의 각도
        Vector3 playerEuler = player.transform.rotation.eulerAngles;        // 플레이어의 각도

        player.transform.rotation = Quaternion.Euler(playerEuler.x, cameraEuler.y, cameraEuler.z);  // 카메라 방향으로 플레이어 방향설정
    }

    private IEnumerator ReduceAttackStateDelay()
    {
        while (true)
        {
            if (stateDelayTimer <= 0)
                break;

            if (player.CurState != E_State.LongRangeAttack)
                break;

            stateDelayTimer -= Time.deltaTime;

            yield return null;
        }

        attackStack = 0;

        if(player.CurState == E_State.LongRangeAttack)
        {
            player.ChangeState(E_State.Idle);
        }

        player.Refernece.Animator.SetBool("LongRangeAttack", false);
    }

    public override void Update()
    {
        player.Refernece.Animator.SetFloat("VelocityX", player.InputX);
        player.Refernece.Animator.SetFloat("VelocityZ", player.InputZ);

        AlignCamForward();

        // 점프 진행했을때
        // TODO : 입력 바꾸어야함
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("점프 진입!");
        //    player.Refernece.Animator.SetBool("Jump", true);
        //    player.Refernece.Rigid.AddForce(Vector3.up * player.Setting.JumpSetting.JumpPower, ForceMode.Impulse);
        //    player.isGrounded = false;
        //    jumpAttackNum = 1;
        //}


        // 공격횟수가 0이 아닐때, 현재 공격 가능한 상태일때, 버튼을 입력했을때, 땅에 있지 않을때
        // TODO : 입력 바꾸어야함
        //if (jumpAttackNum != 0 && Input.GetMouseButtonDown(0) && !player.isGrounded)
        //{
        //    player.Refernece.Animator.SetTrigger("AttackTrigger");
        //    //attackDelayTimer = player.Setting.longRangeSetting.attackDelayTimer;    // 공격 딜레이 재설정
        //    stateDelayTimer = player.Setting.longRangeSetting.stateDelayTimer;       // 상태 딜레이 재설정
        //    jumpAttackNum--;    // 점프 중 공격 1회만 진행하기 위해 1 차감
        //}


        //// 플레이어가 땅에 있다면
        //if (player.isGrounded)
        //{
        //    player.Refernece.Animator.SetBool("Jump", false);
        //    jumpAttackNum = 1;
        //}

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
        if (player.CurState == E_State.Dash)
            return;

        int curHand = player.Refernece.Animator.GetInteger("ThrowHand");
        int nextHand = (curHand + 1) % 2;
        player.Refernece.Animator.SetInteger("ThrowHand", nextHand);



        player.Refernece.Shooter.FireItem();    // 총알 발사


        if (attackStack < MAXSTACK && usedJumpAttack == false)  // 현재 스택이 최대 스택보다 낮다면
        {
            attackStack++;  // 스택 1 추가
        }

    }

    public override void Exit()
    {
        //Debug.Log("@@@@@@@@@@@@@@@@@@@@@@원거리 공격상태 해제됨");
        //attackStack = 0;    // 공격 스택 초기화
        //player.Refernece.Animator.SetBool("LongRangeAttack", false);

    }

}
