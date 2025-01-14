using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PassiveSlotGroup : MonoBehaviour
{
    public PassiveSlot[] slots;
    public GameObject[] lockImage;

    [Inject]
    LobbyData lobbyData;

    private void Awake()
    {
        slots = GetComponentsInChildren<PassiveSlot>(true);
        
        foreach (PassiveSlot slot in slots)
        {
            slot.group = this;
        }

        lockImage[0].SetActive(false);
    }



    public bool CheckLock(PassiveInfo info)
    {

        int num=0;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].info.id == info.id)
            {
                num = i;
                break;
            }
           
        }

      

        return !lockImage[num].activeSelf;

    }

    public void SetLock()
    {
        lockImage[0].SetActive(false);

        if (lobbyData.passiveEnable[slots[0].info.id - 1])
        {
            lockImage[1].SetActive(false);
        }

        if (lobbyData.passiveEnable[slots[1].info.id - 1])
        {
            lockImage[2].SetActive(false);
        }

       

    }
}
