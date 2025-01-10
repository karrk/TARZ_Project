using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
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

    private void Update()
    {
        Destroy(gameObject, 0.5f);
    }

    public void OnTriggerEnter(Collider collider)
    {
        // 충돌한 객체의 태그 확인
        string collisionTag = collider.gameObject.tag;


        if (collisionTag == "Player")
        {
            Debug.Log("폭발 프리팹 플레이어에게 충돌");

            IDamagable player = collider.gameObject.GetComponent<IDamagable>();
            if (player != null)
            {
                player.TakeHit(power, true);
            }
        }
    }
}
