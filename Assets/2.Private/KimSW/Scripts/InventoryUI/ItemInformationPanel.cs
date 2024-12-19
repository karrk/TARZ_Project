using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemInformationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text equippedItemName;
    [SerializeField] TMP_Text equippedItemInfo;

    [SerializeField] TMP_Text selectItemName;
    [SerializeField] TMP_Text selectItemInfo;

   
    public void SetEquippedItemInformation(Item item)
    {
        equippedItemName.text = item.itemName;
        equippedItemInfo.text = item.information;
    }

    public void SetSelectItemInformation(Item item)
    {
        selectItemName.text = item.itemName;
        selectItemInfo.text = item.information;
    }

    public void SetDefaultItemInformation()
    {
        selectItemName.text = "-";
        selectItemInfo.text = "";
    }
    public void SetDefaultEquippedInformation()
    {
        equippedItemName.text = "-";
        equippedItemInfo.text = "";
    }
}
