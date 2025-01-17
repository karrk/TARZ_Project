using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class MenualPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

   
    [SerializeField] GameObject[] con;



    public float checkTime;
    public CancellationTokenSource cancell = new CancellationTokenSource();


    private void Start()
    {
        ControllerCheckTask().Forget();
    }

    void OnDestroy()
    {
        cancell.Cancel();
        cancell.Dispose();
    }




    async UniTaskVoid ControllerCheckTask()
    {
        while (true)
        {


            var controllers = Input.GetJoystickNames();

            if (controllers.Length <= 0)
            {
                OnOffConImage(0);

            }
            // 컨트롤러
            else if (controllers[0].Length > 0)
            {
                OnOffConImage(1);

            }
            // 키마
            else if (controllers[0].Length == 0)
            {
                OnOffConImage(0);
            }



            await UniTask.Delay(TimeSpan.FromSeconds(checkTime), cancellationToken: cancell.Token);

        }
    }

    void OnOffConImage(int num)
    {
        for (int i = 0; i < con.Length; i++)
        {
            if (i == num)
            {
                con[i].SetActive(true);
            }
            else
            {
                con[i].SetActive(false);
            }
        }
    }



    public void OpenUIPanel()
    {
        gameObject.SetActive(true);
    
    }

    public void CloseUIPanel()
    {
        gameObject.SetActive(false);
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

    }
}
