using UnityEngine;
using Zenject;

public class PoolingObjectTest : MonoBehaviour, IPooledObject
{
    [Inject] private PoolManager Manager;

    public E_PoolType MyType => E_PoolType.TestObject;

    public GameObject MyObj => this.gameObject;

    public void Return()
    {
        Manager.Return(this);
    }
}
