using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MonsterSpawner : MonoBehaviour
{
    [Inject] protected PoolManager manager;
    [Inject] protected ProjectPlayer player;

    protected virtual void Start()
    {
        SpawnMethod();
    }

    protected virtual async void SpawnMethod()
    {
        await Spawn();
    }

    protected virtual async UniTask Spawn()
    {
        await WaitPool();

        foreach (var creator in GetComponentsInChildren<MonsterCreate>())
        {
            BaseMonster mob = manager.GetObject<BaseMonster>(creator.Type);
            mob.transform.position = creator.transform.position;
            mob.transform.rotation = Quaternion.Euler(0,Random.Range(0,359),0);
            mob.Init(player);
        }
    }

    protected async UniTask WaitPool()
    {
        while (true)
        {
            if (manager.IsInited == true)
                break;

            await UniTask.Delay(500);
        }
    }
}
