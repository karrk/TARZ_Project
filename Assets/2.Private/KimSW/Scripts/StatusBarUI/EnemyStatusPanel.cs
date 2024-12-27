using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class EnemyStatusPanel : AnimatedUI
{
    [SerializeField] EnemyHpView hpView;
    private void Awake()
    {
        SetMoveOffset();
        rectTransform.anchoredPosition = positionOffset;
    }

    public bool EnemyHpViewCheck()
    {
        if(hpView.Hp.Value > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetEnemyHP(int hp)
    {
        hpView.SetEnemyHp(hp);

    }
}
