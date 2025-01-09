using Cysharp.Threading.Tasks;
using UnityEngine;

public class ListTriggerMobSpawner : TriggerMobSpawner
{
    [SerializeField] private int loopCount;
    private int curMobCount;

    protected async override UniTask Spawn()
    {
        await WaitPool();

        foreach (var creator in GetComponentsInChildren<MonsterCreate>())
        {
            BaseMonster mob = manager.GetObject<BaseMonster>(creator.Type);
            mob.transform.position = creator.transform.position;
            mob.transform.rotation = Quaternion.Euler(0, Random.Range(0, 359), 0);
            mob.Init(player);
            curMobCount++;
            mob.OnDead += DecreaseCount;
        }
    }

    private async void DecreaseCount()
    {
        curMobCount--;

        if (curMobCount <= 0 && loopCount > 0)
            await Spawn();
    }
}
