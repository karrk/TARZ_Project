using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable 
{
    void Push(Vector3 pos, E_SkillType skillType);
}
