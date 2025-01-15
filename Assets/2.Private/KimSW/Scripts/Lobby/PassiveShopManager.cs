using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PassiveShopManager : MonoBehaviour
{

    [Inject]
    LobbyData lobbyData;
    public PassiveSlotGroup[] slotGroup;
  
  

    private void Awake()
    {
        slotGroup = GetComponentsInChildren<PassiveSlotGroup>(true);
    }

    private void Start()
    {
        SetSlotInfo();

    }

    void SetSlotInfo()
    {
        int num = 0;
        for (int i = 0; i < slotGroup.Length; i++)
        {
            for (int j = 0; j < slotGroup[i].slots.Length; j++)
            {
                slotGroup[i].slots[j].SetInfo(lobbyData.passives[num]);
               
                num++;
            }
        }

        foreach (PassiveSlotGroup group in slotGroup)
        {
            group.SetLock();
        }

    }
}
