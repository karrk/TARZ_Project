using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;
public class PlayerUIPresenter : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [Inject] 
    PlayerUIModel playerModel;




    void Start()
    {


        SetUniRxEvent();
      
    }


    public void SetUniRxEvent()
    {
        playerModel.MaxHp
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.PlayerStatusBarPanel.PlayerHpSliderView.SetSliderMax(value))
        .AddTo(this);

        playerModel.Hp
        .Where(value => playerModel.Hp.Value < 0)
        .Subscribe(value => { playerModel.Hp.Value = 0; })
        .AddTo(this);

        playerModel.Hp
       .Where(value => playerModel.Hp.Value > playerModel.MaxHp.Value)
       .Subscribe(value => { playerModel.Hp.Value = playerModel.MaxHp.Value; })
       .AddTo(this);

        playerModel.Hp
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxHp.Value)
       .Subscribe(value => inGameUI.PlayerStatusBarPanel.PlayerHpSliderView.SetSlider(value))
       .AddTo(this);


        playerModel.MaxStamina
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.PlayerStatusBarPanel.PlayerStaminaSliderView.SetSliderMax(value))
        .AddTo(this);

        playerModel.Stamina
        .Where(value => playerModel.Stamina.Value < 0)
        .Subscribe(value => { playerModel.Stamina.Value = 0; })
        .AddTo(this);

        playerModel.Stamina
       .Where(value => playerModel.Stamina.Value > playerModel.MaxStamina.Value)
       .Subscribe(value => { playerModel.Stamina.Value = playerModel.MaxStamina.Value; })
       .AddTo(this);


        playerModel.Stamina
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxStamina.Value)
       .Subscribe(value => inGameUI.PlayerStatusBarPanel.PlayerStaminaSliderView.SetSlider(value))
       .AddTo(this);


        playerModel.SkillGauge
        .Where(value => playerModel.SkillGauge.Value < 0)
        .Subscribe(value => { playerModel.SkillGauge.Value = 0; })
        .AddTo(this);

        playerModel.SkillGauge
       .Where(value => playerModel.SkillGauge.Value > 100)
       .Subscribe(value => { playerModel.SkillGauge.Value = 100; })
       .AddTo(this);

        playerModel.SkillGauge
       .Where(value => value >= 0)
       .Where(value => value <= 100)
       .Subscribe(value => inGameUI.PlayerStatusBarPanel.PlayerSkillSliderView.SetSlider(value))
       .AddTo(this);



        playerModel.MaxGarbageCount
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.PlayerStatusBarPanel.GarbageInventoryView.SetSliderMax(value))
        .AddTo(this);

        playerModel.GarbageCount
        .Where(value => playerModel.GarbageCount.Value < 0)
        .Subscribe(value => { playerModel.GarbageCount.Value = 0; })
        .AddTo(this);

        playerModel.GarbageCount
       .Where(value => playerModel.GarbageCount.Value > playerModel.MaxGarbageCount.Value)
       .Subscribe(value => { playerModel.GarbageCount.Value = playerModel.MaxGarbageCount.Value; })
       .AddTo(this);

        playerModel.GarbageCount
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxGarbageCount.Value)
       .Subscribe(value => inGameUI.PlayerStatusBarPanel.GarbageInventoryView.SetSlider(value))
       .AddTo(this);



        playerModel.TargetEXP
       .Where(value => playerModel.TargetEXP.Value < 0)
       .Subscribe(value => { playerModel.TargetEXP.Value = 0; })
       .AddTo(this);


        playerModel.TargetEXP
         .Where(value => value >= 0)
         .Subscribe(value => playerModel.RollingValue())
         .AddTo(this);



        playerModel.CurrentEXP
          .Where(value => value >= 0)
          .Subscribe(value => inGameUI.PlayerStatusBarPanel.PlayerExpView.SetExpText(value))
          .AddTo(this);

       
    }


}
