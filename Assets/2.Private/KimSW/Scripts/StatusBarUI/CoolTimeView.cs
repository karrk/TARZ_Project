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

    bool[] isStart = new bool[2];

    private void Awake()
    {
        SetMoveOffset();
        rectTransform.anchoredPosition = positionOffset;
    }

    public void StartCoolTime(int num)
    {
        if (!isStart[num])
        {

            EnableCooltimeText(num);
            SetSpriteDisable(num);
            isStart[num] = true;
        }
    }

    public void EndCoolTime(int num)
    {
        DisableCooltimeText(num);
        SetSpriteEnable(num);
        isStart[num] = false;
    }
    public void SetSkillTimeValue(float value, int num)
    {
        timeText[num].text = value.ToString("N0");
        StartCoolTime(num);
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
