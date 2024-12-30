using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour, ISelectHandler
{
    public GameObject set;
    public UnityAction<GameObject> selectEvent;
    public void OnSelect(BaseEventData eventData)
    {
        selectEvent?.Invoke(set);
    }

}
