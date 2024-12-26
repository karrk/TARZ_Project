using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class LongRangeSkill_4 : BaseState
{
    //[SerializeField] private ProjectPlayer player;

    //public LongRangeSkill_4(ProjectPlayer player)
    //{
    //    this.player = player;
    //    this.radius = 3f;
    //    this.zOffset = 4f;
    //    this.skillDelay = 0.5f;
    //    this.skillDamage = 0.5f;
    //    this.enemyLayer = LayerMask.GetMask("Monster");
    //}

    [SerializeField] private float radius = 3f; // 원의 반지름
    [SerializeField] private float zOffset = 4f; // 플레이어에서 z축으로 떨어진 거리
    [SerializeField] private float skillDelay = 1f; // 스킬 딜레이
    [SerializeField] private float skillDamage = 3f; // 스킬 데미지
    [SerializeField] private LayerMask enemyLayer;

    public LongRangeSkill_4(ProjectPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("원거리 스킬 4번 실행!");

        CameraController cameraController = player.Cam.GetComponent<CameraController>();

        if (cameraController != null)
        {

            Vector3 cameraEuler = player.Cam.transform.rotation.eulerAngles;
            Vector3 playerEuler = player.transform.rotation.eulerAngles;

            player.transform.rotation = Quaternion.Euler(playerEuler.x, cameraEuler.y, cameraEuler.z);

        }

        player.StartCoroutine(GetTargetCouroutine());
    }

    private IEnumerator GetTargetCouroutine()
    {
        // 플레이어 앞쪽의 원 중심 계산
        Vector3 center = player.transform.position + player.transform.forward * player.Setting.Skill4Setting.zOffset; 

        Debug.Log($"스킬 범위 중심 : {center}");

        yield return new WaitForSeconds( player.Setting.Skill4Setting.Delay );

        Collider[] hitcolliders = Physics.OverlapSphere( center, player.Setting.Skill4Setting.Radius , player.Setting.Skill4Setting.TargetMask);
        foreach( Collider hitCollider in hitcolliders)
        {
            IDamagable damagable = hitCollider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeHit(player.Setting.Skill4Setting.Damage, true);
            }
        }

        player.ChangeState(E_State.Idle);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Vector3 center = player.transform.position + player.transform.forward * zOffset;
    //    Gizmos.DrawWireSphere( center, radius );
    //}
}
