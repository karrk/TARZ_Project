using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
<<<<<<< Updated upstream
    SpawnEffect spawnEffect;
=======
    public SpawnEffect spawnEffect;
    public GameObject prefab;
>>>>>>> Stashed changes

    private void Start()
    {
<<<<<<< Updated upstream
        spawnEffect.PlayRebornEffect();
=======
        Instantiate(prefab);
    }

    private void Update()
    {
        // spawnEffect.RegenEffect();
>>>>>>> Stashed changes
    }
}
