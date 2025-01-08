using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class LongRangeSkill_2 : BaseState
{

    private GameObject armUnit => player.Refernece.Skill2_ArmUnit;

    //[SerializeField] private ProjectPlayer player;
    [SerializeField] private float skillDamage;       // 스킬 공격력. 현재 기획서상 200 데미지

    // 판정 범위 거리
    [SerializeField] private float viewArea;

    // 판정 범위 각도
    [Range(0, 360)]
    [SerializeField] private float viewAngle;

    // 충돌 감지 대상
    [SerializeField] private LayerMask targetMask;

    // 충돌 감지된 오브젝트들을 담는 리스트
    [HideInInspector]
    [SerializeField] private List<Transform> Targets = new List<Transform>();   // 감지된 오브젝트를 담아두는 배열. 현재로선 사용안되도 됨

    [SerializeField] private float curDelay;
    private bool isStartSkill;

    public LongRangeSkill_2(ProjectPlayer player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("스킬 2 시전 시작!");
        curDelay = player.Setting.Skill2Setting.Delay;

        CameraController cameraController = player.Cam.GetComponent<CameraController>();

        if (cameraController != null)
        {

            Vector3 cameraEuler = player.Cam.transform.rotation.eulerAngles;
            Vector3 playerEuler = player.transform.rotation.eulerAngles;

            player.transform.rotation = Quaternion.Euler(playerEuler.x, cameraEuler.y, cameraEuler.z);

        }

        armUnit.SetActive(true);
    }

    //public override void Update()
    //{
    //    if (curDelay > 0f)
    //    {
    //        curDelay -= Time.deltaTime;

    //    }
    //    else
    //    {
    //        if(!isStartSkill)
    //        {
    //            isStartSkill = true;
    //            GetTarget();
    //            Debug.Log("원거리 스킬 2 활성화됨");
    //            curDelay = player.Setting.Skill2Setting.Delay;
    //        }
    //        else
    //        {
    //            Debug.Log("딜레이 다 지나감");
    //            player.ChangeState(E_State.Idle);
    //        }
    //    }
    //}

    public void LongRangeSkill_2_On()
    {
        player.StartCoroutine(DelayCoroutine());
    }

    public override void Exit()
    {
        isStartSkill = false;
        armUnit.SetActive(false);
    }


    public void GetTarget()
    {
        Targets.Clear();    // 배열 초기화
        Collider[] TargetCollider = Physics.OverlapSphere(player.transform.position, player.Setting.Skill2Setting.ViewArea, player.Setting.Skill2Setting.TargetMask);

        for (int i = 0; i < TargetCollider.Length; i++) // 감지된 콜라이더 
        {
            Transform target = TargetCollider[i].transform;
            Vector3 direction = target.position - player.transform.position;

            if (Vector3.Dot(direction.normalized, player.transform.forward) > GetAngle(player.Setting.Skill2Setting.ViewAngle / 2).z)
            {
                Debug.Log(GetAngle(player.Setting.Skill2Setting.ViewAngle / 2).z);
                Targets.Add(target);

                // TODO : 스킬 2번을 사용하였을때 IDamagable 인터페이스를 가지고 있는 몬스터를 확인해서 해당 컴포넌트를 가지고 있으면
                // 데미지를 입히는 방식으로 우선 구현
                IDamagable damagable = target.GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeHit(player.Setting.Skill2Setting.Damage, false);
                }

            }
        }
    }

    public Vector3 GetAngle(float AngleInDegree)
    {
        return new Vector3(Mathf.Sin(AngleInDegree * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegree * Mathf.Deg2Rad));
    }

    private IEnumerator DelayCoroutine()
    {
        GetTarget();
        yield return new WaitForSeconds(curDelay);
        player.ChangeState(E_State.Idle);
    }
}
