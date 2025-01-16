using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MeleeSkill_2 : BaseState
{
    public MeleeSkill_2(ProjectPlayer player) : base(player)
    {
    }

    private GameObject hitBox => player.Refernece.MeleeSkill2HitBox;

    [SerializeField] private float dashTime;         // 현재 대쉬 지속 시간
    private Vector3 dashDirection;  // 대쉬 방향


    [SerializeField] private float coolTime;
    private bool canSkill = true;
    public bool CanSkill { get { return canSkill; } set { canSkill = value; } }


    public override void Enter()
    {
        dashTime = player.Setting.MeleeSkill2Setting.DashTime;      
        coolTime = player.Setting.MeleeSkill2Setting.CoolTime;
        player.playerUIModel.SkillCoolTime[1].Value = player.Setting.MeleeSkill2Setting.CoolTime;

        player.candash = true;

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), true);

        // 플레이어 입력방향에 따라 대쉬 방향 설정
        Vector3 forward = player.Cam.transform.forward;
        Vector3 right = player.Cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        dashDirection = (forward * player.InputZ) + (right * player.InputX);

        // 입력이 없으면 마지막 바라보는 방향으로 대쉬
        if (dashDirection == Vector3.zero)
        {
            dashDirection = player.transform.forward;
        }

        Quaternion rotation = Quaternion.LookRotation(dashDirection);
        //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rotation, 500f * Time.deltaTime);
        player.transform.rotation = rotation;

        //Debug.Log("근접 스킬 2번 시전 시작!");
        player.Refernece.Animator.SetTrigger("MeleeSkill_2");
        player.Refernece.EffectController.MeleeSkill_2Effect_Start();

        hitBox.SetActive(true);

        player.Refernece.Rigid.AddForce(dashDirection * player.Setting.MeleeSkill2Setting.DashSpeed, ForceMode.VelocityChange);

        canSkill = false;
        player.StartCoroutine(CoolTimeCoroutine());
    }

    public override void Update()
    {
        if (dashTime > 0f)
        {
            dashTime -= Time.deltaTime;
        }
        else
        {
            //Debug.Log("근접 스킬 2번 진행됨");
            player.ChangeState(E_State.Idle);
        }
    }


    public override void Exit()
    {
        hitBox.SetActive(false);
        player.Refernece.EffectController.MeleeSkill_2Effect_End();
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Monster"), false);
    }

    private IEnumerator CoolTimeCoroutine()
    {
        while (player.playerUIModel.SkillCoolTime[1].Value > 0)
        {
            player.playerUIModel.SkillCoolTime[1].Value -= Time.deltaTime;

            yield return null;

        }
        canSkill = true;
    }
}
