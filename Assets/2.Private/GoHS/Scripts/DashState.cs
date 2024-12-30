using System.Collections;
using UnityEngine;

[System.Serializable]
public class DashState : BaseState
{
    [SerializeField] private float dashTime;         // 현재 대쉬 지속 시간
    private Vector3 dashDirection;  // 대쉬 방향

    public DashState(ProjectPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("Dash 상태 진입!");

        dashTime = player.Setting.DashSetting.DashTime;         // 플레이어 스크립트에서 설정한 지속시간만큼 설정
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

        player.Refernece.Animator.SetTrigger("Dash");

    }

    public override void Update()
    {
        //Debug.Log("Dash 진행중");

        if(player.candash)
        {
            if (dashTime > 0f)
            {
                player.transform.Translate(dashDirection * player.Setting.DashSetting.DashSpeed * Time.deltaTime, Space.World);
                dashTime -= Time.deltaTime;
            }
            else
            {
                player.StartCoroutine(DashCooldownRoutine());
                Debug.Log("대쉬 진행됨");
                player.ChangeState(E_State.Idle);
            }
        }
    }

    private IEnumerator DashCooldownRoutine()
    {
        player.candash = false; 
        yield return new WaitForSeconds(player.Setting.DashSetting.DashCoolTime);
        player.candash = true;
    }
}
