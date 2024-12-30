using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMeleeAttack : BaseState
{
    public DashMeleeAttack(ProjectPlayer player) : base(player)
    {
    }

    private GameObject hitBox => player.Refernece.DashMeleeAttackHitBox;

    public override void Enter()
    {
        Debug.Log("대쉬 근접 공격 시전 시작!");
        player.Refernece.Animator.SetTrigger("DashMeleeAttack");
    }

    public override void Exit()
    {
        hitBox.SetActive(false);
        player.Refernece.Rigid.velocity = Vector3.zero;
        player.Refernece.Rigid.angularVelocity = Vector3.zero;
    }

    public void DashMeleeAttackOn()
    {
        player.StartCoroutine(DelayCoroutine());
        player.Refernece.Rigid.velocity = Vector3.zero;
        player.Refernece.Rigid.angularVelocity = Vector3.zero;
    }

    private IEnumerator DelayCoroutine()
    {
        hitBox.SetActive(true);
        yield return new WaitForSeconds(player.Setting.DashMeleeAttackSetting.Delay);
        player.ChangeState(E_State.Idle);
    }
}
