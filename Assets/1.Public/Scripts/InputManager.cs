using System;
using UnityEngine;
using Zenject;

public class InputManager : ITickable
{
    /// <summary>
    /// 이동방식의 키 입력시 동작하는 이벤트
    /// </summary>
    public event Action<Vector3> InputedMove;
    /// <summary>
    /// 스페이스, 점프에 대응하는 이벤트
    /// </summary>
    public event Action InputedJump;
    /// <summary>
    /// 회전 입력시 동작하는 이벤트
    /// </summary>
    public event Action<Vector3> InputedRot;

    private Vector3 moveVec;
    private Vector3 rotVec;

    public void Tick()
    {
        InputMove();
        InputJump();
        InputRot();
    }

    private void InputMove()
    {
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.z = Input.GetAxisRaw("Vertical");

        if (moveVec == Vector3.zero)
            return;

        InputedMove?.Invoke(moveVec);
    }

    private void InputJump()
    {
        if (Input.GetButtonDown("Jump"))
            InputedJump?.Invoke();
    }

    private void InputRot()
    {
        rotVec.x = Input.GetAxisRaw("Mouse X");
        rotVec.y = Input.GetAxisRaw("Mouse Y");

        if (rotVec == Vector3.zero)
            return;

        InputedRot?.Invoke(rotVec);
    }
}
