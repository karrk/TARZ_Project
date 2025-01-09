using System.Collections;
using UnityEngine;
using Zenject;

public class MonsterSpawner : MonoBehaviour
{
    [Inject] private PoolManager manager;
    [Inject] private ProjectPlayer player;

    private void Start()
    {
        StartCoroutine(WaitSpawn());
    }

    private IEnumerator WaitSpawn()
    {
        while (true)
        {
            if (manager.IsInited == true)
                break;

            yield return new WaitForSeconds(0.5f);
        }

        Spawn();
    }

    private void Spawn()
    {
        foreach (var creator in GetComponentsInChildren<MonsterCreate>())
        {
            BaseMonster mob = manager.GetObject<BaseMonster>(creator.Type);
            mob.transform.position = creator.transform.position;
            mob.transform.rotation = Quaternion.Euler(0,Random.Range(0,359),0);
            mob.Init(player);
        }
    }
}
