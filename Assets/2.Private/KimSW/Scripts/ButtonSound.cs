using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class ButtonSound : MonoBehaviour, ISelectHandler
{
    [Inject]
    SoundManager soundManager;

    public void OnSelect(BaseEventData eventData)
    {
        soundManager.PlaySFX(E_Audio.UI_ButtonMove);
    }

    public void OnClick()
    {
        soundManager.PlaySFX(E_Audio.UI_ButtonClick);
    }

    public void OnClickShop()
    {
        soundManager.PlaySFX(E_Audio.UI_ShopConfirm);
    }
}
