using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
public class ChatUI : MonoBehaviour
{
   [SerializeField] TMP_Text tmpText;

    [TextArea]
    [SerializeField] string chatText;

    CancellationTokenSource cancell = new CancellationTokenSource();

     void Start()
    {
        TextTask().Forget();

        Invoke("InvokeTest", 1.0f);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cancell.Cancel();
            tmpText.text = chatText;
        }
    

    }

    private void OnDestroy()
    {
        cancell.Cancel();
        cancell.Dispose();
    }

    async UniTaskVoid TextTask()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < chatText.Length; i++)
        {
           
            sb.Append(chatText[i]);
            tmpText.text = sb.ToString();
 
            if (Char.IsWhiteSpace(chatText[i]))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancell.Token);
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancell.Token);
            }
            
        }
  

    }

    public void InvokeTest()
    {
        Debug.Log("hello");
    }
}
