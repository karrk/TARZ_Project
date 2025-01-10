using System;
using UnityEngine;
using Zenject;

public class PoolingObjectTest : MonoBehaviour, IPooledObject
{
    [Inject] private PoolManager Manager;

    public Enum MyType => E_Monster.BasicMob;

    public GameObject MyObj => this.gameObject;

    public void Return()
    {
        Manager.Return(this);
    }

    private void Update()
    {
        //if(Input.GetMouseButtonDown(1))
        //{
        //    Return();
        //}
    }
}
