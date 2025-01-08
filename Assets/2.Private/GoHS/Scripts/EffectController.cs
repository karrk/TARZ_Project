using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    // 대쉬 이펙트
    [SerializeField] private ParticleSystem Dash_LeftHand; 
    [SerializeField] private ParticleSystem Dash_RightHand;

    // 원거리 스킬 4번
    [SerializeField] private GameObject longRangeSkill_4;

    // 원거리 스킬 5번
    [SerializeField] private GameObject longRangeSkill_5;

    #region 대쉬
    public void DashEffect()
    {
        Dash_LeftHand.Play();
        Dash_RightHand.Play();
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
        yield return new WaitForSeconds(1f);
        longRangeSkill_4.SetActive(false);
    }
    #endregion

    #region 원거리 스킬 5번

    public void LongRangeSkill_5Effect_Start()
    {
        longRangeSkill_5.SetActive(true);
    }

    public void LongRangeSkill_5Effect_End()
    {
        longRangeSkill_5.SetActive(false);
    }
    #endregion

}
