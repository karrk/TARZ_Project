using DG.Tweening;
using UniRx;
using Zenject;

public class PlayerUIModel : IInitializable
{
    public ReactiveProperty<int> MaxHp;
    public ReactiveProperty<int> Hp;
    public ReactiveProperty<float> MaxStamina;
    public ReactiveProperty<float> Stamina;
    public ReactiveProperty<float> SkillGauge ;

    public ReactiveProperty<int> GarbageCount;
    public ReactiveProperty<int> MaxGarbageCount;

    public ReactiveProperty<int> TargetEXP;
    public ReactiveProperty<int> CurrentEXP;
    
    public void Initialize()
    {
        MaxHp = new ReactiveProperty<int>(100);
        //Hp.Value = MaxHp.Value;
        Hp = new ReactiveProperty<int>(MaxHp.Value);
        MaxStamina = new ReactiveProperty<float>(100);
        Stamina = new ReactiveProperty<float>(MaxStamina.Value);
        //Stamina.Value = MaxStamina.Value;
        SkillGauge = new ReactiveProperty<float>(0);

        GarbageCount = new ReactiveProperty<int>(0);
        MaxGarbageCount = new ReactiveProperty<int>(50);

        TargetEXP = new ReactiveProperty<int>(0);
        CurrentEXP = new ReactiveProperty<int>(0);
    }

    public void RollingValue()
    {
        DOVirtual.Int(CurrentEXP.Value, TargetEXP.Value, 0.3f, (x) => { CurrentEXP.Value = x;  });
    }
}
