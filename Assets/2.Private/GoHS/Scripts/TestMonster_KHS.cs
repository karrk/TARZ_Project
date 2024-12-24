using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TestMonster_KHS : MonoBehaviour, IDamagable
{
    // hp
    public void TakeHit(float str, bool chargable)
    {
        Debug.Log(str);
        Debug.Log($"{gameObject.name}");
    }

}
