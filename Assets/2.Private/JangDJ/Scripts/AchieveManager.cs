using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public enum E_Achieve { None = -1, TurnOn, GetItem, FullInventory, UseSKill, DieToDie }

public class AchieveManager : MonoBehaviour
{
    [Inject] private LobbyData save;
    private TMP_Text tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TMP_Text>();
    }

    private Dictionary<E_Achieve, string> clearStr = new Dictionary<E_Achieve, string>()
    {
        { E_Achieve.TurnOn,"Play Game\n게임을 시작했습니다" },
        { E_Achieve.GetItem,"Get Garbage\n물건을 주웠습니다" },
        { E_Achieve.FullInventory,"Fulled Garbage\n인벤토리를 가득채웠습니다" },
        { E_Achieve.UseSKill,"Use Skill\n필살기를 사용했습니다" },
        { E_Achieve.DieToDie,"Die To Die\n좀비를 처지했습니다" }
    };

    private void Start()
    {
        UpdateAchieve(E_Achieve.TurnOn);
    }

    public void UpdateAchieve(E_Achieve type)
    {
        if (save.achieves[(int)type] == true)
            return;

        SetText(clearStr[type]);

        save.achieves[(int)type] = true;
        save.SaveData();
    }

    private void SetText(string m_text)
    {
        RectTransform rt = tmp.GetComponent<RectTransform>();
        tmp.text = m_text;

        Sequence sq = DOTween.Sequence()
            .Append(rt.DOPivotY(0, 0.5f))
            .AppendInterval(3f)
            .Append(rt.DOPivotY(1, 0.5f));
            
    }
}
