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
        this.delay = 0.5f;
    }

    [SerializeField] private float delay;
}
