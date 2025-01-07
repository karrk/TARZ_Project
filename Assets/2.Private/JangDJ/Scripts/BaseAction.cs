using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BaseAction : Action
{
    public SharedGameObject my;
    public BaseMonster mob;

    public override void OnStart()
    {
        Init();
    }

    private void Init()
    {
        my.SetValue(this.gameObject);
        mob = my.Value.GetComponent<BaseMonster>();
    }
}
