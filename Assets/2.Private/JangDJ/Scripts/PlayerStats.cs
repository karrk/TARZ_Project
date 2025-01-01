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
    public event Action<float> OnChangedMaxHP;
    public event Action<float> OnChangedCurHP;
    public event Action<float> OnChangedCurStamina;
    public event Action<float> OnChangedMaxStamina;
    public event Action<float> OnChangedCurThrowCount;
    public event Action<float> OnChangedMaxThrowCount;

    // 장비 정보를 담는 클래스 객체
    // hp 는 캐릭터에서 한번 긁어간다.

    public float Atk { get; private set; }

    private float throwCapacity;
    public float ThrowCapacity => throwCapacity;

    private float maxMana;
    public float CurMana { get; private set; }
    private float manaAbsorption;

    private float staminaRecoveryRate;
    private float maxStamina;
    public float MaxStamina => maxStamina;
    private float curStamina;

    public float CurHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public float MovementSpeed { get; private set; }

    // TODO 씬 전환시에도 코루틴과 같은 비동기 작업이 진행될지 확인

    public void Initialize()
    {
        maxMana = setting.BasicSetting.MaxMana;
        CurMana = 0;
        manaAbsorption = baseStat.ManaAbsorption;
        maxStamina = baseStat.MaxStamina;
        staminaRecoveryRate = baseStat.StaminaRecoveryRate;
        throwCapacity = baseStat.ThrowableItemCapacity;
        Atk = baseStat.AttackPower;
        MaxHealth = baseStat.MaxHealth;
        CurHealth = MaxHealth;
        MovementSpeed = baseStat.MovementSpeed;

        equips.AddActionOnChangedEquip(E_StatType.ManaAbsorption, UpdateManaAbsorption);
        equips.AddActionOnChangedEquip(E_StatType.MaxStamina, UpdateMaxStamina);
        equips.AddActionOnChangedEquip(E_StatType.ThrowableItemCapacity, UpdateGarbageCapacity);
        equips.AddActionOnChangedEquip(E_StatType.AttackPower, UpdateAttackPower);
        equips.AddActionOnChangedEquip(E_StatType.MaxHealth, UpdateMaxHP);
        equips.AddActionOnChangedEquip(E_StatType.MovementSpeed, UpdateMovementSpeed);
    }

    /// <summary>
    /// 씬전환시 각 수치를 갱신하기 위한 함수
    /// </summary>
    public void RenewalStat()
    {
        UseStamina(-0.1f);
        AddHP(0);
        OnChangedCurMana?.Invoke(CurMana);
    }

    #region 이속

    private void UpdateMovementSpeed()
    {
        MovementSpeed = baseStat.MovementSpeed + equips.GetStat(E_StatType.MovementSpeed);
    }

    #endregion

    #region 체력

    /// <returns> true = 체력이 0이하의 상태입니다. </returns>
    public bool AddHP(float value)
    {
        // 디펜스 관련 수치가 없음
        CurHealth = Math.Clamp(CurHealth + value, 0, MaxHealth);

        OnChangedCurHP?.Invoke(CurHealth);

        if (CurHealth <= 0)
            return true;

        return false;
    }

    private void UpdateMaxHP()
    {
        this.MaxHealth = baseStat.MaxHealth + equips.GetStat(E_StatType.MaxHealth);
    }

    #endregion

    #region 기본 공격력

    private void UpdateAttackPower()
    {
        this.Atk = baseStat.AttackPower + equips.GetStat(E_StatType.AttackPower);
    }

    #endregion

    #region 가비지

    private void UpdateGarbageCapacity()
    {
        throwCapacity = baseStat.ThrowableItemCapacity + equips.GetStat(E_StatType.ThrowableItemCapacity);
    }

    public void UpdateGarbageCount(float count)
    {
        OnChangedCurThrowCount?.Invoke(count);
    }

    #endregion

    #region 스테미너

    private bool usedStamina = false;
    private CancellationTokenSource staminaThreadToken;

    /// <summary>
    /// 행동에 필요한 스테미너를 사용할 수 있는지 판별합니다.
    /// </summary>
    /// <returns> false = 스테미너 여유분이 부족 </returns>
    public bool UseStamina(float needValue)
    {
        if (curStamina < needValue)
            return false;

        usedStamina = true;

        curStamina = Math.Clamp(curStamina - needValue, 0, maxStamina);

        OnChangedCurStamina?.Invoke(curStamina);

        staminaThreadToken?.Cancel();
        staminaThreadToken = new CancellationTokenSource();

        StaminaChargeRoutine(staminaThreadToken.Token).Forget();

        return true;
    }

    private async UniTask StaminaChargeRoutine(CancellationToken token)
    {
        usedStamina = false;
        await UniTask.Delay((int)(setting.BasicSetting.StaminaChargeWaitTime * 1000));

        while (true)
        {
            if (usedStamina == true || curStamina >= maxStamina)
                break;

            curStamina = Math.Clamp(curStamina + staminaRecoveryRate, 0, maxStamina);
            OnChangedCurStamina?.Invoke(curStamina);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }
    }

    private void UpdateMaxStamina()
    {
        this.maxStamina = baseStat.MaxStamina + equips.GetStat(E_StatType.MaxStamina);
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
        CurMana = Math.Clamp(CurMana - point * 100, 0, maxMana);
        OnChangedCurMana?.Invoke(CurMana);
    }

    /// <summary>
    /// 캐릭터 스탯에 의한 마나 흡수
    /// </summary>
    public void ChargeMana()
    {
        CurMana = Math.Clamp
            (CurMana + manaAbsorption, 0, maxMana);
        OnChangedCurMana?.Invoke(CurMana);
    }

    private void UpdateManaAbsorption()
    {
        manaAbsorption = baseStat.ManaAbsorption + equips.GetStat(E_StatType.ManaAbsorption);
    }

    #endregion
}