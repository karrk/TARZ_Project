using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

[Serializable]
public class Shooter
{
    private Transform muzzlePoint; // 발사 위치
    private GarbageQueue garbageQueue; // GarbageQueue 참조
    private float attackPower = 10f;
    private PoolManager manager;
    private ProjectInstaller.PlayerSettings setting;

    [Inject]
    public void Construct(GarbageQueue garbageQueue, 
        ProjectInstaller.PlayerSettings setting,
        PoolManager manager, ProjectPlayer player)
    {
        this.garbageQueue = garbageQueue;
        this.manager = manager;
        this.setting = setting;
        this.muzzlePoint = player.Refernece.MuzzlePoint;
    }

    /// <summary>
    /// 받아온 프리탭을 발사함
    /// </summary>
    public void FireItem()
    {
        // 다음 아이템의 프리팹 가져오기
        E_Garbage idx = garbageQueue.GetNextGarbageIndex();

        Garbage garbage = manager.GetObject<Garbage>(idx);

        if (garbage == null)
            return;

        garbage.SetAsProjectile(attackPower);

        if(garbage.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
        {
            rb = garbage.AddComponent<Rigidbody>();
        }

        rb.transform.position = muzzlePoint.position;
        rb.velocity = Vector3.zero;
        rb.AddForce(muzzlePoint.forward * setting.BasicSetting.ThrowingSpeed, ForceMode.Impulse);
    }

    public void FireItem(Vector3 firePos, Vector3 dir)
    {
        garbageQueue.AddItem(UnityEngine.Random.Range((int)E_Garbage.Test2, (int)E_Garbage.Size));
        E_Garbage idx = garbageQueue.GetNextGarbageIndex();

        Garbage garbage = manager.GetObject<Garbage>(idx);

        if (garbage == null)
            return;

        garbage.SetImmediateMode();

        garbage.SetAsProjectile(attackPower);

        if (garbage.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
        {
            rb = garbage.AddComponent<Rigidbody>();
        }

        rb.transform.position = firePos;
        rb.transform.forward = dir;
        rb.velocity = Vector3.zero;
        rb.AddForce(rb.transform.forward * setting.BasicSetting.ThrowingSpeed, ForceMode.Impulse);
    }


}
