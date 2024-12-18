using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseState
{
    public virtual void Enter() { }                 // 상태에 진입했을때 1회 호출
    public virtual void Update() { }                // 해당 상태에서 진행되어야하는 Update 진행
    public virtual void FixedUpdate() { }           // 해당 상태에서 진행되어야하는 FixedUpdate 진행
    public virtual void Exit() { }                  // 상태에서 다른 상태로 전환되는 타이밍에 1회 호출
}

