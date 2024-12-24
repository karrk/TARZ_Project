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
        this.delay = 1.5f;
    }

    [SerializeField] private float delay;
    [SerializeField] private GameObject hitBox;
    public GameObject HitBox { get { return hitBox; } set { hitBox = value; } }

    [SerializeField] private float curDelay;

    public override void Enter()
    {
        Debug.Log("스킬 1 시전 시작!");
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
            if (!hitBox.activeSelf)
            {
                hitBox.SetActive(true);
                Debug.Log("원거리 스킬 3 활성화됨");
                curDelay = delay;
            }
            else
            {
                Debug.Log("딜레이 다 지나감");
                player.ChangeState(E_State.Idle);
            }
        }
    }

    public override void Exit()
    {
        hitBox.SetActive(false);
    }
}
