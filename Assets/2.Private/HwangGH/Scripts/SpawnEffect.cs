using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject target;

    public void PlayRebornEffect()
    {
        Instantiate(gameObject,target.transform.position, target.transform.rotation);
        particle.Play();
    }
}
