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
    }

    [SerializeField] private GameObject hitbox;
    [SerializeField] private float delay;

    public override void Enter()
    {
        Debug.Log("스킬 1 시전 시작!");
    }

    public override void Update()
    {
        if(delay > 0f)
        {
            delay -= Time.deltaTime;
        }
        else
        {
            hitbox.SetActive(true);
        }
    }

    public override void Exit()
    {
        hitbox.SetActive(false);
    }
}
