using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlotPanel
{
    public void SetSlot();

    public void SlotSelectCallback(int slotNumber);

    public void SetSprite(int num, Sprite sprite);

}
