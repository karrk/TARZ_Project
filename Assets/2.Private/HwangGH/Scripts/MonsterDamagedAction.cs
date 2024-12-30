using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Unity.VisualScripting;

public class MonsterDamagedAction : Action
{
	public float monsterHp;
	public SharedFloat maxHP;

    public override void OnAwake()
    {
		monsterHp = 100;
    }

    public override void OnStart()
	{
        
    }

	public override TaskStatus OnUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
            maxHP.Value -= 25;
			Debug.Log($"{maxHP.Value}");
        }

		if (Input.GetKeyDown(KeyCode.A))
		{
			monsterHp = maxHP.Value;

        }
		return TaskStatus.Success;
	}
}