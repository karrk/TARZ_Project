using UnityEngine;

public interface IPooledObject
{
    public E_PoolType MyType { get; }
    public GameObject MyObj { get; }
    public void Return();
}