using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint; // 발사 위치
    public GarbageQueue garbageQueue; // ItemQueue 참조

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바로 발사
        {
            FireItem();
        }
    }

    private void FireItem()
    {
        // 다음 아이템의 프리팹 가져오기
        GameObject garbagePrefab = garbageQueue.GetNextItemPrefab();
        if (garbagePrefab != null)
        {
            // 프리팹을 발사 위치에 생성
            GameObject projectile = Instantiate(garbagePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null) rb = projectile.AddComponent<Rigidbody>();

            rb.AddForce(firePoint.forward * 1, ForceMode.Force);
            Debug.Log($"Fired item: {garbagePrefab.name}");
        }
    }
}
