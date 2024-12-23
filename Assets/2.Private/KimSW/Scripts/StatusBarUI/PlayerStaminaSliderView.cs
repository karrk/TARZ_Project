using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaminaSliderView : SliderView
{
    [SerializeField] float duration;


    public override void SetSlider(float value)
    {
        slider.DOKill();
        slider.DOValue(value, duration).SetEase(Ease.Linear);
    }
}
