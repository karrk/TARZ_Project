using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSlotGroup : MonoBehaviour
{
    public PassiveSlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<PassiveSlot>(true);
    }
}
