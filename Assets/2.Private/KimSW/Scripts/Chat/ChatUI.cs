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
    /*
    [SerializeField] TMP_Text tmpText;
    
    [TextArea]
    [SerializeField] string chatText;
    */
    public float duration;
    public CancellationTokenSource cancell = new CancellationTokenSource();
    /*
     void Start()
    {
        TextTask().Forget();

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cancell.Cancel();
            tmpText.text = chatText;
        }
    

    }
    */
    private void OnDestroy()
    {
        CancelTask();
    }

    public void StartChatTask(TMP_Text targetText, string str)
    {
        CancelTask();
        cancell = new CancellationTokenSource();
        TextTask(targetText, str).Forget();
    }

    public void CancelTask()
    {
        cancell.Cancel();
        cancell.Dispose();
    }

    async UniTaskVoid TextTask(TMP_Text targetText, string str)
    {
        StringBuilder sb = new StringBuilder();
       
        for (int i = 0; i < str.Length; i++)
        {
            sb.Append(str[i]);
            targetText.text = sb.ToString();

            await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: cancell.Token);
            /*
            if (Char.IsWhiteSpace(str[i]))
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: cancell.Token);
            }
            else
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancell.Token);
            }
            */
        }

    }

}
