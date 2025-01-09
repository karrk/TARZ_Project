using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class LongRangeSkill_4 : BaseState
{
    //[SerializeField] private ProjectPlayer player;

    public LongRangeSkill_4(ProjectPlayer player) : base(player)
    {
    }

    private GameObject armUnit => player.Refernece.Skill4_ArmUnit;

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
        
        armUnit.SetActive(true);

        player.Refernece.EffectController.UseSkillEffect();
    }


    public override void Exit()
    {
        armUnit.SetActive(false);
    }

    public void LongRangeSkill_4_On()
    {
        player.StartCoroutine(DelayCoroutine());
    }

    private IEnumerator DelayCoroutine()
    {
        // 플레이어 앞쪽의 원 중심 계산
        Vector3 center = player.transform.position + player.transform.forward * player.Setting.Skill4Setting.zOffset;

        Debug.Log($"스킬 범위 중심 : {center}");

        Collider[] hitcolliders = Physics.OverlapSphere(center, player.Setting.Skill4Setting.Radius, player.Setting.Skill4Setting.TargetMask);
        foreach (Collider hitCollider in hitcolliders)
        {
            IDamagable damagable = hitCollider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeHit(player.Setting.Skill4Setting.Damage, false);
            }
        }

        yield return new WaitForSeconds(player.Setting.Skill4Setting.Delay);

        player.ChangeState(E_State.Idle);
    }

}
