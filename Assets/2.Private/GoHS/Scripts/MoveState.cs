using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



[System.Serializable]
public class MoveState : BaseState
{

    [SerializeField] ProjectPlayer player;

    public MoveState(ProjectPlayer player)
    {
        this.player = player;
    }


    public override void Enter()
    {
        Debug.Log("Move상태 진입!");
    }

    public override void Update()
    {
        Debug.Log("Move 업데이트문 진행중!");
        if (player.InputX == 0 && player.InputZ == 0)
        {
            player.ChangeState(E_State.Idle);
        }

        if (Input.GetKeyDown(KeyCode.Space))

        {
            player.ChangeState(E_State.Jump);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(E_State.Dash);
        }

        if (Input.GetMouseButtonDown(0))
        {
            player.ChangeState(E_State.LongRangeAttack);
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

        Vector3 moveDirection = forward * player.InputZ + right * player.InputX;
        moveDirection.Normalize();

        player.transform.Translate(moveDirection * player.MoveSpeed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rotation, 30f * Time.deltaTime);
        }

    }


}
