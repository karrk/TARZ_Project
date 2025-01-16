using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class TestInput : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [Inject]
    ItemInventory inventory;

    [Inject]
    PlayerUIModel playerModel;

    [Inject]
    PoolManager poolManager;

 

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.GetItem();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inGameUI.OnInventory();
        }
        
        if (Input.GetButtonDown("Cancel"))
        {
            inGameUI.InputCancel();
        }
       
        //playerModel.Stamina.Value += Time.deltaTime * 100;

        if (Input.GetKeyDown(KeyCode.T))
        {
            poolManager.GetObject<DamageText>(E_VFX.DamageText).SetText("200", transform.position, false);

        }
         

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (!inGameUI.CurrentMenu.Equals(inGameUI.StatusBarPanel))
            {
                return;
            }
                inGameUI.CurrentMenu = inGameUI.EquipmentBackpackPanel;
            inGameUI.CurrentMenu.OpenUIPanel();

        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inGameUI.CurrentMenu.Equals(inGameUI.EquipmentBackpackPanel))
            {
                return;
            }
            inGameUI.EquipmentBackpackPanel.ChangeSelect();

        }
        */
        if (Input.GetKeyDown(KeyCode.U))
        {
            inGameUI.AlertText.SetAlertText("스태미나가 부족합니다");

        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inGameUI.AlertText.SetAlertText("게이지가 부족합니다");

        }
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            inGameUI.CurrentMenu = inGameUI.PassiveShopPanel;
            inGameUI.CurrentMenu.OpenUIPanel();

        }
        */

        /*

        
        if (Input.GetMouseButtonDown(0))
        {

            playerModel.Hp.Value -= 10;
            playerModel.Stamina.Value -= 200;
            playerModel.SkillGauge.Value -= 30;
            playerModel.GarbageCount.Value --;
            playerModel.TargetEXP.Value -= 500;

        }

        if (Input.GetMouseButtonDown(1))
        {

            playerModel.Hp.Value += 10;
            playerModel.Stamina.Value += 200;
            playerModel.SkillGauge.Value += 30;
            playerModel.GarbageCount.Value++;
            playerModel.TargetEXP.Value += 500;

        }
        */

    }
}
