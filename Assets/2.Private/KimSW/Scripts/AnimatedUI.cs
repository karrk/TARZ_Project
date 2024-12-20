using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedUI : MonoBehaviour
{

    

    public void ThrowSlotUI(UISlot currentSlot, UISlot targetSlot, AnimatedUI targetPanel)
    {
        Transform currentSlotTransform = currentSlot.GetImageTransform();
        Vector3 targetSlotVector = targetSlot.GetImageTransform().position;
        Sprite sprite = currentSlot.GetSprite();

        currentSlotTransform.DOMove(targetSlotVector, 0.1f).
            SetEase(Ease.OutBack).
            OnKill(() => {
                targetSlot.SetSlotImage(sprite);
                currentSlot.RemoveSlotImage();
                currentSlotTransform.localPosition = Vector3.zero;
                targetPanel.ShakeObject();
                 });
    }

    public void ShakeObject()
    {
        transform.DOShakeRotation(0.3f,1 ).
            SetEase(Ease.Linear).
            OnKill(()=> { transform.localRotation = Quaternion.identity; });
    }
}
