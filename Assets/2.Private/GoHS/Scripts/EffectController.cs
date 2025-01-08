using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    // 대쉬 이펙트
    [SerializeField] private ParticleSystem Dash_LeftHand; 
    [SerializeField] private ParticleSystem Dash_RightHand;

    // 원거리 스킬 1번
    [SerializeField] private ParticleSystem longRangeSkill_1;

    // 원거리 스킬 2번
    [SerializeField] private ParticleSystem longRangeSkill_2;

    // 원거리 스킬 4번
    [SerializeField] private GameObject longRangeSkill_4;

    // 원거리 스킬 5번
    [SerializeField] private GameObject longRangeSkill_5_Wind;
    [SerializeField] private ParticleSystem longRangeSkill_5_End;

    // 근거리 스킬 1번
    [SerializeField] private ParticleSystem MeleeSkill_1;

    // 근거리 스킬 2번
    [SerializeField] private GameObject MeleeSkill_2;

    #region 대쉬
    public void DashEffect()
    {
        Dash_LeftHand.Play();
        Dash_RightHand.Play();
    }
    #endregion

    #region 원거리 스킬 1번

    public void LongRangeSkill_1Effect()
    {
        longRangeSkill_1.Play();
    }

    #endregion

    #region 원거리 스킬 2번

    public void LongRangeSkill_2Effect()
    {
        longRangeSkill_2.Play();
    }

    #endregion

    #region 원거리 스킬 4번
    public void LongRangeSkill_4Effect()
    {
        StartCoroutine(Delay_LongRangeSkill_4Coroutine());
    }

    private IEnumerator Delay_LongRangeSkill_4Coroutine()
    {
        longRangeSkill_4.SetActive(true);
        yield return new WaitForSeconds(2f);
        longRangeSkill_4.SetActive(false);
    }
    #endregion

    #region 원거리 스킬 5번

    public void LongRangeSkill_5Effect_Start()
    {
        longRangeSkill_5_Wind.SetActive(true);
    }

    public void LongRangeSkill_5Effect_End()
    {
        longRangeSkill_5_Wind.SetActive(false);
        longRangeSkill_5_End.Play();
    }
    #endregion

    #region 근거리 스킬 1번

    public void MeleeSkill_1Effect()
    {
        MeleeSkill_1.Play();
    }

    #endregion

    #region 근거리 스킬 2번

    public void MeleeSkill_2Effect_Start()
    {
        MeleeSkill_2.SetActive(true);
    }

    public void MeleeSkill_2Effect_End()
    {
        MeleeSkill_2.SetActive(false);
    }

    #endregion
}
