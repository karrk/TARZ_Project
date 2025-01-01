using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Zenject;

// 프로젝트 컨텍스트 - 씬 전환시에도 상태를 유지시키기 위함
public class PlayerStats : IInitializable
{
    [Inject] private ProjectInstaller.PlayerSettings setting;
    [Inject] private ProjectInstaller.PlayerBaseStats baseStat;
    [Inject] private PlayerEquipment equips;

    public event Action<float> OnChangedCurMana;
    public event Action<float> OnChangedCurStamina;
    public event Action<float> OnChangedMaxStamina;

    // 장비 정보를 담는 클래스 객체
    // hp 는 캐릭터에서 한번 긁어간다.

    public float Atk;
    public float ThrowCapacity;

    public float MaxMana;
    public float CurMana;
    public float ManaAbsorption;

    public float StaminaRecoveryRate;
    public float MaxStamina;
    public float CurStamina;

    public float MaxHealth;
    public float MovementSpeed;

    // TODO 씬 전환시에도 코루틴과 같은 비동기 작업이 진행될지 확인

    public void Initialize()
    {
        MaxMana = setting.BasicSetting.MaxMana;
        equips.AddActionOnChangedEquip(E_StatType.ManaAbsorption, UpdateManaAbsorption);
        equips.AddActionOnChangedEquip(E_StatType.MaxStamina, UpdateMaxStamina);
    }

    #region 스테미너

    private bool usedStamina = false;
    private CancellationTokenSource staminaThreadToken;

    /// <summary>
    /// 행동에 필요한 스테미너를 사용할 수 있는지 판별합니다.
    /// </summary>
    /// <returns> false = 스테미너 여유분이 부족 </returns>
    public bool UseStamina(float needValue)
    {
        if (CurStamina < needValue)
            return false;

        CurStamina = Math.Clamp(CurStamina - needValue, 0, MaxStamina);

        OnChangedCurStamina?.Invoke(CurStamina);

        staminaThreadToken?.Cancel();
        staminaThreadToken = new CancellationTokenSource();

        StaminaChargeRoutine(staminaThreadToken.Token).Forget();

        usedStamina = true;

        return true;
    }

    private async UniTask StaminaChargeRoutine(CancellationToken token)
    {
        usedStamina = false;
        await UniTask.Delay((int)(setting.BasicSetting.StaminaChargeWaitTime * 1000));

        while (true)
        {
            if (usedStamina == true || CurStamina >= MaxStamina)
                break;

            CurStamina = Math.Clamp(CurStamina + StaminaRecoveryRate, 0, MaxStamina);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private void UpdateMaxStamina()
    {
        this.MaxStamina = baseStat.MaxStamina + equips.GetStat(E_StatType.MaxStamina);
    }


    #endregion

    #region 스킬, 마나 관련

    /// <returns> 0 = 사용 불가 외 나머지는 스킬 번호를 반환</returns>
    public int UseSkill()
    {
        int skillNumber = GetSkillNumber();

        if (skillNumber == 0)
            return 0;

        ReduceMana(skillNumber);

        return skillNumber;
    }

    private int GetSkillNumber()
    {
        int number = 0;

        for (int i = 0; i < setting.BasicSetting.SkillAnchor.Length; i++)
        {
            if (CurMana / 100f < setting.BasicSetting.SkillAnchor[i])
                break;

            number++;
        }

        return number;
    }

    private void ReduceMana(int skillNumber)
    {
        float point = setting.BasicSetting.SkillAnchor[skillNumber - 1];
        CurMana = Math.Clamp(CurMana - point * 100, 0, MaxMana);
        OnChangedCurMana?.Invoke(CurMana);
    }

    /// <summary>
    /// 캐릭터 스탯에 의한 마나 흡수
    /// </summary>
    public void ChargeMana()
    {
        CurMana = Math.Clamp
            (CurMana + ManaAbsorption, 0, MaxMana);
        OnChangedCurMana?.Invoke(CurMana);
    }

    private void UpdateManaAbsorption()
    {
        ManaAbsorption = baseStat.ManaAbsorption + equips.GetStat(E_StatType.ManaAbsorption);
    }

    #endregion
}

// 장비 상태 ?? 클래스

// ui에서 장비를 착용했을때, 이 클래스 내부에 업데이트시킨다.

// ui에서 장비가 교체될때, 이 클래스 내부에 업데이트 시킨다.

// 기획서없다, 간소화가 어느정도인지 모르겠다,
// 간단하게 hp , atk, def,
