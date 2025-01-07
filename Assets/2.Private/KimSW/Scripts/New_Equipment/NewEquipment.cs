using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RarityLevel { }
enum OptionType { }


public class NewEquipment : MonoBehaviour
{
    int id;
    string equipmentName;
    string description;
    RarityLevel rarityLevel;
    int upgradeLevel;
    OptionType optionType;
    float optionValue;
    Sprite illust;
}
