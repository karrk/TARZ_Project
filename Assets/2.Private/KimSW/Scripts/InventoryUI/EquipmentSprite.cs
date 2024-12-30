using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentSprite", menuName = "EquipmentSprite", order = int.MinValue)]
public class EquipmentSprite : ScriptableObject
{
   public EquipmentSpriteType[] spriteType;
}



[Serializable]
public class EquipmentSpriteType
{
    public E_EquipmentsType itemType;
    public Sprite[] sprite;

}