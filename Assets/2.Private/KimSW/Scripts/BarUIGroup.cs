using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarUIGroup : AnimatedUI
{
    private void Awake()
    {
        SetMoveOffset();
        rectTransform.anchoredPosition = positionOffset;
    }
}
