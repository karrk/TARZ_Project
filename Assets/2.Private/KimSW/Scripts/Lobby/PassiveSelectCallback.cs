using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PassiveSelectCallback : MonoBehaviour, ISelectHandler
{
    public UnityAction<int> selectEvent;
    public int num;

    public void OnSelect(BaseEventData eventData)
    {
        selectEvent?.Invoke(num);
    }
}
