using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Shooter : MonoBehaviour
{
    public Transform firePoint; // 발사 위치
    public GarbageQueue garbageQueue; // GarbageQueue 참조
    //private float speed;
    private float attackPower = 10f;
    private PoolManager manager;
    private ProjectInstaller.PlayerSettings setting;

    [Inject]
    public void Construct(GarbageQueue garbageQueue, 
        ProjectInstaller.PlayerSettings setting,
        PoolManager manager)
    {
        this.garbageQueue = garbageQueue;
        this.manager = manager;
        this.setting = setting;
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
        E_Garbage idx = garbageQueue.GetNextGarbageIndex();

        Garbage garbage = manager.GetObject<Garbage>(idx);
        garbage.SetAsProjectile(attackPower);

        if(garbage.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
        {
            rb = garbage.AddComponent<Rigidbody>();
        }

        rb.transform.position = firePoint.position;
        rb.velocity = Vector3.zero;
        rb.AddForce(firePoint.forward * setting.BasicSetting.ThrowingSpeed, ForceMode.Impulse);

        //if (idx )
        //{
        //    // 프리팹을 발사 위치에 생성
        //    GameObject projectile = Instantiate(garbagePrefab, firePoint.position, firePoint.rotation);

        //    // 생성된 투사체에서 Garbage 컴포넌트 가져오기
        //    Garbage garbage = projectile.GetComponent<Garbage>();
        //    if (garbage != null)
        //    {
        //        // 발사 상태로 설정
        //        garbage.SetAsProjectile(attackPower);
        //    }
        //    else
        //    {
        //        Debug.Log("Garbage component not found on the projectile!");
        //    }

        //    // Rigidbody 확인 및 추가
        //    Rigidbody rb = projectile.GetComponent<Rigidbody>();
        //    if (rb == null) rb = projectile.AddComponent<Rigidbody>();

        //    // 발사 방향으로 힘 가하기
        //    rb.AddForce(firePoint.forward * speed, ForceMode.Impulse);
        //    Debug.Log($"Fired item: {garbagePrefab.name}");
        //}
        //else
        //{
        //    Debug.LogWarning("No garbage prefab available to fire.");
        //}
    }

    public void FireItem(Vector3 firePos, Vector3 dir)
    {
        E_Garbage idx = garbageQueue.GetNextGarbageIndex();

        Garbage garbage = manager.GetObject<Garbage>(idx);
        garbage.SetAsProjectile(attackPower);

        if (garbage.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
        {
            rb = garbage.AddComponent<Rigidbody>();
        }

        rb.transform.position = firePos;
        rb.transform.forward = firePos + dir;
        rb.velocity = Vector3.zero;
        rb.AddForce(firePoint.forward * setting.BasicSetting.ThrowingSpeed, ForceMode.Impulse);
    }
}
