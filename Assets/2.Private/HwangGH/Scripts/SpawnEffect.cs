using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
<<<<<<< Updated upstream
    public ParticleSystem particle;
    public GameObject target;

    public void PlayRebornEffect()
    {
        Instantiate(gameObject,target.transform.position, target.transform.rotation);
        particle.Play();
    }
=======
    public float remainTime;
    ParticleSystem particle;

    private void Awake()
    {
        remainTime = 0;
    }

    public void RegenEffect()
    {
        remainTime += Time.deltaTime;
        if(remainTime >= 1f)
            gameObject.SetActive(false);
    }
>>>>>>> Stashed changes
}
