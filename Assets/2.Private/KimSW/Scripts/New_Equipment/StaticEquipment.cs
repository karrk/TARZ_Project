using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEquipment : MonoBehaviour
{
    public List<NewEquipment> newEquipments = new List<NewEquipment>();

    public List<NewEquipment> equipped = new List<NewEquipment>();
    public int[] equippedLevel = new int[5];

    public bool firstInit;

    public EquipmentManager manager;

    public event Action OnChangedEquip;

    public void InvokeOnChangedEq()
    {
        OnChangedEquip?.Invoke();
    }

    public void ResetData()
    {
        equipped = new List<NewEquipment>();
        equippedLevel = new int[5];
    }
}
