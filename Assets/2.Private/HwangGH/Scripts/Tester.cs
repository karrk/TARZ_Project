using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public EffectDestroy spawnEffect;

    private void Start()
    {
        Instantiate(spawnEffect);
    }
}
