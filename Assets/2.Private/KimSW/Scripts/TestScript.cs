using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TestScript : MonoBehaviour
{
    [Inject]
    PoolManager poolManager;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)){
            poolManager.GetObject<DamageText>(E_VFX.DamageText).SetText("200", transform.position, false);

        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            poolManager.GetObject<DamageText>(E_VFX.DamageText).SetText("1000", transform.position, true);

        }
    }
}
