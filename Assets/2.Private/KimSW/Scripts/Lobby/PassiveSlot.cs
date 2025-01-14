using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSlot : MonoBehaviour
{
    public ButtonSelectCallback button;
    public GameObject lockImage;
    private void Awake()
    {
        button = GetComponentInChildren<ButtonSelectCallback>(true);
    }
}
