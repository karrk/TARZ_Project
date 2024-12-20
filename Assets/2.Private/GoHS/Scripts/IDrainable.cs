using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrainable 
{
    void DrainTowards(Vector3 targetPosition, float speed);
}
