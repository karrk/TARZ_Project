using UnityEngine;
using Zenject;

[System.Serializable]
public class IdleState : BaseState
{
    [SerializeField] ProjectPlayer player;

    [Inject] private InputManager inputManager;

    public IdleState(ProjectPlayer player)
    {
        this.player = player;
    }


    public override void Enter()
    {
        player.animator.SetBool("Idle", true);
        //Debug.Log("현재 Idle 상태 진입 성공");
        player.Rigid.velocity = Vector3.zero;
        player.Rigid.angularVelocity = Vector3.zero;

    }

    public override void Update()
    {
        //Debug.Log("Idle 업데이트문 진행중");



        // 움직이는 상태로 넘어가는 로직
        if (player.InputX != 0 || player.InputZ != 0)
        {
            player.ChangeState(E_State.Move);
        }

        // 점프 상태로 넘어가는 로직
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(E_State.Jump);
        }

        // 대쉬 상태로 넘어가는 로직
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(E_State.Dash);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    player.ChangeState(E_State.LongRangeAttack);
        //}

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            player.ChangeState(E_State.Drain);
        }

        if( Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeState(E_State.LongRangeSkill_1);
        }

    }

    public override void Exit()
    {
        player.animator.SetBool("Idle", false);
    }


}
