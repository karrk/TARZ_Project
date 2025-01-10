using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class ChangedColerBody : BaseAction
{
    private Renderer renderer;
    private Color originalColor;

    public override void OnStart()
    {
        base.OnStart();


        if (mob != null)
        {
            renderer = mob.GetComponentInChildren<Renderer>();
            if (renderer != null)
            {
                originalColor = renderer.material.color;
            }
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (renderer == null) return TaskStatus.Failure;

        // 현재 색상에서 빨간색으로 점진적으로 변경
        Color targetColor = Color.red;
        renderer.material.color = Color.Lerp(renderer.material.color, targetColor, mob.Stat.changeSpeed * Time.deltaTime);

        // 목표 색상에 도달했으면 성공 반환
        if (renderer.material.color == targetColor)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }
}