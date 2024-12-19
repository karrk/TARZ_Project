using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class FadePanel : MonoBehaviour, IFade
{
    [SerializeField] protected Button selectButton;

    [SerializeField] protected float fadeOutDuration;
    [SerializeField] protected float fadeInDuration;
 
    protected Image[] images;
    protected TMP_Text[] texts;

    void Awake()
    {
        SetComponent();
    }

    public virtual void SetComponent()
    {
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<TMP_Text>();
    }

    public virtual void FadeOutUI()
    {
        EventSystem.current.SetSelectedGameObject(null);
        foreach (Image image in images)
        {
            image.DOKill();
            image.DOFade(0, fadeOutDuration).OnComplete(() => { gameObject.SetActive(false); });

        }

        foreach (TMP_Text text in texts)
        {
            text.DOKill();
            text.DOFade(0, fadeOutDuration);
        }

    }

    public virtual void FadeInUI()
    {
        foreach (Image image in images)
        {
            image.DOKill();
            image.DOFade(1, fadeInDuration).OnComplete(() => {EventSystem.current.SetSelectedGameObject(selectButton.gameObject); });

        }

        foreach (TMP_Text text in texts)
        {
            text.DOKill();
            text.DOFade(1, fadeInDuration);
        }
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}
