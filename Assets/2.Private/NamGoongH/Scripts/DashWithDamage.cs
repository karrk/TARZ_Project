using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWithDamage : DashAction
{
    public float damage = 10f;

    public override void Execute()
    {
        base.Execute();
        // 데미지 로직 추가
    }
}
