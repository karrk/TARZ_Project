using DG.Tweening;
using System.Diagnostics;
using UniRx;
using Zenject;

// 프로젝트 컨텍스트
public class PlayerUIModel : IInitializable
{
    [Inject] private PlayerStats stats;
    [Inject] private GarbageQueue garbages;

    public ReactiveProperty<float> MaxHp;
    public ReactiveProperty<float> Hp;
    public ReactiveProperty<float> MaxStamina;
    public ReactiveProperty<float> Stamina;
    public ReactiveProperty<float> SkillGauge ;

    public ReactiveProperty<float>[] SkillCoolTime = new ReactiveProperty<float>[2];

    public ReactiveProperty<int> GarbageCount;
    public ReactiveProperty<int> MaxGarbageCount;

    public ReactiveProperty<int> TargetEXP;
    public ReactiveProperty<int> CurrentEXP;
    
    public void Initialize()
    {
        MaxHp = new ReactiveProperty<float>(stats.MaxHealth);
        //Hp.Value = MaxHp.Value;
        Hp = new ReactiveProperty<float>(stats.CurHealth);
        MaxStamina = new ReactiveProperty<float>(stats.MaxStamina);
        Stamina = new ReactiveProperty<float>(stats.CurStamina);
        //Stamina.Value = MaxStamina.Value;
        SkillGauge = new ReactiveProperty<float>(stats.CurMana);

        GarbageCount = new ReactiveProperty<int>(garbages.Count);
        MaxGarbageCount = new ReactiveProperty<int>((int)stats.ThrowCapacity);

        for (int i = 0; i < SkillCoolTime.Length; i++)
        {
            SkillCoolTime[i] = new ReactiveProperty<float>(0);
        }
        TargetEXP = new ReactiveProperty<int>(0);
        CurrentEXP = new ReactiveProperty<int>(0);
    }

    public void RollingValue()
    {
        DOVirtual.Int(CurrentEXP.Value, TargetEXP.Value, 0.3f, (x) => { CurrentEXP.Value = x;  }).SetUpdate(true);
    }
}
