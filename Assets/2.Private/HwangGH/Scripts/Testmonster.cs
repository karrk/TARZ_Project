using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testmonster : MonoBehaviour
{
    public ParticleSystem target;
    Tester effect;

    private void Start()
    {
        Debug.Log("생성");
        Instantiate(target.gameObject);
    }
}
