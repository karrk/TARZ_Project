using Cysharp.Threading.Tasks;
using UnityEngine;

public class TargetTriggerMobSpawner : MonsterSpawner
{
    [SerializeField] private MonsterCreate[] prevCreators;
    [Space(20f)]
    [SerializeField] private MonsterCreate[] NextCreators;

    private int prevMobCount;

    protected override async void Start()
    {
        await Spawn(prevCreators,true);
    }

    protected async UniTask Spawn(MonsterCreate[] creators,bool counted)
    {
        await WaitPool();

        foreach (var creator in creators)
        {
            BaseMonster mob = manager.GetObject<BaseMonster>(creator.Type);
            mob.transform.position = creator.transform.position;
            mob.transform.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
            mob.Init(player);

            if(counted== true)
            {
                mob.OnDead += DecreaseCount;
            }
        }
    }

    private async void DecreaseCount()
    {
        prevMobCount--;

        if(prevMobCount <= 0)
        {
            await Spawn(NextCreators, true);
        }
    }
}
