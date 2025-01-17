using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeleeSkill_1 : BaseState
{
    

    public MeleeSkill_1(ProjectPlayer player) : base(player)
    {
    }

    [SerializeField] private float curDelay;    // 시전중
    [SerializeField] private float coolTime;    // 쿨타임
    private bool isStartSkill;

    private bool canSkill = true;               // 지금 스킬을 사용할 수 있는가?
    public bool CanSkill { get { return canSkill; } set { canSkill = value; } }

    public override void Enter()
    {
       // Debug.Log("근접 스킬 1 시작!");
        curDelay = player.Setting.MeleeSkill1Setting.Delay;
        coolTime = player.Setting.MeleeSkill1Setting.CoolTime;
       
        player.playerUIModel.SkillCoolTime[0].Value = player.Setting.MeleeSkill1Setting.CoolTime;

        player.Refernece.Animator.SetTrigger("MeleeSkill_1");


        canSkill = false;
        player.StartCoroutine(CoolTimeCoroutine());
    }

    public override void Update()
    {
        if (curDelay > 0f)
        {
            curDelay -= Time.deltaTime;

        }
        else
        {
            if (!isStartSkill)
            {
                isStartSkill = true;
                GetTarget();
                player.SoundManager.PlaySFX(E_Audio.Char_MeleeSkill_1);
                //Debug.Log("근접 스킬 1번 활성화됨");
                curDelay = player.Setting.MeleeSkill1Setting.Delay;
            }
            else
            {
                //Debug.Log("딜레이 다 지나감");
                player.ChangeState(E_State.Idle);
            }
        }
    }

    public override void Exit()
    {
        isStartSkill = false;
    }


    public void GetTarget()
    {
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, player.Setting.MeleeSkill1Setting.ViewArea, player.Setting.MeleeSkill1Setting.TargetMask);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(player.Setting.MeleeSkill1Setting.ViewAngle / 2).z)
            {
                Debug.Log(GetAngle(player.Setting.MeleeSkill1Setting.ViewAngle / 2).z);

                // TODO : 스킬 2번을 사용하였을때 IDamagable 인터페이스를 가지고 있는 몬스터를 확인해서 해당 컴포넌트를 가지고 있으면
                // 데미지를 입히는 방식으로 우선 구현
                IDamagable damagable = target.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    int powerFactor = player.stats.UsedMeleePowerUp == true ? 10 : 1;

                    damagable.TakeHit(
                        player.Setting.MeleeSkill1Setting.Damage * powerFactor, false);
                }

                // 플레이어의 3만큼(아마 변경 필요) 앞 방향으로 몬스터에게 위치 전달 
                IPushable pushable = target.GetComponent<IPushable>();
                if (pushable != null)
                {
                    Vector3 pushPosition = player.transform.position + player.transform.TransformDirection(0, 0, player.Setting.MeleeSkill1Setting.Zoffset);
                    pushable.Push(pushPosition, E_SkillType.MeleeSkill1);
                }

            }
        }
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }

    private IEnumerator CoolTimeCoroutine()
    {
       
        while (player.playerUIModel.SkillCoolTime[0].Value > 0)
        {
            player.playerUIModel.SkillCoolTime[0].Value -= Time.deltaTime;
 
            yield return null;
           
        }
        canSkill = true;
    }
}
