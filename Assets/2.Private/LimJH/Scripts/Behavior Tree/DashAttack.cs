using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using System.Threading;
using Unity.VisualScripting.Antlr3.Runtime;

public class DashAttack : BaseAction
{
    private Vector3 dashDirection;
    private bool isDashing;
    private bool isEndDash;
    private bool readyGroggyAct;
    private bool initState;
    private bool onChangedState;
    private bool forceEnd;

    private CancellationTokenSource cancellationTokenSource;

    public override void OnStart()
    {
        base.OnStart();

        // CancellationTokenSource 초기화
        cancellationTokenSource = new CancellationTokenSource();

        if (onChangedState)
        {
            forceEnd = true;
            onChangedState = false;
        }

        initState = mob.Stat.isGroggyActive;
        mob.Stat.rushCount = Random.Range(1, 4); // 1~3 랜덤 횟수 선택

        // 돌진 중 상태 초기화
        isDashing = false;
        isEndDash = false;
    }

    private async UniTask Rush(int count, CancellationToken token)
    {
        isDashing = true;

        for (int i = 0; i < count; i++)
        {
            await RushLogic(token);
            if (token.IsCancellationRequested) break;
            await Wait(token);
            if (token.IsCancellationRequested) break;
        }

        isEndDash = true;
    }

    private async UniTask Wait(CancellationToken token)
    {
        float timer = bossMob.DashAttackDelay;
        while (timer > 0)
        {
            if (isEndDash || token.IsCancellationRequested)
                break;

            timer -= Time.deltaTime;
            await UniTask.Yield(token);
        }
    }

    private async UniTask RushLogic(CancellationToken token)
    {
        mob.Reference.Anim.Play("Boss_Skill1(Rush)");

        dashDirection = (mob.PlayerPos - transform.position).normalized;

        Vector3 start = transform.position;
        Vector3 targetPos = transform.position + dashDirection * mob.Stat.dashSpeed;

        float duration = Vector3.Distance(start, targetPos) / mob.Stat.dashSpeed;
        float t = 0;

        while (t < 1)
        {
            if (token.IsCancellationRequested) break;

            if (readyGroggyAct && initState)
            {
                onChangedState = true;
                readyGroggyAct = false;
                isEndDash = true;
                break;
            }

            RotateTowardsPlayer();
            transform.position = Vector3.Lerp(start, targetPos, t);
            t += Time.deltaTime / duration;

            await UniTask.Yield(token);
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 targetDirection = (mob.PlayerPos - transform.position).normalized;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (forceEnd)
        {
            forceEnd = false;
            return TaskStatus.Success;
        }

        if (mob.player == null)
        {
            return TaskStatus.Failure;
        }

        if (!isDashing)
        {
            Rush(mob.Stat.rushCount, cancellationTokenSource.Token).Forget();
        }
        else if (isEndDash)
        {
            return TaskStatus.Success;
        }
        else if (onChangedState)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        base.OnEnd();

        // 비동기 작업 취소
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();

        isDashing = false; // 상태 초기화
    }
}