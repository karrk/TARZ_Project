using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Cysharp.Threading.Tasks.Triggers;

public class DetectPlayer : Action
{
	[SerializeField] PlayerController player;
	[SerializeField] Transform targetPlayer;

	public void LookAtPlayer()
	{

	}

}