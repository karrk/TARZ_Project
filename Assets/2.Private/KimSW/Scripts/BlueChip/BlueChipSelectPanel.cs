using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class BlueChipSelectPanel : MonoBehaviour, IOpenCloseMenu
{
    [Inject]
    InGameUI inGameUI;

   
    [SerializeField] Button selectedButton;

    [SerializeField] Image beforeSprite;
    [SerializeField] TMP_Text beforeBluechipName;
    [SerializeField] TMP_Text beforeBluechipExplain;

    [SerializeField] Image afterSprite;
    [SerializeField] TMP_Text afterBluechipName;
    [SerializeField] TMP_Text afterBluechipExplain;

    public InteractBlueChip chip;

    public void OpenUIPanel()
    {
        inGameUI.EquipmentGetPanel.gameObject.SetActive(false);

        gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectedButton.gameObject);
        Time.timeScale = 0f;
    }

    public void CloseUIPanel()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        
        inGameUI.CurrentMenu = inGameUI.StatusBarPanel;
        inGameUI.CurrentMenu.OpenUIPanel();
        chip.RemoveInstance();


    }

    public void SetInfo(BlueChip beforeChip, BlueChip afterChip)
    {
        afterBluechipName.text = afterChip.blueChipName;
        afterBluechipExplain.text = afterChip.description;

        beforeBluechipName.text = beforeChip.blueChipName;
        beforeBluechipExplain.text = beforeChip.description;
    }


    public void ChangeButton()
    {
        chip.ChangeChip();
        CloseUIPanel();
    }

    public void CancelButton()
    {
        CloseUIPanel();
    }

}
