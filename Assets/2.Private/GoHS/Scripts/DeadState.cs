using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public class DeadState : BaseState
{
    public DeadState(ProjectPlayer player) : base(player)
    {
    }

    [Inject] private SignalBus signal;

    public override void Enter()
    {
        Debug.Log("플레이어 사망");
        player.Refernece.Animator.SetBool("Dead", true);
        player.Refernece.EffectController.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        player.Signal.Fire<PlayerDeadSignal>();
        player.inGameUI.OnGameOver(false);
        Debug.Log("플레이어 사망 애니메이션 이벤트 발생");
    }

}
