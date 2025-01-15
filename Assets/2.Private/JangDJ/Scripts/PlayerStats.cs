using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

// 프로젝트 컨텍스트 - 씬 전환시에도 상태를 유지시키기 위함
public class PlayerStats : IInitializable, ITickable
{
    [Inject] private ProjectInstaller.PlayerSettings setting;
    [Inject] private ProjectInstaller.PlayerBaseStats baseStat;
    private ProjectInstaller.PlayerBaseStats equipStats = new ProjectInstaller.PlayerBaseStats();
    
    //[Inject] private PlayerEquipment equips;
    [Inject] private CoroutineHelper helper;

    [Inject] private StaticEquipment newEquips;

    public event Action<float> OnChangedCurMana;
    public event Action<float> OnChangedMaxHP;
    public event Action<float> OnChangedCurHP;
    public event Action<float> OnChangedCurStamina;
    public event Action<float> OnChangedMaxStamina;
    public event Action<float> OnChangedCurThrowCount;
    public event Action<float> OnChangedMaxThrowCount;

    // 장비 정보를 담는 클래스 객체
    // hp 는 캐릭터에서 한번 긁어간다.

    public float Atk => baseStat.AttackPower + equipStats.AttackPower * 0.1f;

    private float throwCapacity;
    public float ThrowCapacity => throwCapacity;

    private float maxMana;
    public float CurMana { get; private set; }
    private float ManaAbsorption => baseStat.ManaAbsorption + equipStats.ManaAbsorption;

    private float staminaRecoveryRate;
    private float maxStamina => baseStat.MaxStamina + equipStats.MaxStamina;
    public float MaxStamina => baseStat.MaxStamina + equipStats.MaxStamina;
    private float curStamina;
    public float CurStamina => curStamina;

    public float CurHealth { get; private set; }
    public float MaxHealth => baseStat.MaxHealth + equipStats.MaxHealth;
    public float MovementSpeed => baseStat.MovementSpeed + equipStats.MovementSpeed * 0.01f;

    public float AtkSpeed => baseStat.AttackSpeed + equipStats.AttackSpeed * 0.001f;

    private int removeCount;

    #region 블루칩 모드

    public bool UsedMeleePowerUp;
    public bool ExpMode;
    public bool ZeroGarbageMode;
    public bool AbsorbHpMode;
    public bool SwitchBGM; 

    #endregion

    public void AddBlueChip(BlueChipType type)
    {
        switch (type)
        {
            case BlueChipType.Melee:
                UsedMeleePowerUp = true;
                break;
            case BlueChipType.Exp:
                ExpMode = true;
                break;
            case BlueChipType.RangeAttack:
                ZeroGarbageMode = true;
                break;
            case BlueChipType.AbsorbHp:
                AbsorbHpMode = true;
                break;
            case BlueChipType.BGM:
                SwitchBGM = true;
                break;
            default:
                break;
        }
    }

    public void MobDeadCountup()
    {
        if (AbsorbHpMode == false)
            return;

        removeCount++;

        if(removeCount >= 10)
        {
            AddHP(1);
            removeCount = 0;
        }
    }

    public void Initialize()
    {
        maxMana = setting.BasicSetting.MaxMana;
        CurMana = 0;
        staminaRecoveryRate = baseStat.StaminaRecoveryRate;
        throwCapacity = baseStat.ThrowableItemCapacity;
        CurHealth = MaxHealth;

        newEquips.OnChangedEquip += RenewalStats;

    }

    public void RenewalStats()
    {
        var equips = newEquips.manager.equipped;

        foreach (var item in equips)
        {
            switch (item.optionType)
            {
                case OptionType.MOVESPD:
                    equipStats.MovementSpeed = item.optionValue;
                    Debug.Log($"추가이속 {item.optionValue}");
                    break;
                case OptionType.ATK:
                    equipStats.AttackPower = item.optionValue;
                    Debug.Log($"추가공격력 {item.optionValue}");
                    break;
                case OptionType.ATKSPD:
                    equipStats.AttackSpeed = item.optionValue;
                    break;
                case OptionType.HP:
                    equipStats.MaxHealth = item.optionValue;
                    Debug.Log($"추가체력 {item.optionValue}");
                    OnChangedMaxHP(equipStats.MaxHealth + baseStat.MaxHealth);
                    OnChangedCurHP(CurHealth);
                    break;
                case OptionType.INVENTORY:
                    equipStats.ThrowableItemCapacity = (int)item.optionValue;
                    Debug.Log($"추가인벤 {item.optionValue}");
                    OnChangedMaxThrowCount?.Invoke(baseStat.ThrowableItemCapacity + equipStats.ThrowableItemCapacity);
                    break;
                case OptionType.GAUGEINC:
                    equipStats.ManaAbsorption = item.optionValue;
                    Debug.Log($"추가마나 {item.optionValue}");
                    break;
                case OptionType.STAMINA:
                    equipStats.MaxStamina = item.optionValue;
                    Debug.Log($"추가스테 {item.optionValue}");
                    OnChangedMaxStamina(equipStats.MaxStamina + baseStat.MaxStamina);
                    break;
                case OptionType.LUCK:
                    equipStats.Luck = item.optionValue;
                    Debug.Log($"추가럭 {item.optionValue}");
                    break;
            }
        }
    }

    /// <summary>
    /// 씬전환시 각 수치를 갱신하기 위한 함수
    /// </summary>
    public void SceneChangedFunction()
    {
        UseStamina(-0.1f);
        AddHP(0);
        OnChangedCurMana?.Invoke(CurMana);
    }

    #region 이속

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

    #endregion

    #region 기본 공격력


    #endregion

    #region 가비지

    private void UpdateGarbageCapacity()
    {
        throwCapacity = baseStat.ThrowableItemCapacity + equipStats.ThrowableItemCapacity;
    }

    public void UpdateGarbageCount(float count)
    {
        OnChangedCurThrowCount?.Invoke(count);
    }

    #endregion

    #region 스테미너

    private bool usedStamina = false;
    private Coroutine ChargeRoutine;
    private bool forceStopUseStamina = false;

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

        if (ChargeRoutine != null)
            helper.StopCoroutine(ChargeRoutine);

        helper.StartCoroutine(StaminaChargeRoutine());

        return true;
    }

    public void SetForceStopUseStamina()
    {
        forceStopUseStamina = true;
    }

    public async void UseStamina(float needValue, Action<InputAction.CallbackContext> endAction)
    {
        usedStamina = true;
        forceStopUseStamina = false;

        if (ChargeRoutine != null)
            helper.StopCoroutine(ChargeRoutine);

        await UseStaminaRoutine(needValue, endAction);

        helper.StartCoroutine(StaminaChargeRoutine());
    }

    private async UniTask UseStaminaRoutine(float value, Action<InputAction.CallbackContext> endAction)
    {
        float needStamina = value * Time.deltaTime;

        while (true)
        {
            if (curStamina < needStamina || forceStopUseStamina == true)
                break;

            curStamina = Math.Clamp(curStamina - needStamina, 0, maxStamina);

            OnChangedCurStamina?.Invoke(curStamina);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        if(forceStopUseStamina == false)
        {
            endAction.Invoke(new InputAction.CallbackContext());
        }

        usedStamina = false;
    }

    private IEnumerator StaminaChargeRoutine()
    {
        yield return new WaitForSeconds(setting.BasicSetting.StaminaChargeWaitTime);
        usedStamina = false;

        while (true)
        {
            if (usedStamina == true || curStamina >= maxStamina)
                break;

            curStamina = Math.Clamp(curStamina + staminaRecoveryRate, 0, maxStamina);
            OnChangedCurStamina?.Invoke(curStamina);

            yield return null;
        }
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
            (CurMana + ManaAbsorption, 0, maxMana);
        OnChangedCurMana?.Invoke(CurMana);
    }




    #endregion

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {

        }
    }
}