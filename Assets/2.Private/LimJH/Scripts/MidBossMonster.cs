using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class MidBossMonster : BaseMonster
{
    /*public float RangedAttackDelay;*/
    public float DashAttackDelay;

    [SerializeField] private GameObject explosivesPrefab;
    [SerializeField] private GameObject explosivesRange;
    [SerializeField] public ParticleSystem bossMobDie;
    private GameObject range;

    private void Start()
    {
        this.Stat.halfHealth = base.Stat.Health / 2;
        inGameUI = GameObject.FindGameObjectWithTag("ship").GetComponent<InGameUI>();
        inGameUI.InitEnemyHP(Stat.Health);
    }

    protected override void Update()
    {
        base.Update();
    }

    public void Skill2()
    {
        if (explosivesPrefab == null)
        {
            UnityEngine.Debug.LogError("폭탄 프리팹이 설정되지 않았습니다.");
            return;
        }
        Vector3 spawnPosition = transform.position + new Vector3(0, 4f, 0);
        GameObject explosivePrefab = Instantiate(explosivesPrefab, spawnPosition, Quaternion.identity);

        Explosive explosive = explosivePrefab.GetComponent<Explosive>();
        if (explosive != null)
        {
            explosive.onExplosiveBomb += OnExplosiveRangeDelete;
        }

        Vector3 targetPosition = player.transform.position;

        range = GameObject.Instantiate(explosivesRange, targetPosition, Quaternion.identity);

        // 목표 방향 계산
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0; // 수평 방향만 고려
        float distance = direction.magnitude; // 수평 거리

        // 초기 속도 설정
        Rigidbody rb = explosivePrefab.GetComponent<Rigidbody>();
        if (rb == null)
        {
            UnityEngine.Debug.LogError("폭탄 프리팹에 Rigidbody가 없습니다.");
            return;
        }

        float launchHeight = 4f; // 던지는 초기 높이
        float horizontalSpeed = distance / Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) / launchHeight);
        Vector3 velocity = direction.normalized * horizontalSpeed;
        velocity.y = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * launchHeight); // 수직 속도 계산

        rb.velocity = velocity;
    }

    private void OnExplosiveRangeDelete()
    {
        Destroy(range);
    }
}
