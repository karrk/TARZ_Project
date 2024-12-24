using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IDamagable 
{
    void TakeHit(float value, bool chargable);
}
