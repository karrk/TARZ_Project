using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] string sceneName;

    [SerializeField] GameObject loadingImage;

    [Inject] private SignalBus signal;


    public void Loading()
    {

        StartCoroutine(StartGame());

    }
    public void Loading(string str)
    {
        sceneName = str;
        StartCoroutine(StartGame());

    }

    IEnumerator StartGame()
    {
        signal.Fire<StageEndSignal>();

        loadingImage.SetActive(true);
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        while (!loading.isDone)
        {
         
            yield return null;
        }

        loading.allowSceneActivation = true;
    }

  
}
