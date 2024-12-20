using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneUI : BindUI
{
    public  MainMenuPanel MainMenuPanel {  get; private set; }
    public LoadGamePanel LoadGamePanel { get; private set; }
    public NewGamePanel NewGamePanel { get; private set; }

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

        CurrentMenu = MainMenuPanel;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentMenu is not null)
            CurrentMenu.CloseUIPanel();
        }
    }

    private void OnApplicationQuit()
    {
        DOTween.KillAll();
    }

}
