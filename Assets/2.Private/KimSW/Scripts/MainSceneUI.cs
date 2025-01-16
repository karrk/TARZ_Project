using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class MainSceneUI : BindUI
{
    [Inject]
    StaticEquipment staticEquipment;

    public InputActionReference cancelRef;

    public LoadingScript loadingScript;
    public  MainMenuPanel MainMenuPanel {  get; private set; }
    public LoadGamePanel LoadGamePanel { get; private set; }
    public NewGamePanel NewGamePanel { get; private set; }

    public MainOptionPanel MainOptionPanel { get; private set; }
   

    private IOpenCloseMenu currentMenu;

    public IOpenCloseMenu CurrentMenu { get { return currentMenu; } set { currentMenu = value; } }

    private void Awake()
    {
        Bind();
        MainMenuPanel = GetUI<MainMenuPanel>("MainMenuPanel");
        MainMenuPanel.SetComponent();

        LoadGamePanel = GetUI<LoadGamePanel>("LoadGamePanel");
        LoadGamePanel.SetComponent();

        NewGamePanel = GetUI<NewGamePanel>("NewGamePanel");
        NewGamePanel.SetComponent();

        MainOptionPanel = GetUI<MainOptionPanel>("MainOptionPanel");

        CurrentMenu = MainMenuPanel;

        staticEquipment.firstInit = false;
    }


    private void OnApplicationQuit()
    {
        DOTween.KillAll();
    }
    public void CancelInput(InputAction.CallbackContext value)
    {
        if (currentMenu is not null)
            CurrentMenu.CloseUIPanel();
    }

    public void ChangeScene()
    {
        loadingScript.Loading();
    }
    private void OnEnable()
    {
        cancelRef.action.performed += CancelInput;
    }

    private void OnDisable()
    {
        cancelRef.action.performed -= CancelInput;
    }


}
