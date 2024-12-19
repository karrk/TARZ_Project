using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpSliderView : SliderView
{
    [SerializeField] float decreaseDuration;
    [SerializeField] float colorDuration;
    [SerializeField] float hitDuration;

    [SerializeField] Image fillImage;
    [SerializeField] Image hitImage;

    Color hitColor;
    Color normalColor;


    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();

        ColorUtility.TryParseHtmlString("#ff0000", out hitColor);
        ColorUtility.TryParseHtmlString("#d3f1ff", out normalColor);

        fillImage.color = normalColor;

    }

    public override void SetSlider(float value)
    {
        if(value < slider.value)
        {
            DecreaseValue();
        }
     

        slider.DOValue(value, decreaseDuration).SetEase(Ease.Linear);

  
    }

  

    public void DecreaseValue()
    {
    
        hitImage.DOFade(0.7f, 0).OnComplete(() => { hitImage.enabled = true; });
        hitImage.DOFade(0.0f, hitDuration).OnComplete(() => { hitImage.enabled = false; });

        fillImage.color = hitColor;
        ResetColor();
    }

    public void ResetColor()
    {
        fillImage.DOColor(normalColor, colorDuration);
    }
}
