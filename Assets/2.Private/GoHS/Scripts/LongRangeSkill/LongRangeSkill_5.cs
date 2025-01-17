using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LongRangeSkill_5 : BaseState
{
    private GameObject garbages => player.Refernece.Skill5Garbages;

    public LongRangeSkill_5(ProjectPlayer player) : base(player)
    {
    }

    public float Radius => player.Setting.Skill5Setting.Radius;
    public Vector3 AnchorPos => garbages.transform.position;

    public override void Enter()
    {
        player.StartCoroutine(ActionSkill());
        player.Refernece.Animator.SetBool("LongRange5", true);

        player.Refernece.EffectController.UseSkillEffect();
        player.Refernece.EffectController.LongRangeSkill_5Effect_Start();
        player.SoundManager.SetLoopSKillSound(true);
        player.SoundManager.SkillSoundStart(E_Audio.Char_LongRangeSkill_5_Rotate);
    }

    private IEnumerator ActionSkill()
    {
        float rotateTimer = 0;

        yield return new WaitForSeconds(player.Setting.Skill5Setting.StartDelay);
        garbages.SetActive(true);

        while (true)
        {
            if(player.Setting.Skill5Setting.RotateTime <= rotateTimer) { break; }

            Rotation();
            GetTarget();

            rotateTimer += Time.deltaTime;
            yield return null;
        }

        garbages.SetActive(false);
        player.Refernece.EffectController.LongRangeSkill_5Effect_End();
        Shoot();
        yield return new WaitForSeconds(player.Setting.Skill5Setting.EndDelay);


        player.Refernece.Animator.SetBool("LongRange5", false);
        player.ChangeState(E_State.Idle);
    }

    private void Rotation()
    {
        garbages.transform.Rotate(Vector3.up * Time.deltaTime * player.Setting.Skill5Setting.RotateSpeed, Space.Self);
    }

    private void Shoot()
    {
        Vector3 randPos;
        Vector3 dir;

        for (int i = 0; i < player.Setting.Skill5Setting.ThrowCount; i++)
        {
            randPos = GetRandomPos();
            dir = (randPos - AnchorPos).normalized;
            player.Refernece.Shooter.FireItem(randPos, dir, 500);
        }

        player.Refernece.Animator.SetTrigger("LongRange5_End");
        player.SoundManager.SkillSoundStop();
        player.SoundManager.PlaySFX(E_Audio.Char_LongRangeSkill_5_Shoot);
    }

    private Vector3 GetRandomPos()
    {
        Vector3 randDir = new Vector3(
            Random.Range(-1f, 1f),
            0,
            Random.Range(-1f, 1f));

        randDir = randDir.normalized;

        Vector3 newPos = AnchorPos + randDir * player.Setting.Skill5Setting.Radius;

        return newPos;
    }

    public void GetTarget()
    {
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, player.Setting.Skill5Setting.ViewArea, player.Setting.Skill5Setting.TargetMask);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(player.Setting.Skill5Setting.ViewAngle / 2).z)
            {
                Debug.Log(GetAngle(player.Setting.Skill5Setting.ViewAngle / 2).z);

                // TODO : 스킬 2번을 사용하였을때 IDamagable 인터페이스를 가지고 있는 몬스터를 확인해서 해당 컴포넌트를 가지고 있으면
                // 데미지를 입히는 방식으로 우선 구현
                IPushable pushable = target.GetComponent<IPushable>();
                if (pushable != null)
                {
                    pushable.Push(player.transform.position, E_SkillType.LongRangeSkill5);
                }

            }
        }
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }

}
