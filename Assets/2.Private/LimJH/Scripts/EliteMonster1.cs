using System.Diagnostics;
using UnityEngine;

public class EliteMonster1 : BaseMonster
{
    public float jumpAttackCoolTime;
    [SerializeField] private GameObject shockwavePrefab;

    protected override void Update()
    {
        base.Update();

        //if (behaviorTree != null && behaviorTree.GetVariable("canJumpAttack") != null)
        //{
        //    behaviorTree.SetVariableValue("canJumpAttack", canJumpAttack);
        //}
    }

    public void EndJumpAttack()
    {
        if (shockwavePrefab == null)
        {
            UnityEngine.Debug.LogError("충격파 프리팹이 설정되지 않았습니다.");
            return;
        }

        GameObject projectile = Instantiate(shockwavePrefab, transform.position, Quaternion.identity);
    }
}
