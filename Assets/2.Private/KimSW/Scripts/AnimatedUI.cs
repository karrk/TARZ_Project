using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedUI : MonoBehaviour
{
    protected Quaternion rotationOffset;
    protected RectTransform rectTransform;

    protected Vector2 positionOffset;
    [SerializeField] protected Vector2 targetOffset;

    [SerializeField] float targetScale;
    private void Awake()
    {
        SetMoveOffset();
    }

    public void SetMoveOffset()
    {
        rectTransform = GetComponent<RectTransform>();
        positionOffset = rectTransform.anchoredPosition;
        targetOffset = targetOffset + rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = targetOffset;
    }

    public void MoveOnUI()
    {
        
      
        gameObject.SetActive(true);
        rectTransform.DOAnchorPos(positionOffset, 0.1f).
            SetEase(Ease.OutBack).
            SetUpdate(true).
            OnKill(() =>
            {
                rectTransform.anchoredPosition = positionOffset;
            });
            
    }

    public void MoveOffUI()
    {
        rectTransform.DOAnchorPos(targetOffset, 0.1f).
          SetEase(Ease.InBack).
          SetUpdate(true).
          OnKill(() =>
          {
              gameObject.SetActive(false);
              rectTransform.anchoredPosition = targetOffset;
          });
          
    }
    
    public void ShrunkAnimation(Transform objectTransform)
    {
        DOTween.Sequence().
            Append(objectTransform.DOScale(targetScale, 0.1f)).
            Append(objectTransform.DOScale(1, 0.1f)).
            OnKill(() =>
            {
                objectTransform.localScale = new Vector3(1, 1, 1);

            });

    }


    public void ThrowSlotUI(UISlot currentSlot, UISlot targetSlot, AnimatedUI targetPanel)
    {
        Transform currentSlotTransform = currentSlot.GetImageTransform();
        Vector3 targetSlotVector = targetSlot.GetImageTransform().position;
        Sprite sprite = currentSlot.GetSprite();

        currentSlotTransform.DOMove(targetSlotVector, 0.1f).
            SetEase(Ease.Linear).
            OnKill(() => {
                targetSlot.SetSlotImage(sprite);
                targetSlot.OnVFX();
                ShrunkAnimation(targetSlot.transform);

                currentSlot.RemoveSlotImage();
                currentSlotTransform.localPosition = Vector3.zero;
                
                targetPanel.ShakeObject();
                 });
    }

    public void ShakeObject()
    {
        transform.DOShakeRotation(0.3f,1 ).
            SetEase(Ease.Linear).
            OnKill(()=> { transform.localRotation = rotationOffset; });
    }
}
