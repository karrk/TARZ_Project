public enum E_CSVTableType
{
    None = -1,
    Monster,

    Size,
}

#region 몬스터 데이터 관련

public struct S_MonsterInfoData
{
    public int ID;
    public E_MonsterGrade Grade;
    public E_MonsterAtkType AtkType;
    public int[] Methods;
    public int[] Patterns; // 패턴관련 상의가 필요함
    public float Health;
    public float Power;
    public float AtkSpeed;
    public float MoveSpeed;
    public float ThrowingSpeed;
}

public enum E_MonsterTable
{
    None = -1,
    Info,
    AttackMethod,

    Size,
}

public enum E_MonsterGrade
{
    None = -1,
    Normal,
    Elite,
    Boss,

    Size,
}

public enum E_MonsterAtkType
{
    None = -1,
    Melee,
    Ranged,

    Size,
}

#endregion

