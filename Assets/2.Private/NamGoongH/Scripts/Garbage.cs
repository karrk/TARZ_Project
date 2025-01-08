using System;
using UnityEngine;
using Zenject;

public class Garbage : MonoBehaviour, IDrainable, IPooledObject
{
    public int garbageIndex; // 투척물의 인덱스
    public E_Garbage garbageType;

    private bool isProjectile = false;
    public bool IsProjectile { get { return isProjectile; } set { isProjectile = value; } } // 플레이어가 발사했는지 판별

    public Enum MyType => garbageType;

    public GameObject MyObj => this.gameObject;

    private float power;

    private bool isImmediatelyReturnMode;

    [Inject] private PoolManager manager;

    public void SetImmediateMode()
    {
        isImmediatelyReturnMode = true;
    }

    public void DrainTowards(Vector3 targetPosition, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    private void Start()
    {
        // 플레이어 콜라이더 가져오기
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Collider playerCollider = player.GetComponent<Collider>();
            Collider garbageCollider = GetComponent<Collider>();

            // 플레이어와 투척물 간의 충돌 무시
            if (playerCollider != null && garbageCollider != null)
            {
                Physics.IgnoreCollision(playerCollider , garbageCollider);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // 충돌한 객체의 태그 확인
        string collisionTag = collision.gameObject.tag;


        // 몬스터와 충돌한 경우
        if (collisionTag == "Monster" && IsProjectile == true)
        {
            //Debug.Log("Hit Monster!");

            IDamagable monster = collision.gameObject.GetComponent<IDamagable>();
            if(monster != null)
            {
                monster.TakeHit(power, true);
            }

            // 투척물 소멸
            //Destroy(gameObject);
            Return();
        }

        // 바닥 또는 기본 환경과 충돌한 경우
        else if (collisionTag == "Ground" || collisionTag == "Environment")
        {
            if (isImmediatelyReturnMode == true)
                Return();

            // 투척물이 비어있는 상태일때
            if (garbageIndex == 0)
            {
                // 무한 투척물 파괴
                //Destroy(gameObject);

                Return();
            }
            // 투척물 상태 해제
            IsProjectile = false;
            //Debug.Log("Garbage hit the ground and is no longer a projectile.");
        }
    }

    // 플레이어에 의해 발사되었음을 설정
    public void SetAsProjectile(float power)
    {
        IsProjectile = true;
        this.power = power;
    }

    public void Return()
    {
        if (isImmediatelyReturnMode == true)
            isImmediatelyReturnMode = false;

        manager.Return(this);
    }
}
