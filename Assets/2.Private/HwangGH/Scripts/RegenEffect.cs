using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenEffect : MonoBehaviour
{
    // 모든 몬스터에 적용되는 젠이펙트
    private void Awake()
    {
        Destroy(gameObject, 3f);
    }
}
