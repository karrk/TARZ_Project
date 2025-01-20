using System.Diagnostics;
using UnityEngine;

public class EliteMonster2 : BaseMonster
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] protected override float headHeight => 2.5f;

    protected override void Update()
    {
        base.Update();

        //if (behaviorTree != null && behaviorTree.GetVariable("canJumpAttack") != null)
        //{
        //    behaviorTree.SetVariableValue("canJumpAttack", canJumpAttack);
        //}
    }

    public void EndBombAttack()
    {
        if (bombPrefab == null)
        {
            UnityEngine.Debug.LogError("충격파 프리팹이 설정되지 않았습니다.");
            return;
        }

        GameObject projectile = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Return();
    }
}
