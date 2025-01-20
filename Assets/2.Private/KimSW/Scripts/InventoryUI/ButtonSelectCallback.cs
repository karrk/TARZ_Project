using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class ButtonSelectCallback : MonoBehaviour, ISelectHandler
{
    public UnityAction selectEvent;

    public void OnSelect(BaseEventData eventData)
    {
        selectEvent?.Invoke();
    }
}
