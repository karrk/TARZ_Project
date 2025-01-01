using System;
using UniRx;
using Zenject;
public class PlayerUIPresenter : IInitializable, IDisposable
{
    [Inject]
    InGameUI inGameUI;

    [Inject] 
    PlayerUIModel playerModel;

    public void Dispose()
    {
        playerModel.MaxHp.Dispose();
        playerModel.Hp.Dispose();
        playerModel.MaxStamina.Dispose();
        playerModel.Stamina.Dispose();
        playerModel.SkillGauge.Dispose();
        playerModel.MaxGarbageCount.Dispose();
        playerModel.GarbageCount.Dispose();
        playerModel.TargetEXP.Dispose();
        playerModel.CurrentEXP.Dispose();
    }

    public void Initialize()
    {
        SetUniRxEvent();
    }

    public void SetUniRxEvent()
    {
        playerModel.MaxHp
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.StatusBarPanel.PlayerHpSliderView.SetSliderMax(value));

        playerModel.Hp
        .Where(value => playerModel.Hp.Value < 0)
        .Subscribe(value => { playerModel.Hp.Value = 0; });

        playerModel.Hp
       .Where(value => playerModel.Hp.Value > playerModel.MaxHp.Value)
       .Subscribe(value => { playerModel.Hp.Value = playerModel.MaxHp.Value; });

        playerModel.Hp
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxHp.Value)
       .Subscribe(value => inGameUI.StatusBarPanel.PlayerHpSliderView.SetSlider(value));


        playerModel.MaxStamina
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.StatusBarPanel.PlayerStaminaSliderView.SetSliderMax(value));

        playerModel.Stamina
        .Where(value => playerModel.Stamina.Value < 0)
        .Subscribe(value => { playerModel.Stamina.Value = 0; });

        playerModel.Stamina
       .Where(value => playerModel.Stamina.Value > playerModel.MaxStamina.Value)
       .Subscribe(value => { playerModel.Stamina.Value = playerModel.MaxStamina.Value; });


        playerModel.Stamina
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxStamina.Value)
       .Subscribe(value => inGameUI.StatusBarPanel.PlayerStaminaSliderView.SetSlider(value));


        playerModel.SkillGauge
        .Where(value => playerModel.SkillGauge.Value < 0)
        .Subscribe(value => { playerModel.SkillGauge.Value = 0; });

        playerModel.SkillGauge
       .Where(value => playerModel.SkillGauge.Value > 100)
       .Subscribe(value => { playerModel.SkillGauge.Value = 100; });

        playerModel.SkillGauge
       .Where(value => value >= 0)
       .Where(value => value <= 100)
       .Subscribe(value => inGameUI.StatusBarPanel.PlayerSkillSliderView.SetSlider(value));



        playerModel.MaxGarbageCount
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.StatusBarPanel.GarbageInventoryView.SetTextMax(value));

        playerModel.GarbageCount
        .Where(value => playerModel.GarbageCount.Value < 0)
        .Subscribe(value => { playerModel.GarbageCount.Value = 0; });

        playerModel.GarbageCount
       .Where(value => playerModel.GarbageCount.Value > playerModel.MaxGarbageCount.Value)
       .Subscribe(value => { playerModel.GarbageCount.Value = playerModel.MaxGarbageCount.Value; });

        playerModel.GarbageCount
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxGarbageCount.Value)
       .Subscribe(value => inGameUI.StatusBarPanel.GarbageInventoryView.SetText(value));



        playerModel.TargetEXP
       .Where(value => playerModel.TargetEXP.Value < 0)
       .Subscribe(value => { playerModel.TargetEXP.Value = 0; });


        playerModel.TargetEXP
         .Where(value => value >= 0)
         .Subscribe(value => playerModel.RollingValue());



        playerModel.CurrentEXP
          .Where(value => value >= 0)
          .Subscribe(value => inGameUI.StatusBarPanel.PlayerExpView.SetExpText(value));

       
    }

    
}
