using System.Collections.Generic;

public class DataBase
{
    private Dictionary<int, S_MonsterInfoData> MonsterInfoTable = new Dictionary<int, S_MonsterInfoData>();
    
    public void AddMonsterInfoData(S_MonsterInfoData monsterData)
    {
        MonsterInfoTable.Add(monsterData.ID, monsterData);
    }
}