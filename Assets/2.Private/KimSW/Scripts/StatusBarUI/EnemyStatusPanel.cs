using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class EnemyStatusPanel : MonoBehaviour
{
    public EnemyHpView hpView;
 

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

    public void SetEnemyHP(float hp)
    {
        hpView.SetEnemyHp(hp);

    }
}
