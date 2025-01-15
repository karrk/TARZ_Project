using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    public float destroyTime;
    private void Awake()
    {
        Destroy(gameObject, destroyTime);
    }
}
