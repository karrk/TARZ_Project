using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PassiveSelectCallback : MonoBehaviour, ISelectHandler
{
    public UnityAction<int> selectEvent;
    public int num;

    public Image lockImage;

    public int cost;

    public void OnSelect(BaseEventData eventData)
    {
        selectEvent?.Invoke(num);
    }
}
