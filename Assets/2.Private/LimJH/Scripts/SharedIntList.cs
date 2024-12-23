using System.Collections.Generic;
using BehaviorDesigner.Runtime;

[System.Serializable]
public class SharedIntList : SharedVariable<List<int>>
{
    public static implicit operator SharedIntList(List<int> value)
    {
        return new SharedIntList { Value = value };
    }
}