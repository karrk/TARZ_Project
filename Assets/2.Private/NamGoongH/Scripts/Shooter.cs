using UnityEngine;
using Zenject;

public class Shooter : MonoBehaviour
{
    public Transform firePoint; // 발사 위치
    public GarbageQueue garbageQueue; // GarbageQueue 참조
    private float speed;

    [Inject]
    public void Construct(GarbageQueue garbageQueue, ProjectInstaller.PlayerSettings setting)
    {
        this.garbageQueue = garbageQueue;
        this.speed = setting.ThrowingSpeed;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바로 발사
    //    {
    //        FireItem();
    //    }
    //}

    /// <summary>
    /// 받아온 프리탭을 발사함
    /// </summary>
    public void FireItem()
    {
        // 다음 아이템의 프리팹 가져오기
        GameObject garbagePrefab = garbageQueue.GetNextGarbagePrefab();
        if (garbagePrefab != null)
        {
            // 프리팹을 발사 위치에 생성
            GameObject projectile = Instantiate(garbagePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null) rb = projectile.AddComponent<Rigidbody>();

            rb.AddForce(firePoint.forward * speed, ForceMode.Impulse);
            Debug.Log($"Fired item: {garbagePrefab.name}");
        }
    }
}
