using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolManager : MonoBehaviour
{
    [Inject] private ProjectInstaller.PooledPrefab prefabs;
    [Inject] private DiContainer container;

    public Transform MainDirectory { get; private set; }

    private Dictionary<E_PoolType, Dictionary<Enum, ObjPool>> pools = new Dictionary<E_PoolType, Dictionary<Enum, ObjPool>>();

    private void CreateMainDirectory()
    {
        MainDirectory = new GameObject().transform;
        MainDirectory.name = "Pools";
        MainDirectory.SetParent(transform);
    }

    public void RegistPools()
    {
        CreateMainDirectory();

        pools.Add(E_PoolType.Monster, new Dictionary<Enum, ObjPool>());
        pools.Add(E_PoolType.VFX, new Dictionary<Enum, ObjPool>());

        var monsterPrefab = prefabs.Monster.GetPairTable();
        foreach (var item in monsterPrefab)
        {
            pools[E_PoolType.Monster][item.Key] 
                = new ObjPool(container, item.Value,5,MainDirectory);
        }

        var vfxTable = prefabs.VFX.GetPairTable();
        foreach (var item in vfxTable)
        {
            pools[E_PoolType.VFX][item.Key]
                = new ObjPool(container, item.Value, 5, MainDirectory);
        }

    }

    /// <summary>
    /// 목록에 등록된 오브젝트 요소를 반환받습니다.
    /// </summary>
    public GameObject GetObject(Enum type)
    {
        Type requestType = type.GetType();

        foreach (var total in pools)
        {
            foreach (var inner in total.Value)
            {
                if(inner.Key.Equals(type))
                {
                    return inner.Value.GetObject();
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 요청된 오브젝트를 반환받으며 바로 컴포넌트로 접근합니다.
    /// </summary>
    public T GetObject<T>(Enum type)
    {
        return GetObject(type).GetComponent<T>();
    }

    /// <summary>
    /// 대상 오브젝트를 풀로 반환하며 오브젝트를 비활성화 합니다.
    /// </summary>
    public void Return(IPooledObject obj)
    {
        foreach (var total in pools)
        {
            foreach (var inner in total.Value)
            {
                if (inner.Key.Equals(obj.MyType))
                    inner.Value.Return(obj.MyObj);
            }
        }
    }

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        GetObject(E_Monster.TestA);
    //    }
    //}

    #region 오브젝트 풀
    private class ObjPool
    {
        private List<GameObject> pool;
        private Transform innerDirectory;
        private DiContainer container;

        public ObjPool(DiContainer container, GameObject prefab,int initCount = 10, Transform parent = null)
        {
            innerDirectory = new GameObject().transform;
            innerDirectory.gameObject.name = $"{prefab.name} Pool";
            innerDirectory.SetParent(parent);
            this.container = container;

            pool = new List<GameObject>(initCount);
            CreateObject(prefab, initCount);
        }

        private void CreateObject(GameObject prefab, int count)
        {
            GameObject newObj;

            for (int i = 0; i < count; i++)
            {
                newObj = container.InstantiatePrefab(prefab, innerDirectory);
                newObj.SetActive(false);
                pool.Add(newObj);
            }
        }

        public GameObject GetObject()
        {
            GameObject obj;

            if(pool.Count <= 1)
            {
                CreateObject(pool[0], pool.Capacity * 2);
                return GetObject();
            }

            obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            obj.SetActive(true);

            return obj;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    #endregion
}
