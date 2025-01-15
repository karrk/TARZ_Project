using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float power;
    [SerializeField] public GameObject ExplosivePoint;

    private void Start()
    {
        // 몬스터 콜라이더 가져오기
        GameObject monster = GameObject.FindWithTag("Monster");
        if (monster != null)
        {
            Collider monsterCollider = monster.GetComponent<Collider>();
            Collider projectileCollider = GetComponent<Collider>();

            // 몬스터와 투척물 간의 충돌 무시
            if (monsterCollider != null && projectileCollider != null)
            {
                Physics.IgnoreCollision(monsterCollider, projectileCollider);
            }
        }
    }

    public void OnTriggerEnter(Collider collider)
    {
        // 몬스터가 아닌 다른 오브젝트와 충돌 시
        if (collider.CompareTag("Monster"))
        {
            return; // 충돌 무시
        }

        // ExplosivePoint 프리팹 생성
        if (ExplosivePoint != null)
        {
            Instantiate(ExplosivePoint, transform.position, Quaternion.identity);
        }

        // 현재 오브젝트 파괴
        Destroy(gameObject);
    }
}
