using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarbageInventoryView : SliderView
{
    [SerializeField] TMP_Text currentText;
    [SerializeField] TMP_Text maxText;


    public override void SetSlider(float value)
    {
        slider.value = value;
        currentText.text = value.ToString("000");
    }

    public override void SetSliderMax(float value)
    {
        slider.maxValue = value;
        maxText.text = value.ToString("000");

    }
}
