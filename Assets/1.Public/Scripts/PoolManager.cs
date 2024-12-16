using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolManager : MonoBehaviour
{
    [Inject] private ProjectInstaller.Prefabs prefabs;

    public Transform MainDirectory => this.transform;

    private Dictionary<E_PoolType, ObjPool> poolTable = new Dictionary<E_PoolType, ObjPool>();

    /// <summary>
    /// 설정한 오브젝트들의 풀을 구성합니다.
    /// </summary>
    public void RegistPools()
    {
        poolTable.Add(E_PoolType.TestObject, new ObjPool(E_PoolType.TestObject, prefabs.PoolObjects[0], 3, MainDirectory));
    }

    /// <summary>
    /// 해당 타입의 오브젝트를 반환합니다.
    /// </summary>
    public GameObject GetObject(E_PoolType type)
    {
        return poolTable[type].GetObject();
    }

    /// <summary>
    /// 해당 타입의 오브젝트의 컴포넌트를 반환합니다.
    /// </summary>
    public T GetObject<T>(E_PoolType type)
    {
        return GetObject(type).GetComponent<T>();
    }

    /// <summary>
    /// IpooledObject를 상속받는 오브젝트를 반환합니다.
    /// </summary>
    public void Return(IPooledObject obj)
    {
        poolTable[obj.MyType].Return(obj);
    }

    private class ObjPool
    {
        private List<GameObject> pool;
        private GameObject prefab;
        private Transform innerDirectory;
        private E_PoolType type;

        public ObjPool(E_PoolType type, GameObject prefab, int initCount, Transform parent)
        {
            this.prefab = prefab;
            pool = new List<GameObject>(initCount);
            innerDirectory = new GameObject().transform;
            innerDirectory.name = prefab.name + "Pool";
            innerDirectory.SetParent(parent);

            Create(pool.Capacity);
        }

        private void Create(int count)
        {
            GameObject newObj;

            for (int i = 0; i < count; i++)
            {
                newObj = Instantiate(prefab);
                pool.Add(newObj);

                newObj.transform.SetParent(innerDirectory);
            }
        }

        public GameObject GetObject()
        {
            if(pool.Count <= 0)
            {
                Create(pool.Capacity * 2);
                return GetObject();
            }

            GameObject obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            obj.SetActive(true);

            return obj;
        }

        public void Return(IPooledObject obj)
        {
            pool.Add(obj.MyObj);
            obj.MyObj.SetActive(false);
        }
    }
}
