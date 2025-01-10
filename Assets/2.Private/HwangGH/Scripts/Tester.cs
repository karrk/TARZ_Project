using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public SpawnEffect spawnEffect;

    private void Awake()
    {
        spawnEffect = GetComponent<SpawnEffect>();
    }

    private void Update()
    {
        // spawnEffect.SpawnDefaultEffect();
    }
}
