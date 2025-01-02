using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyImage : MonoBehaviour
{
    [System.Serializable]
    public class KeySprite
    {
        public Sprite[] sprite;
    }

    [SerializeField] Image[] keyImages;
    [SerializeField] KeySprite[] keySprites;

 

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
                for (int i = 0; i < keyImages.Length; i++) {
                    keyImages[i].sprite = keySprites[0].sprite[i];
                }

            }
            // 컨트롤러
            else if (controllers[0].Length > 0)
            {
                for (int i = 0; i < keyImages.Length; i++)
                {
                    keyImages[i].sprite = keySprites[1].sprite[i];
                }

            }
            // 키마
            else if (controllers[0].Length == 0)
            {
                for (int i = 0; i < keyImages.Length; i++)
                {
                    keyImages[i].sprite = keySprites[0].sprite[i];
                }
            }



            await UniTask.Delay(TimeSpan.FromSeconds(checkTime), cancellationToken: cancell.Token);

        }
    }
}
