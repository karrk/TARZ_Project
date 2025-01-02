using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
public class PlayerUIPresenter : IInitializable, IDisposable
{
    [Inject]
    InGameUI inGameUI;

    [Inject]
    PlayerUIModel playerModel; // 씬컨텍스트 요소인데,

    private List<IDisposable> list = new List<IDisposable>();

    public void Dispose()
    {
        //Debug.Log("UI 프레젠터 해제");

        foreach (var item in list)
        {
            item.Dispose();
        }

        list.Clear();
    }

    public void Initialize()
    {
        //Debug.Log("UI 프레젠터 이닛");
        SetUniRxEvent();
    }

    public void SetUniRxEvent()
    {
        list.Add(playerModel.MaxHp
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.StatusBarPanel.PlayerHpSliderView.SetSliderMax(value)));

        list.Add(playerModel.Hp
        .Where(value => playerModel.Hp.Value < 0)
        .Subscribe(value => { playerModel.Hp.Value = 0; }));

        list.Add(playerModel.Hp
       .Where(value => playerModel.Hp.Value > playerModel.MaxHp.Value)
       .Subscribe(value => { playerModel.Hp.Value = playerModel.MaxHp.Value; }));

        list.Add(playerModel.Hp
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxHp.Value)
       .Subscribe(value => inGameUI.StatusBarPanel.PlayerHpSliderView.SetSlider(value)));


        list.Add(playerModel.MaxStamina
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.StatusBarPanel.PlayerStaminaSliderView.SetSliderMax(value)));

        list.Add(playerModel.Stamina
        .Where(value => playerModel.Stamina.Value < 0)
        .Subscribe(value => { playerModel.Stamina.Value = 0; }));

        list.Add(playerModel.Stamina
       .Where(value => playerModel.Stamina.Value > playerModel.MaxStamina.Value)
       .Subscribe(value => { playerModel.Stamina.Value = playerModel.MaxStamina.Value; }));


        list.Add(playerModel.Stamina
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxStamina.Value)
       .Subscribe(value => inGameUI.StatusBarPanel.PlayerStaminaSliderView.SetSlider(value)));


        list.Add(playerModel.SkillGauge
        .Where(value => playerModel.SkillGauge.Value < 0)
        .Subscribe(value => { playerModel.SkillGauge.Value = 0; }));

        list.Add(playerModel.SkillGauge
       .Where(value => playerModel.SkillGauge.Value > 100)
       .Subscribe(value => { playerModel.SkillGauge.Value = 100; }));

        list.Add(playerModel.SkillGauge
       .Where(value => value >= 0)
       .Where(value => value <= 100)
       .Subscribe(value => inGameUI.StatusBarPanel.PlayerSkillSliderView.SetSlider(value)));

        list.Add(playerModel.MaxGarbageCount
        .Where(value => value >= 0)
        .Subscribe(value => inGameUI.StatusBarPanel.GarbageInventoryView.SetTextMax(value)));

        list.Add(playerModel.GarbageCount
        .Where(value => playerModel.GarbageCount.Value < 0)
        .Subscribe(value => { playerModel.GarbageCount.Value = 0; }));

        list.Add(playerModel.GarbageCount
       .Where(value => playerModel.GarbageCount.Value > playerModel.MaxGarbageCount.Value)
       .Subscribe(value => { playerModel.GarbageCount.Value = playerModel.MaxGarbageCount.Value; }));

        list.Add(playerModel.GarbageCount
       .Where(value => value >= 0)
       .Where(value => value <= playerModel.MaxGarbageCount.Value)
       .Subscribe(value => inGameUI.StatusBarPanel.GarbageInventoryView.SetText(value)));



        list.Add(playerModel.TargetEXP
       .Where(value => playerModel.TargetEXP.Value < 0)
       .Subscribe(value => { playerModel.TargetEXP.Value = 0; }));


        list.Add(playerModel.TargetEXP
         .Where(value => value >= 0)
         .Subscribe(value => playerModel.RollingValue()));



        list.Add(playerModel.CurrentEXP
          .Where(value => value >= 0)
          .Subscribe(value => inGameUI.StatusBarPanel.PlayerExpView.SetExpText(value)));


    }


}
