using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EquipmentSelectButton : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [SerializeField] Image buttonImage;

    [SerializeField] Image sprite;
    [SerializeField] TMP_Text descriptionText;

    [SerializeField] TMP_Text levelText;
    [SerializeField] UpgradeLayout upgradeLayout;

    [SerializeField] Color[] rarityColor;

    NewEquipment equipment;

    public void SetInfo(NewEquipment newEquipment)
    {
        equipment = newEquipment;
        sprite.sprite = equipment.illust;
        descriptionText.text = equipment.equipmentName;

        upgradeLayout.RemoveFill();
        upgradeLayout.SetLayout(newEquipment.upgradeLevel);

        levelText.text = "0";

        for (int i = 0; i < inGameUI.EquipmentManager.equipped.Count; i++)
        {
            if(newEquipment.id == inGameUI.EquipmentManager.equipped[i].id)
            {
                upgradeLayout.ChangeFill(inGameUI.EquipmentManager.equippedLevel[i]);
                levelText.text = inGameUI.EquipmentManager.equippedLevel[i].ToString();
            }
        }

        buttonImage.color = rarityColor[(int)equipment.rarityTier];
    }

    public void SetGold()
    {
        equipment = null;
        sprite.sprite = null;
        descriptionText.text = "골드";
        levelText.text = "";
        upgradeLayout.RemoveFill();
        upgradeLayout.SetLayout(0);

        buttonImage.color = rarityColor[0];
    }

    public void SelectButton()
    {
        if (equipment)
        {
            inGameUI.EquipmentManager.AddEquipped(equipment);
        }
        else
        {
            Debug.Log("골드 획득");
        }
        inGameUI.EquipmentSelectPanel.CloseUIPanel();
    }


}
