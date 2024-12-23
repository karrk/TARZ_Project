using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using static ProjectPlayer;

[System.Serializable]
public class DashState : BaseState
{

    [SerializeField] ProjectPlayer player;


    public DashState(ProjectPlayer player)
    {
        this.player = player;
    }


    [SerializeField] private float dashTime;         // 현재 대쉬 지속 시간
    private Vector3 dashDirection;  // 대쉬 방향


    public override void Enter()
    {
        Debug.Log("Dash 상태 진입!");
        player.animator.SetTrigger("Dash");
        dashTime = player.dashduration;         // 플레이어 스크립트에서 설정한 지속시간만큼 설정

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

    }

    public override void Update()
    {
        Debug.Log("Dash 진행중");

        if (dashTime > 0f)
        {
            player.transform.Translate(dashDirection * player.dashSpeed * Time.deltaTime, Space.World);
            dashTime -= Time.deltaTime;
        }
        else
        {
            player.StartCoroutine(DashCooldownRoutine());
            player.ChangeState(E_State.Idle);
        }
    }

    private IEnumerator DashCooldownRoutine()
    {
        player.candash = false;
        yield return new WaitForSeconds(player.dashCoolDown);
        player.candash = true;
    }
}
