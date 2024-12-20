using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;


public class InventoryPanel : MonoBehaviour
{


    [SerializeField] public BackpackPanel backpackPanel;
    [SerializeField] public EquipmentPanel equipmentPanel;



    public void GetItem(int num, Sprite sprite)
    {
        backpackPanel.SetSprite(num, sprite);
    }

    public void OnInventoryUI()
    {
        backpackPanel.SetSelectCursor();
    }



}
