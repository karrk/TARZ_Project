using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StepTriggerMobSpawner : TriggerMobSpawner
{
    [SerializeField] private List<StepList> stepLists;
    [SerializeField] private float stepIntervalTime;



    protected override async void OnTriggerEnter(Collider other)
    {
        coll.enabled = false;
        await Spawn();
    }

    protected async override UniTask Spawn()
    {
        await WaitPool();

        for (int i = 0; i < stepLists.Count; i++)
        {
            StepList list = stepLists[i];

            for (int j = 0; j < list.Count; j++)
            {
                BaseMonster mob = manager.GetObject<BaseMonster>(list[j].Type);
                mob.transform.position = list[j].transform.position;
                mob.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 359), 0);
                mob.Init(player);
            }

            await UniTask.Delay((int)(stepIntervalTime * 1000));
        }
    }
}

[Serializable]
public class StepList
{
    public MonsterCreate[] creators;
    public int Count => creators.Length;
    public MonsterCreate this[int idx]
    { get { return creators[idx]; } }
}
