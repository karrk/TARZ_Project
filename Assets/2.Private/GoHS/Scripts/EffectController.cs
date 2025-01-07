using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    // 대쉬 이펙트
    [SerializeField] public ParticleSystem Dash_LeftHand; 
    [SerializeField] public ParticleSystem Dash_RightHand;
    



    public void DashEffect()
    {
        Dash_LeftHand.Play();
        Dash_RightHand.Play();
    }

    //private void OnParticleSystemStopped()
    //{
    //    Dash_LeftHand.Stop();
    //    Dash_RightHand.Stop();
    //}
}
