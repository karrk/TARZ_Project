using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PassiveSlot : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [Inject]
    LobbyData lobbyData;

    public ButtonSelectCallback button;

    public PassiveInfo info;

   
    public PassiveSlotGroup group;

    [SerializeField] bool isSelect;
    [SerializeField] Image passiveImage;

    private void Awake()
    {
        button = GetComponentInChildren<ButtonSelectCallback>(true);
        if (isSelect)
        {
            SetInfo(lobbyData.passives[0]);
        }
    }

    public void SetInfo(PassiveInfo info)
    {
        this.info = info;
        passiveImage.sprite = info.illust;

        if (lobbyData.passiveEnable[info.id - 1])
        {
            passiveImage.color = Color.white;
        }
    }

    public void OnClickButton()
    {
        if (lobbyData.passiveEnable[info.id - 1])
        {
            inGameUI.PassiveShopPanel.SetSelectButton();
            inGameUI.PassiveShopPanel.ActiveCost(false);
            inGameUI.PassiveShopPanel.currentPassive = info;
            return;
        }
        inGameUI.PassiveShopPanel.ActiveCost(true);
        if (!group.CheckLock(info))
        {
            return;
        }



       if(info.cost <= lobbyData.exp)
        {
            lobbyData.exp -= info.cost;
            lobbyData.SetExp();

            lobbyData.passiveEnable[info.id-1] = true;

            group.SetLock();
            inGameUI.PassiveShopPanel.ActiveCost(false);

            lobbyData.SaveData();

            if (lobbyData.passiveEnable[info.id - 1])
            {
                passiveImage.color = Color.white;
            }
        }
    }

    public void OnSelectButton()
    {
      

        inGameUI.PassiveShopPanel.SetInfoText(info);
        if (lobbyData.passiveEnable[info.id - 1])
        {
            inGameUI.PassiveShopPanel.ActiveCost(false);
            
            return;
        }
        inGameUI.PassiveShopPanel.ActiveCost(true);
       
    }

    private void OnEnable()
    {
        button.selectEvent += OnSelectButton;
    }
    private void OnDisable()
    {
        button.selectEvent -= OnSelectButton;
    }
}
