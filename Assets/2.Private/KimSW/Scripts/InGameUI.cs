using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InGameUI : BindUI
{
    private GameObject inventoryUI;
    private InventoryPanel inventoryPanel;
    private ItemInformationPanel itemInformationPanel;
    private PlayerStatusBarPanel playerStatusBarPanel;
    private InGameMenuPanel inGameMenuPanel;
    public GameObject InventoryUI { get { return inventoryUI; } }
    public InventoryPanel InventoryPanel { get { return inventoryPanel; } }

    public ItemInformationPanel ItemInformationPanel { get { return itemInformationPanel; } }

    public PlayerStatusBarPanel PlayerStatusBarPanel { get { return playerStatusBarPanel; } }

    public InGameMenuPanel InGameMenuPanel { get { return inGameMenuPanel; } }

    private void Awake()
    {
        Bind();
        inventoryUI = GetUI("InGame_InventoryPanel");
        inventoryPanel = GetUI<InventoryPanel>("InventoryPanel");
        itemInformationPanel = GetUI<ItemInformationPanel>("ItemInformationPanel");
        playerStatusBarPanel = GetUI<PlayerStatusBarPanel>("PlayerStatusBarPanel");
        inGameMenuPanel = GetUI<InGameMenuPanel>("InGameMenuPanel");

    }
    private void Start()
    {
        InventoryUI.SetActive(false);
    }


    public void InputEsc()
    {
        if (InventoryUI.activeSelf)
        {

            OffInventory();
        }
        else if(InGameMenuPanel.gameObject.activeSelf)
        {
            OffInGameMenu();
        }
        else
        {
            OnInGameMenu();
        }

    }


    public void OnInventory()
    {
        if (InGameMenuPanel.gameObject.activeSelf)
            return;

        InventoryUI.SetActive(true);
        InventoryPanel.OnInventoryUI();
        PlayerStatusBarPanel.FadeOutUI();

    }

    public void OffInventory()
    {
        InventoryUI.SetActive(false);
        PlayerStatusBarPanel.FadeInUI();
    }

    public void OnInGameMenu()
    {
        InGameMenuPanel.gameObject.SetActive(true);
        PlayerStatusBarPanel.FadeOutUI();
        InGameMenuPanel.FadeInUI();
    }
    public void OffInGameMenu()
    {
       // InGameMenuPanel.gameObject.SetActive(false);
        PlayerStatusBarPanel.FadeInUI();
        InGameMenuPanel.FadeOutUI();
    }
}
