using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBossMonster : BaseMonster
{
    public float RangedAttackDelay;
    public float DashAttackDelay;

    [SerializeField] private GameObject explosivesPrefab;

    private void Start()
    {
        this.Stat.halfHealth = base.Stat.Health / 2;
    }

    protected override void Update()
    {
        base.Update();

        // 테스트용: Space 키로 피해를 받는 상황을 테스트
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TakeDamage(1);
            TakeHit(1000);
        }

        // 피해 감소를 적용할 때 체력이 절반 이하로 떨어지면 피해 감소를 90%로 적용
        //if (base.health <= halfHealth && base.damageReducation == 1f)
        //{
        //    ApplyDamageReduction(0.1f);  // 90% 피해 감소
        //}
    }

    // 피해 감소 비율을 적용하는 메소드
    private void ApplyDamageReduction(float reduction)
    {
        //base.damageReducation = reduction;
    }

    // 기둥 상태가 다 부서졌을 때 원상복귀 처리
    public void RestoreDamageReduction()
    {
        //base.damageReducation = 1f;  // 피해 감소를 원상복귀 (100%)
    }

    public void Skill2()
    {
        if (explosivesPrefab == null)
        {
            UnityEngine.Debug.LogError("폭탄 프리팹이 설정되지 않았습니다.");
            return;
        }
        Vector3 spawnPosition = transform.position + new Vector3(0, 4f, 0);
        GameObject projectile = Instantiate(explosivesPrefab, spawnPosition, Quaternion.identity);

        Vector3 targetPosition = player.transform.position;

        // 목표 방향 계산
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // 수평 방향만 고려
        float distance = direction.magnitude; // 수평 거리

        // 초기 속도 설정
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null)
        {
            UnityEngine.Debug.LogError("폭탄 프리팹에 Rigidbody가 없습니다.");
            return;
        }

        float launchHeight = 4f; // 던지는 초기 높이(공격 범위 만큼 지정)
        float horizontalSpeed = distance / Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) / launchHeight);
        Vector3 velocity = direction.normalized * horizontalSpeed;
        velocity.y = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * launchHeight); // 수직 속도 계산

        rb.velocity = velocity;
    }
}
