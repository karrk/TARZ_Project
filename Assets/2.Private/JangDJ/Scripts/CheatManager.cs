using UnityEngine;
using Zenject;

public class CheatManager : ITickable
{
    [Inject] private GarbageQueue garbageManager;
    [Inject] private SkillManager skillManager;

    public void Tick()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetGarbage();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetGauge();
        }

    }

    private void GetGarbage()
    {
        for (int i = 0; i < 10; i++)
        {
            garbageManager.AddItem(Random.Range((int)E_Garbage.Test2, (int)E_Garbage.Size));
        }
    }

    private void GetGauge()
    {
        skillManager.UpdateGauge();
    }
}
