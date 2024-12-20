using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ItemInfo",menuName ="ItemInfo",order =int.MinValue)]
public class ItemInfo : ScriptableObject
{
    public Sprite sprite;
    public E_EquipmentsType itemType;
    public string itemName;
    public string info;


}
