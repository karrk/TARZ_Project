using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    [SerializeField] public GameObject SpawnEffectObject;
    [SerializeField] public float delaySeconds;
    [SerializeField] public Transform target;
    private WaitForSeconds delay;
    private Coroutine spawnCoroutine;

    private void Init()
    {
        delay = new WaitForSeconds(delaySeconds);
        spawnCoroutine = null;
    }

    // public void SpawnDefaultEffect()
    // {
    //     if (spawnCoroutine == null)
    //     {
    //         spawnCoroutine = StartCoroutine(SpawnTime());
    //     }
    //     else if (spawnCoroutine != null)
    //     {
    //         {
    //             StopCoroutine(spawnCoroutine);
    //             spawnCoroutine = null;
    //         }
    //     }
    // }
    // public IEnumerator SpawnTime()
    // {
    //     Instantiate(SpawnEffectObject.gameObject, target.transform.position, target.transform.rotation);
    //     yield return delay;
    // }
}
