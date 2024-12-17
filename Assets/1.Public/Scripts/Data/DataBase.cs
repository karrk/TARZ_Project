using System.Collections.Generic;

// 게임에서 사용될 데이터의 원본을 담아두기 위한 클래스
public class DataBase
{
    private Dictionary<int, S_MonsterInfoData> MonsterInfoTable = new Dictionary<int, S_MonsterInfoData>();
    
    public void AddMonsterInfoData(S_MonsterInfoData monsterData)
    {
        MonsterInfoTable.Add(monsterData.ID, monsterData);
    }
}