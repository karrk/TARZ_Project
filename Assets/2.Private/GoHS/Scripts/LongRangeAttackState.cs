using Zenject;

[System.Serializable]

public class LongRangeAttackState : BaseState
{
    public LongRangeAttackState(ProjectPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        //Debug.Log("원거리 공격 실행! LongRangeAttackState 상태 진입");
        //player.SpawnBullet();
        player.Refernece.Shooter.FireItem();
    }

    public override void Update()
    {
        player.ChangeState(E_State.Idle);

    }
}
