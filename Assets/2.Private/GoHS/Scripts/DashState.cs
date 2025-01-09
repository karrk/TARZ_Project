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

        Quaternion rotation = Quaternion.LookRotation(dashDirection);
        //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, rotation, 500f * Time.deltaTime);
        player.transform.rotation = rotation;

        player.candash = true;
        player.SoundManager.PlaySFX(player.SoundSetting.Player.Audio);
    }

    public override void Update()
    {
        //Debug.Log("Dash 진행중");
        if (player.candash)
        {
            player.Refernece.Animator.SetTrigger("Dash");
            player.Refernece.Rigid.AddForce(dashDirection * player.Setting.DashSetting.DashSpeed, ForceMode.VelocityChange);
            player.candash = false;


        }
        else
        {
            if (dashTime > 0f)
            {
                player.candash = false;
                dashTime -= Time.deltaTime;
            }
            else
            {
                Debug.Log("대쉬 진행됨");
                player.Refernece.Rigid.velocity = Vector3.zero;
                player.Refernece.Rigid.angularVelocity = Vector3.zero;
                player.ChangeState(E_State.Idle);
            }

        }
    }

    private IEnumerator DashCooldownRoutine()
    {
        player.Refernece.Animator.SetTrigger("Dash");
        player.Refernece.Rigid.AddForce(dashDirection * player.Setting.DashSetting.DashSpeed, ForceMode.VelocityChange);
        yield return new WaitForSeconds(player.Setting.DashSetting.DashTime);

        Debug.Log("대쉬 진행됨");
        player.ChangeState(E_State.Idle);
    }
}
