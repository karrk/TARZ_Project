using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StatCollider : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inGameUI.CurrentMenu = inGameUI.PassiveShopPanel;
            inGameUI.CurrentMenu.OpenUIPanel();
        }
    }
  
}
