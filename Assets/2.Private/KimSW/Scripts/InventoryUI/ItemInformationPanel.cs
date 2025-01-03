using System.Text;
using TMPro;
using UnityEngine;

public class ItemInformationPanel : MonoBehaviour
{
    [SerializeField] TMP_Text equippedItemName;
    [SerializeField] TMP_Text equippedItemInfo;
    [SerializeField] TMP_Text equippedItemGrade;

    [SerializeField] TMP_Text selectItemName;
    [SerializeField] TMP_Text selectItemInfo;
    [SerializeField] TMP_Text selectItemGrade;

    StringBuilder stringBuilder = new StringBuilder();

    ChatUI equippedTextChat = new ChatUI();
    ChatUI selectItemTextChat = new ChatUI();

    public void SetEquippedItemInformation(Equipment item)
    {
        equippedItemName.text = item.name;

        stringBuilder.Clear();
        foreach (var equippedItem in item.stats)
        {
            stringBuilder.Append(equippedItem.statType);
            stringBuilder.Append("  ");
            stringBuilder.Append(equippedItem.statValue);
            stringBuilder.Append("\n");
        }


        equippedTextChat.StartChatTask(equippedItemInfo, stringBuilder.ToString());

      //  equippedItemInfo.text = stringBuilder.ToString();

        equippedItemGrade.text = item.grade.ToString();
    }

    public void SetSelectItemInformation(Equipment item)
    {
        selectItemName.text = item.name;

        stringBuilder.Clear();
        foreach (var selectedItem in item.stats)
        {
            stringBuilder.Append(selectedItem.statType);
            stringBuilder.Append("  ");
            stringBuilder.Append(selectedItem.statValue);
            stringBuilder.Append("\n");
        }


        selectItemTextChat.StartChatTask(selectItemInfo, stringBuilder.ToString());

        //selectItemInfo.text = stringBuilder.ToString();

        selectItemGrade.text = item.grade.ToString();
    }

    public void SetDefaultItemInformation()
    {
        selectItemTextChat.StartChatTask(selectItemInfo, "");
        selectItemName.text = "-";
        selectItemInfo.text = "";
        selectItemGrade.text = "";
    }
    public void SetDefaultEquippedInformation()
    {
        equippedTextChat.StartChatTask(equippedItemInfo, "");
        equippedItemName.text = "-";
        equippedItemInfo.text = "";
        equippedItemGrade.text = "";
    }
}
