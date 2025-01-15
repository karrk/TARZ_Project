using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EquipmentSelectButton : MonoBehaviour
{
    [Inject]
    InGameUI inGameUI;

    [Inject]
    PlayerUIModel playerUIModel;

    [SerializeField] Image buttonImage;
    
    [SerializeField] Image sprite;
    [SerializeField] TMP_Text descriptionText;

    [SerializeField] TMP_Text levelText;
    [SerializeField] UpgradeLayout upgradeLayout;

    [SerializeField] Color[] rarityColor;

    NewEquipment equipment;

    StringBuilder sb = new StringBuilder();

    public void SetInfo(NewEquipment newEquipment)
    {
        equipment = newEquipment;
        sprite.sprite = equipment.illust;

        sb.Clear();
        sb.Append(equipment.equipmentName);
        sb.Append("\n\n");
        sb.Append($"{equipment.optionType}\n");

        string value = "0";

        foreach ( var item in inGameUI.EquipmentManager.equipped)
        {
            if (item.equipmentType == equipment.equipmentType)
            {
                value = item.optionValue.ToString();
            }
        }
        sb.Append($"{value} > {equipment.optionValue}");

        descriptionText.text = sb.ToString();

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
        descriptionText.text = "경험치";
        levelText.text = "";
        upgradeLayout.RemoveFill();
        upgradeLayout.SetLayout(0);

        buttonImage.color = rarityColor[0];
    }

    public void SelectButton()
    {
        Time.timeScale = 1.0f;

        if (equipment)
        {
            inGameUI.EquipmentManager.AddEquipped(equipment);
        }
        else
        {
            playerUIModel.TargetEXP.Value += 500;
        }
        inGameUI.EquipmentSelectPanel.CloseUIPanel();
    }


}
