using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable 
{
    void Push(Vector3 pos);

    void Push(Vector3 playerPos, Vector3 TargetPos);
}
