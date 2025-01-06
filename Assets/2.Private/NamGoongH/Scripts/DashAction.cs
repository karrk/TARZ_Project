using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAction : MonoBehaviour, IAction
{
    public float dashSpeed = 10f;

    public virtual void Execute()
    {
        // 대쉬함
    }
}
