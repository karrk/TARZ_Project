using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class LongRangeSkill_1 : BaseState
{
    [SerializeField] private ProjectPlayer player;

    public LongRangeSkill_1(ProjectPlayer player)
    {
        this.player = player;
        this.delay = 0.5f;
    }

    [SerializeField] private GameObject hitBox;
    public GameObject HitBox { get { return hitBox; }  set { hitBox = value; } }

    [SerializeField] private float delay;
    [SerializeField] private float curDelay;

    public override void Enter()
    {
        Debug.Log("스킬 1 시전 시작!");
        curDelay = delay;
    }

    public override void Update()
    {
        if(curDelay > 0f)
        {
            curDelay -= Time.deltaTime;
            
        }
        else
        {
            if (!hitBox.activeSelf)
            {
                hitBox.SetActive(true);
                Debug.Log("원거리 스킬 1 활성화됨");
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
