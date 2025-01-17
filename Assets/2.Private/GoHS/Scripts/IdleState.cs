using UnityEngine;
using Zenject;

[System.Serializable]
public class IdleState : BaseState
{

    public IdleState(ProjectPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        player.Refernece.Animator.SetBool("Idle", true);
        //Debug.Log("현재 Idle 상태 진입 성공");
        player.Refernece.Rigid.velocity = Vector3.zero;
        player.Refernece.Rigid.angularVelocity = Vector3.zero;

    }

    public override void Update()
    {
        //Debug.Log("Idle 업데이트문 진행중");



        // 움직이는 상태로 넘어가는 로직
        if (player.InputX != 0 || player.InputZ != 0)
        {
            player.ChangeState(E_State.Move);
        }

        // 대쉬 상태로 넘어가는 로직
        //if (Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Space))
        //{
        //    player.ChangeState(E_State.Dash);
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    player.ChangeState(E_State.LongRangeAttack);
        //}

        //if (Input.GetKeyDown(KeyCode.LeftControl))
        //{
        //    player.ChangeState(E_State.Drain);
        //}

        //if( Input.GetKeyDown(KeyCode.R))
        //{
        //    player.ChangeState(E_State.LongRangeSkill_1);
        //}

    }

    public override void Exit()
    {
        player.Refernece.Animator.SetBool("Idle", false);
    }


}
