using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityTier { COMMON,RARE, UNIQUE, LEGENDARY, SIZE }
public enum OptionType { MOVESPD,ATK,ATKSPD,HP,INVENTORY,GAUGEINC,STAMINA,LUCK,SIZE }

public enum EquipmentType { BOOTS,ARM,EARING,RING,NECKLACE,LEG,CHEST,BACKPACK, SIZE }

[CreateAssetMenu(fileName = "NewEquipment", menuName = "NewEquipment", order = int.MinValue)]
public class NewEquipment : ScriptableObject
{
    public int id;
    public string equipmentName;
    public string description;

    public RarityTier rarityTier;

    public int upgradeLevel;

    public OptionType optionType;
    public EquipmentType equipmentType;

    public float optionValue;

    public Sprite illust;
}
