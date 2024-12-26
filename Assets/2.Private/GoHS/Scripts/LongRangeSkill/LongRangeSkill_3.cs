using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LongRangeSkill_3 : BaseState
{
    [SerializeField] private ProjectPlayer player;
    [SerializeField] private float skill_Str;       // 스킬 공격력. 현재 기획서상 200 데미지

    public LongRangeSkill_3(ProjectPlayer player)
    {
        this.player = player;
        this.delay = 1f;
    }

    [SerializeField] private float delay;
    [SerializeField] private GameObject hitBox;
    public GameObject HitBox { get { return hitBox; } set { hitBox = value; } }

    [SerializeField] private float curDelay;
    private bool isStartSkill;

    public override void Enter()
    {
        Debug.Log("스킬 3 시전 시작!");
        curDelay = delay;
    }

    public override void Update()
    {
        if (curDelay > 0f)
        {
            curDelay -= Time.deltaTime;

        }
        else 
        {
            if(!isStartSkill)
            {
                isStartSkill = true;
                player.StartCoroutine(StartSkillCoroutine());
                Debug.Log("원거리 스킬 3 활성화됨");
            }
        }
    }

    public override void Exit()
    {
        hitBox.SetActive(false);
    }

    private IEnumerator StartSkillCoroutine()
    {
        float interval = 0.05f;  // 생성되는 간격
        int spawnCount = 25;    // 생성 횟수
        List<GameObject> punchObjList = new List<GameObject>();

        for (int i = 0; i < spawnCount; i++)
        {
            if (hitBox != null)
            {
                Vector3 spawnPos = player.transform.position + player.transform.forward * 2f;
                GameObject newHitBox = GameObject.Instantiate(HitBox, spawnPos, player.transform.rotation);
                punchObjList.Add(newHitBox);

                newHitBox.SetActive(true);

                Debug.Log($"{i}번 펀치 생성함");

                yield return new WaitForSeconds(interval);
            }
        }

        //foreach (GameObject punchObj in punchObjList)
        //{
        //    GameObject.Destroy(punchObj);
        //}
        Debug.Log("모든 펀치 생성완료");
        isStartSkill = false;
        player.ChangeState(E_State.Idle);
    }
}
