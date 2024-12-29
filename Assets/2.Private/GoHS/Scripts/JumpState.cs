using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static ProjectPlayer;


[System.Serializable]
public class JumpState : BaseState
{
    public JumpState(ProjectPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        if (!player.isGrounded) // 이미 점프중이면 점프 진행 안한다. 
        {
            return;
        }

        Debug.Log("점프 진입!");
        player.Refernece.Animator.SetBool("Jump", true);
        player.Refernece.Rigid.AddForce(Vector3.up * player.Setting.JumpSetting.JumpPower, ForceMode.Impulse);
        player.isGrounded = false;


    }

    public override void Update()
    {
        Debug.Log("Jump 업데이트문 진행중");
        if (player.isGrounded)
        {
            player.Refernece.Animator.SetBool("Jump", false);
            player.ChangeState(E_State.Idle);
        }
    }

    public override void FixedUpdate()
    {
        Vector3 forward = player.Cam.transform.forward;
        Vector3 right = player.Cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * player.InputZ + right * player.InputX;
        moveDirection.Normalize();

        player.transform.Translate(moveDirection * player.Setting.BasicSetting.MoveSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rotation, 15f * Time.deltaTime);
        }

    }
}
