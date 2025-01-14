using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveShopManager : MonoBehaviour
{
    public PassiveSlotGroup[] slotGroup;
    [SerializeField] PassiveCSVParser csvParser;
    private void Awake()
    {
        slotGroup = GetComponentsInChildren<PassiveSlotGroup>(true);
    }

    private void Start()
    {
        foreach(var p in csvParser.passives)
        {
            Debug.Log(p.id);
        }
    }
}
