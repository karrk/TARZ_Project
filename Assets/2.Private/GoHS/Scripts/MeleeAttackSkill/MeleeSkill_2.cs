using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkill_2 : BaseState
{
    public MeleeSkill_2(ProjectPlayer player) : base(player)
    {
    }

    private GameObject hitBox => player.Refernece.DashMeleeAttackHitBox;

    [SerializeField] private float dashTime;         // 현재 대쉬 지속 시간
    private Vector3 dashDirection;  // 대쉬 방향


    public override void Enter()
    {
        dashTime = player.Setting.MeleeSkill2Setting.DashTime;         
        player.candash = true;


        // 플레이어 입력방향에 따라 대쉬 방향 설정
        Vector3 forward = player.Cam.transform.forward;
        Vector3 right = player.Cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        dashDirection = (forward * player.InputZ) + (right * player.InputX).normalized;

        // 입력이 없으면 마지막 바라보는 방향으로 대쉬
        if (dashDirection == Vector3.zero)
        {
            dashDirection = player.transform.forward;
        }


        Debug.Log("근접 스킬 2번 시전 시작!");
        player.Refernece.Animator.SetTrigger("MeleeSkill_2");


        player.StartCoroutine(DelayCoroutine());
    }

    public override void Update()
    {
        if (dashTime > 0f)
        {
            player.transform.Translate(dashDirection * player.Setting.MeleeSkill2Setting.DashSpeed * Time.deltaTime, Space.World);
            dashTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("근접 스킬 2번 진행됨");
            player.ChangeState(E_State.Idle);
        }
    }


    public override void Exit()
    {
        hitBox.SetActive(false);
    }

    private IEnumerator DelayCoroutine()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(player.Setting.MeleeSkill2Setting.Delay);
        player.ChangeState(E_State.Idle);
    }
}
