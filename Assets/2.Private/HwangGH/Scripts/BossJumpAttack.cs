using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class BossJumpAttack : Action
{
	[Header("GameObject")]
	public SharedGameObject targetObject;

	[Header("Property")]
	public Vector3 targetPos; // 플레이어 평면위치
	public SharedFloat jumpHeight;
	public SharedFloat elapse;

    public override void OnAwake()
    {
		targetObject = GameObject.FindGameObjectWithTag("Player");
    }

    public override void OnStart()
	{
		if (targetObject == null)
			return;

		elapse = 0;
        jumpHeight = 0;

        targetPos = new Vector3(targetObject.Value.transform.position.x,
                                     targetObject.Value.transform.position.y + jumpHeight.Value,
                                     targetObject.Value.transform.position.z);

        // 점프 애니메이션 트리거 추가 가능
        Debug.Log("점프 시작!");
    }

	public override TaskStatus OnUpdate()
	{
        // jumpHeight = transform.position.y

        return TaskStatus.Success;
	}
}