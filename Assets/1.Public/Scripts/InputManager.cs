using System;
using UnityEngine;
using Zenject;

public class InputManager : ITickable
{
    public event Action<Vector3> InputedMove;
    public event Action InputedJump;
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
