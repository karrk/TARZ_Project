using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoolTimeView : AnimatedUI
{
    [SerializeField] TMP_Text[] timeText;
    [SerializeField] Image[] sprite;

    [SerializeField] Color enableColor;
    [SerializeField] Color disableColor;

    private void Awake()
    {
        SetMoveOffset();
        rectTransform.anchoredPosition = positionOffset;
    }

    public void StartCoolTime(int num)
    {
        EnableCooltimeText(num);
        SetSpriteDisable(num);
    }

    public void EndCoolTime(int num)
    {
        DisableCooltimeText(num);
        SetSpriteEnable(num);
    }
    public void SetSkillTimeValue(float value, int num)
    {
        timeText[num].text = value.ToString("N0");
    }

    public void EnableCooltimeText(int num)
    {
        timeText[num].gameObject.SetActive(true);
    }
    public void DisableCooltimeText(int num)
    {
        timeText[num].gameObject.SetActive(false);
    }

    public void SetSpriteEnable(int num)
    {
        sprite[num].color = enableColor;
    }

    public void SetSpriteDisable(int num)
    {
        sprite[num].color = disableColor;
    }
}
