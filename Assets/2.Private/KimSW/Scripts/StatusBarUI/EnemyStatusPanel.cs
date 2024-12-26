using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class EnemyStatusPanel : AnimatedUI
{
    private void Awake()
    {
        SetMoveOffset();
        rectTransform.anchoredPosition = positionOffset;
    }

}
