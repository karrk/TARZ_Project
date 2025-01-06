using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class LogoPanel : MonoBehaviour
{

    [Inject]
    MainSceneUI mainSceneUI;


    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }




    public void AddEvent()
    {
        this.UpdateAsObservable()
          .Where(x => Input.anyKeyDown)
          .Subscribe(x => { mainSceneUI.MainMenuPanel.gameObject.SetActive(true); gameObject.SetActive(false); });
    }

}
