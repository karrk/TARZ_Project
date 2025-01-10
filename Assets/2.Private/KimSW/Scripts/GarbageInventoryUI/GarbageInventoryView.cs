using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarbageInventoryView : AnimatedUI
{
    [SerializeField] TMP_Text currentText;
    [SerializeField] TMP_Text maxText;

    private void Awake()
    {
        SetMoveOffset();
        rectTransform.anchoredPosition = positionOffset;
    }

    public  void SetText(float value)
    {
        currentText.text = value.ToString("000");
        ShrunkAnimation(currentText.transform);
    }

    public  void SetTextMax(float value)
    {
        maxText.text = value.ToString("000");
        ShrunkAnimation(currentText.transform);
    }
}
