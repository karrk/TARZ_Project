using UnityEngine;
using Zenject;

// 스테이지 진입시 각 데이터를 전부 복사해온다.
// 데이터가 저장되는 시점은 해당 스테이지가 클리어되는 시점으로 생각중

// 스테이지 진행중에 저장이 된다면 여러 문제(꼼수,악용)가 있지 않을까 합니다.
// 클리어 시점에서 현재 데이터를 업데이트 시키는 방식??
// 뭘 작업하기가 아직 난해해 현재는 원본을 전달합니다.

// 값형식이 아닌것 같은 장비, 스킬, 특수강화의 경우
// csv 데이터 테이블의 ID값(int) 형태를 통해 각 요소를 비교하는게 어떤가 합니다.

public class DataSlots : IInitializable
{
    private PlayerData[] slots = new PlayerData[4];
    // UI 슬롯 번호와 맞게 1부터 시작하려 합니다.

    public int SelectedSlotNumber { get; private set; }

    public void SetSlotNumber(int number) { this.SelectedSlotNumber = number; }

    /// <summary>
    /// 현재 슬롯 데이터를 가져옵니다.
    /// </summary>
    public PlayerData GetCurrentData()
    {
        if (SelectedSlotNumber == 0)
        {
            Debug.LogError("현재 선택된 슬롯 번호가 없습니다.");
            return null;
        }

        return slots[SelectedSlotNumber];
    }

    public void Initialize()
    {
        // 프로그램 재시작시 유지되는 데이터 로드는 아직 미구현, 구현할지 미지정
        // 현재는 프로그램 구동시 각 슬롯데이터 초기화

        InitSlots();
    }

    private void InitSlots()
    {
        for (int i = 1; i < slots.Length; i++)
        {
            slots[i] = new PlayerData();
        }
    }

    // 기능 구현 미정
    private void LoadLastGameDatas()
    {

    }
}