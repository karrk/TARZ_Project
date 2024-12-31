using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KeyImage : MonoBehaviour
{
    [SerializeField] GameObject keyImages;
  
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

          

            // 컨트롤러
            if (controllers[0].Length > 0)
            {
                keyImages.SetActive(false);

            }
            // 키마
            else if (controllers[0].Length == 0)
            {
                keyImages.SetActive(true);
            }



            await UniTask.Delay(TimeSpan.FromSeconds(checkTime), cancellationToken: cancell.Token);

        }
    }
}
