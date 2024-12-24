using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

public class StatusInformationPanel : MonoBehaviour
{
    [Inject]
    ItemInventory inventory;

    [SerializeField] TMP_Text characterInformation;
    [SerializeField] TMP_Text itemStatusInformation;

    StringBuilder stringBuilder = new StringBuilder();

    private void Start()
    {
        UpdateStatusInfo();
    }

    public void UpdateStatusInfo()
    {
        SetCharacterInfoText();
        SetItemStatusInfoText();
    }

    /// <summary>
    ///  캐릭터 스텟 정보 입력
    /// </summary>
    public void SetCharacterInfoText()
    {
       


    }

    /// <summary>
    ///  누적된 장비 스텟 정보 입력
    /// </summary>
    public void SetItemStatusInfoText()
    {
        stringBuilder.Clear();

        float[] values = new float[(int)StatType.Size];

        foreach (var item in inventory.Equipments)
        {
            if (item  is null)
            {
                continue;
            }

            foreach (var stat in item.stats)
            {
                values[(int)stat.statType] += stat.statValue;
            }

        }

    

        string[] statType = Enum.GetNames(typeof(StatType));

        for (int i = 0; i < values.Length; i++)
        {
            stringBuilder.Append(statType[i]);
            stringBuilder.Append("  ");
            stringBuilder.Append(values[i]);
            stringBuilder.Append("\n");
        }

        itemStatusInformation.text = stringBuilder.ToString();
    }
}
