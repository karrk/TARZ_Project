using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Sprite sprite;
    public E_EquipmentsType itemType;
    public string itemName;
    public string information;

    public Item(ItemInfo itemInfo)
    {
        sprite = itemInfo.sprite;
        itemType = itemInfo.itemType;
        itemName = itemInfo.itemName;
        information = itemInfo.info;
    }

}
