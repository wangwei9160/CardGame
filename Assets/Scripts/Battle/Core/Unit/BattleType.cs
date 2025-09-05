// COUNT 用来统计每个enum有多少个元素

public enum BattleType
{
    Normal = 0,
    Boss = 1,
}

public enum UnitAttribute
{
    ATTACK = 1,
    DEFEND = 2,
    HP = 3,
    SPEED = 4,
    COUNT, // 在这个上面加类型
}

public enum UnitTeam
{
    OWNER = 1,
    ENEMY = 2,
    COUNT,
}
