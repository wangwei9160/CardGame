using System;
using System.Collections.Generic;

public static class AddBuffFactory
{
    // 所有Buff在这里注册
    public static Dictionary<BuffType, Func<BattleLogicUnit,List<int>, BaseBuff>> _buffFactories = new Dictionary<BuffType, Func<BattleLogicUnit, List<int>, BaseBuff>>()
    {
        {BuffType.Attack, (owner,parameters) => new AttackBuff(owner,parameters) },
        {BuffType.Summon, (owner,parameters) => new SummonBuff(owner,parameters) }
    };

    public static BaseBuff CreateBuff(BattleLogicUnit owner,List<int> parameters)
    {
        BuffType eventType = (BuffType)parameters[0];
        if (_buffFactories.TryGetValue(eventType, out var factory))
        {
            return factory(owner,parameters);
        }
        return null;
    }

}