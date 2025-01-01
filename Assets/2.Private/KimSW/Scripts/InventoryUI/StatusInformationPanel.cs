using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using Zenject;
using static ProjectInstaller;

public class StatusInformationPanel : MonoBehaviour
{
    [Inject]
    ItemInventory inventory;

    [Inject]
    PlayerBaseStats status;

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
        stringBuilder.Clear();
        stringBuilder.Append("체력");
        stringBuilder.Append("  ");
        stringBuilder.Append(status.MaxHealth);
        stringBuilder.Append("\n");

        stringBuilder.Append("공격력");
        stringBuilder.Append("  ");
        stringBuilder.Append(status.AttackPower);
        stringBuilder.Append("\n");

        characterInformation.text = stringBuilder.ToString();

    }

    /// <summary>
    ///  누적된 장비 스텟 정보 입력
    /// </summary>
    public void SetItemStatusInfoText()
    {
        stringBuilder.Clear();

        float[] values = new float[(int)E_StatType.Size];

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

    

        string[] statType = Enum.GetNames(typeof(E_StatType));

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
