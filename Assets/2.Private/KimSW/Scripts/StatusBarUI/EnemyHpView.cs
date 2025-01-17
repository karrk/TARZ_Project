using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class EnemyHpView : SliderView
{
    //모델 프레젠터 분리 필요


    //model
    public ReactiveProperty<float> Hp;

    private IDisposable disposable;



    EnemyStatusPanel panel;

    private void Awake()
    {
        panel = GetComponentInParent<EnemyStatusPanel>();
    }

    public void TempModel(float hp)
    {
        //model
        slider.maxValue = hp;
        Hp.Value = hp;

    }

    public void TempPresenter()
    {
        // presenter
        disposable = Hp
        .Where(value => Hp.Value <= 0)
     .Subscribe(value => { panel.gameObject.SetActive(false); disposable.Dispose(); })
     .AddTo(this);

        disposable = Hp
       .Where(value => value > 0)
       .Subscribe(value => { SetSlider(value); })
       .AddTo(this);
    }

    public void SetEnemyHp(float hp)
    {
        TempModel(hp);
        TempPresenter();
    }

  

}
