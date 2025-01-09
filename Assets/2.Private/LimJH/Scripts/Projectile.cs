using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float power;

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
        // 충돌한 객체의 태그 확인
        string collisionTag = collider.gameObject.tag;


        // 몬스터와 충돌한 경우
        if (collisionTag == "Player")
        {
            Debug.Log("몬스터 투사체 플레이어에게 충돌");

            IDamagable player = collider.gameObject.GetComponent<IDamagable>();
            if (player != null)
            {
                player.TakeHit(power, true);
            }

            // 투척물 소멸
            Destroy(gameObject);
        }

        // 바닥 또는 기본 환경과 충돌한 경우
        else if (collisionTag == "Ground" || collisionTag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
