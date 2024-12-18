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
    private Vector3 arrowVec;

    public void Tick()
    {
        InputMove();
        InputJump();
        InputRot();
        InputB();
        InputArrow();
    }

    /// <summary>
    /// 좌측 스틱 동작감지
    /// </summary>
    private void InputMove()
    {
        moveVec.x = Input.GetAxisRaw("Horizontal");
        moveVec.z = Input.GetAxisRaw("Vertical");

        if (moveVec == Vector3.zero)
            return;

        InputedMove?.Invoke(moveVec);

        Debug.Log("이동");
    }

    /// <summary>
    /// A 키
    /// </summary>
    private void InputJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            InputedJump?.Invoke();
            Debug.Log("점프");
        }
    }

    /// <summary>
    /// 우측 스틱 동작
    /// </summary>
    private void InputRot()
    {
        rotVec.x = Input.GetAxisRaw("Mouse X");
        rotVec.y = Input.GetAxisRaw("Mouse Y");

        if (rotVec == Vector3.zero)
            return;

        InputedRot?.Invoke(rotVec);

        Debug.Log("회전");
    }


    private void InputB()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Cancel");
        }
    }

    private void InputArrow()
    {
        arrowVec.x = Input.GetAxis("DPAD_H");
        arrowVec.y = Input.GetAxis("DPAD_V");

        if (arrowVec == Vector3.zero)
            return;

        Debug.Log(arrowVec);
    }
}
