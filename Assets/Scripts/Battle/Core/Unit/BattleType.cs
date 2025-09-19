// COUNT 用来统计每个enum有多少个元素

public enum BattleType
{
    Normal = 0,
    Boss = 1,
}

public enum UnitAttribute
{
    BLADE = 1,
    MAGIC = 2,
    DEFENCE = 3,
    GLASS = 4,
    VATALITY = 5,
    COUNT, // 在这个上面加类型
}

public enum UnitTeamType
{
    OWNER = 0,
    ENEMY = 1,
    COUNT,
}
