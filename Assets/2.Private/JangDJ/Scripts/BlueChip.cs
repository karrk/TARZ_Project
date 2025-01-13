using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlueChipType { Melee,Exp,RangeAttack,AbsorbHp,BGM }

[CreateAssetMenu(fileName = "BlueChip", menuName = "BlueChip", order = int.MinValue)]
public class BlueChip : ScriptableObject
{
    public string blueChipName;
    public string description;
    public BlueChipType type;

    public float dropRate;
    public Sprite illust;
}
