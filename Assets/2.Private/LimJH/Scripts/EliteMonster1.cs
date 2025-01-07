public class EliteMonster1 : BaseMonster
{
    public bool canJumpAttack = true;
    public float jumpAttackCoolTime;


    protected override void Update()
    {
        base.Update();

        //if (behaviorTree != null && behaviorTree.GetVariable("canJumpAttack") != null)
        //{
        //    behaviorTree.SetVariableValue("canJumpAttack", canJumpAttack);
        //}
    }
}
