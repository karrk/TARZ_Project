using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]

public class LongRangeAttackState : BaseState
{

    [SerializeField] private ProjectPlayer player;
    [Inject] private Shooter shooter;

    public LongRangeAttackState(ProjectPlayer player)
    {
        this.player = player;
    }


    public override void Enter()
    {
        Debug.Log("원거리 공격 실행! LongRangeAttackState 상태 진입");
        //player.SpawnBullet();
        shooter.FireItem();
    }

    public override void Update()
    {

        player.ChangeState(E_State.Idle);

    }
}
