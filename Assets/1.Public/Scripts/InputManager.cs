using System;
using UnityEngine;
using Zenject;

/// <summary>
/// 유저의 입력을 받아들여 필요한 클래스에서 이벤트를 활용해 기능을 연동하기 위한 클래스
/// </summary>
public class InputManager : IInitializable, ITickable
{
    [Inject] private SignalBus signal;

    /// <summary>
    /// 좌측 스틱 컨트롤러 조작시 발생되는 이벤트입니다.
    /// 데스크탑 환경에서는 WASD 키 입력시 발생됩니다.
    /// </summary>
    public event Action<Vector3> OnControlledLeftStick;

    
    /// <summary>
    /// 우측 스틱 컨트롤러 조작시 발생되는 이벤트입니다.
    /// 데스크탑 환경에서는 마우스 이동 시 발생됩니다.
    /// </summary>
    public event Action<Vector3> OnControlledRightStick;

    /// <summary>
    /// DPAD 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 4방향키 입력시 발생됩니다.
    /// </summary>
    public event Action<Vector3> OnControlledDPAD;



    /// <summary>
    /// X 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 Space 키 입력시 발생됩니다.
    /// </summary>
    public event Action PressedXKey;

    /// <summary>
    /// Y 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 Tab 키 입력시 발생됩니다.
    /// </summary>
    public event Action PressedYKey;

    /// <summary>
    /// A 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 좌측 Ctrl 키 입력시 발생됩니다.
    /// </summary>
    public event Action PressedAKey;

    public event Action OnUpAkey;

    /// <summary>
    /// B 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 좌측 Shift 키 입력시 발생됩니다.
    /// </summary>
    public event Action PressedBKey;

    /// <summary>
    /// R2 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 마우스 좌클릭 입력시 발생됩니다.
    /// </summary>
    public event Action PressedR2Key;

    /// <summary>
    /// L1 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 R 키 입력시 발생됩니다.
    /// </summary>
    public event Action PressedL1Key;

    /// <summary>
    /// R1 키 입력시 발생됩니다.
    /// 데스크탑 환경에서는 Q 키 입력시 발생됩니다.
    /// </summary>
    public event Action PressedR1Key;



    /// <summary>
    /// L2 입력이 감지되었을때 발생합니다.
    /// 데스크탑 환경에서는 마우스 우클릭시 발생됩니다.
    /// </summary>
    public event Action OnDownL2Key;

    /// <summary>
    /// L2 입력이 해제 되었을때 발생합니다.
    /// 데스크탑 환경에서는 마우스 우클릭을 해제한경우 발생됩니다.
    /// </summary>
    public event Action OnUpL2Key;

    private Vector3 moveVec;
    private Vector3 rotVec;
    private Vector3 arrowVec;
    private bool enteredL2;
    private bool pressingAkey;


    private void DisconnectActions()
    {
        OnControlledLeftStick = null;
        OnControlledRightStick = null;
        OnControlledDPAD = null;
        PressedXKey = null;
        PressedYKey = null;
        PressedAKey = null;
        PressedBKey = null;
        PressedR2Key = null;
        PressedL1Key = null;
        PressedR1Key = null;
        OnDownL2Key = null;
        OnUpL2Key = null;
    }

    public void Initialize()
    {
        signal.Subscribe<StageEndSignal>(DisconnectActions);
    }

    public void Tick()
    {
        InputAxis();
        InputButtons();
    }

    private void InputAxis()
    {
        InputLeftStick();
        InputRightStick();
        InputDPAD();
    }

    private void InputButtons()
    {
        InputX();
        InputY();
        InputA();
        InputB();
        InputR1();
        InputR2();
        InputL1();
        InputL2();
    }

    /// <summary>
    /// 좌측 스틱 동작감지, WASD
    /// </summary>
    private void InputLeftStick()
    {
        moveVec.x = Input.GetAxis("LeftStickX");
        moveVec.z = Input.GetAxis("LeftStickY");

        //if (moveVec == Vector3.zero)
        //    return;

        //Debug.Log(moveVec);

        OnControlledLeftStick?.Invoke(moveVec);
    }

    /// <summary>
    /// 우측 스틱 동작감지, 마우스
    /// </summary>
    private void InputRightStick()
    {
        rotVec.x = Input.GetAxis("RightStickX");
        rotVec.y = Input.GetAxis("RightStickY");

        if (rotVec == Vector3.zero)
            return;

        OnControlledRightStick?.Invoke(rotVec);
    }

    /// <summary>
    /// 방향키, DPAD
    /// </summary>
    private void InputDPAD()
    {
        arrowVec.x = Input.GetAxisRaw("DPAD_H");
        arrowVec.y = Input.GetAxisRaw("DPAD_V");

        if (arrowVec == Vector3.zero)
            return;

        OnControlledDPAD?.Invoke(arrowVec);
    }

    /// <summary>
    /// Space
    /// </summary>
    private void InputX()
    {
        if (Input.GetButtonDown("X"))
        {
            PressedXKey?.Invoke();
        }
    }

    /// <summary>
    /// Shift
    /// </summary>
    private void InputB()
    {
        if(Input.GetButtonDown("B") || Input.GetMouseButtonDown(1))
        {
            PressedBKey?.Invoke();
        }
    }

    /// <summary>
    /// 좌클릭, 우측 트리거
    /// </summary>
    private void InputR2()
    {
        // 좌클릭은 가능 , 조이스틱 R2 기능 안함

        if (Input.GetButtonDown("R2") && Input.GetAxisRaw("R2")==1)
        {
            PressedR2Key?.Invoke();
            Debug.Log("R2");
        }
    }

    /// <summary>
    /// left Ctrl
    /// </summary>
    private void InputA()
    {
        if ((Input.GetButtonDown("A") || Input.GetKeyDown(KeyCode.F)) && pressingAkey == false)
        {
            PressedAKey?.Invoke();
            pressingAkey = true;
        }
        else if((Input.GetButtonUp("A") || Input.GetKeyUp(KeyCode.F)) && pressingAkey == true)
        {
            pressingAkey = false;
            OnUpAkey?.Invoke();
        }
    }

    /// <summary>
    /// tab key
    /// </summary>
    private void InputY()
    {
        if (Input.GetButtonDown("Y"))
        {
            PressedYKey?.Invoke();
        }
    }

    /// <summary>
    /// R key
    /// </summary>
    private void InputL1()
    {
        if(Input.GetButtonDown("L1"))
        {
            PressedL1Key?.Invoke();
        }
    }

    /// <summary>
    /// Q key
    /// </summary>
    private void InputR1()
    {
        if (Input.GetButtonDown("R1"))
        {
            PressedR1Key?.Invoke();
        }
    }

    /// <summary>
    /// Right Click
    /// </summary>
    private void InputL2()
    {
        if ((Input.GetButtonDown("L2") || Input.GetAxisRaw("L2") == 1) && enteredL2 == false)
        {
            OnDownL2Key?.Invoke();
            enteredL2 = true;
        }
        else if((Input.GetButtonUp("L2") || Input.GetAxisRaw("L2") == 0) && enteredL2 == true)
        {
            OnUpL2Key?.Invoke();
            enteredL2 = false;
        }
    }

    
}
