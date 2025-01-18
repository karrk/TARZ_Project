using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class PassiveShopPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

    [Inject]
    LobbyData lobbyData;

    [SerializeField] GameObject selectButton;

    [SerializeField] GameObject costUI;

    [SerializeField] GameObject equipSlotbutton;

    [SerializeField] TMP_Text passiveNameText;
    [SerializeField] TMP_Text passiveInfoText;
    [SerializeField] TMP_Text passiveCostText;
  
    
    [SerializeField] Image[] passiveSprite;

    [SerializeField] PassiveSelectCallback[] passiveSelectCallbacks;
   

    public PassiveInfo currentPassive;

    StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        SetSlotLock();
    }

    public void SetSlotLock()
    {
        for (int i = 0; i < passiveSelectCallbacks.Length; i++)
        {
            if (lobbyData.equipSlotEnable[i])
            {
                passiveSelectCallbacks[i].lockImage.gameObject.SetActive(false);
            }
        }
    }


    public void SetInfoText(PassiveInfo info)
    {
        sb.Clear();
        passiveNameText.text = info.statName;

        sb.Append(info.statType);
        sb.Append(" ");
        sb.Append(info.statValue);
        sb.Append(" 상승");
        passiveInfoText.text = sb.ToString();
        passiveCostText.text = info.cost.ToString();
    }
    public void SetInfoTextNull()
    {
        passiveNameText.text ="";
        passiveInfoText.text ="";
        passiveCostText.text ="";
    }

    public void SetInfoSlotCost(int num)
    {
        passiveNameText.text = $"슬롯{num+1} 구매";
        passiveInfoText.text = "";
        passiveCostText.text = passiveSelectCallbacks[num].cost.ToString();
        ActiveCost(true);
    }
    public void OnSelectButton(int num)
    {
        if (!lobbyData.equipSlotEnable[num])
        {
            SetInfoSlotCost(num);
            return;
        }
            if (lobbyData.equipPassive[num] == null)
        {
            SetInfoTextNull();
           
        }
        else
        {

            SetInfoText(lobbyData.equipPassive[num]);
        }

    }

    private void OnEnable()
    {
        foreach(PassiveSelectCallback callback in passiveSelectCallbacks)
        {
            callback.selectEvent += OnSelectButton;
        }
    }
    private void OnDisable()
    {
        foreach (PassiveSelectCallback callback in passiveSelectCallbacks)
        {
            callback.selectEvent -= OnSelectButton;
        }
    }

    public void ActiveCost(bool active)
    {
        costUI.SetActive(active);
    }

    public void SetSelectButton()
    {
        EventSystem.current.SetSelectedGameObject(equipSlotbutton);
    }

    public void EquipPassive(int num)
    {
        if (!lobbyData.equipSlotEnable[num])
        {
                if (passiveSelectCallbacks[num].cost <= lobbyData.exp)
            {
                lobbyData.exp -= passiveSelectCallbacks[num].cost;
                lobbyData.SetExp();

                lobbyData.equipSlotEnable[num] = true;
                lobbyData.SaveData();
                SetSlotLock();

               
            }

            return;
        }


        if (currentPassive == null)
            return;

        foreach (var item in lobbyData.equipPassive)
        {
            if(item == null)
            {

            }
            else if (item.id == currentPassive.id)
            {
                return;
            }
        }

        lobbyData.equipPassive[num] = currentPassive;

        SetInfoText(currentPassive);
        SetSprite();

        lobbyData.SaveData();
    }

    public void SetSprite()
    {

        for (int i = 0; i < lobbyData.equipPassive.Length; i++ )
        {
            if (lobbyData.equipPassive[i] == null)
            {
                
            }
            else
            {
                passiveSprite[i].sprite = lobbyData.equipPassive[i].illust;
            }
        }

    }

    public void OpenUIPanel()
    {
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
        SetSprite();
        gameObject.SetActive(true);
        
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(selectButton);
    }

    public void CloseUIPanel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();

    }
}
