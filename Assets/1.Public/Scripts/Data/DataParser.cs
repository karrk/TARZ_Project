using System;
using Zenject;

public class DataParser
{
    [Inject] private DataBase _db;

    /// <summary>
    /// 각 타입에 맞는 파싱을 진행합니다.
    /// </summary>
    public void ParseData(E_CSVTableType type, string[] datas)
    {
        switch (type)
        {
            case E_CSVTableType.Monster:
                ParseMosterInfoDataTable(datas[(int)E_MonsterTable.Info]);
                // 추가
                // Debug.Log("몬스터 데이터 테이블 로드 완료");
                break;
        }
    }

    private void ParseMosterInfoDataTable(string data)
    {
        string[] rowMonsterInfoData = data.Split('\n');
        string[] monsterInfoData;

        for (int j = 0; j < rowMonsterInfoData.Length; j++)
        {
            monsterInfoData = rowMonsterInfoData[j].Split(',');

            S_MonsterInfoData newData = new S_MonsterInfoData();
            int tempIdx = 0;

            newData.ID = int.Parse(monsterInfoData[tempIdx++]);
            newData.Grade = (E_MonsterGrade)int.Parse(monsterInfoData[tempIdx++].Split('.')[0]);
            newData.AtkType = (E_MonsterAtkType)int.Parse(monsterInfoData[tempIdx++].Split('.')[0]);
            newData.Methods = Array.ConvertAll(monsterInfoData[tempIdx++].Split('/'), int.Parse);
            newData.Patterns = Array.ConvertAll(monsterInfoData[tempIdx++].Split('/'), int.Parse);
            newData.Health = int.Parse(monsterInfoData[tempIdx++]);
            newData.Power = int.Parse(monsterInfoData[tempIdx++]);
            newData.AtkSpeed = int.Parse(monsterInfoData[tempIdx++]);
            newData.MoveSpeed = int.Parse(monsterInfoData[tempIdx++]);
            newData.ThrowingSpeed = int.Parse(monsterInfoData[tempIdx++]);

            _db.AddMonsterInfoData(newData);
        }
    }
}
