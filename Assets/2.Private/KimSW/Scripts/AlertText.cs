using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class AlertText : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] float delay;
    [SerializeField] float fadeDuration;

    public void FadeInOut()
    {

        DOTween.KillAll();

        DOTween.Sequence().
           Prepend(text.DOFade(1f, 0)).
           AppendInterval(delay).
           Append(text.DOFade(0, fadeDuration).SetDelay(delay)).
           OnComplete(() =>
           {
               gameObject.SetActive(false);

           });
    }

    public void SetAlertText(string str)
    {
        text.text = str;
        gameObject.SetActive(true);
        FadeInOut();
    }
}
