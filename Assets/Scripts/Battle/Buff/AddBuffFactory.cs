using System;
using System.Collections.Generic;

public static class AddBuffFactory
{
    // 所有Buff在这里注册
    public static Dictionary<BuffType, Func<List<int>, BaseBuff>> _buffFactories = new Dictionary<BuffType, Func<List<int>, BaseBuff>>()
    {
        {BuffType.Attack, (parameters) => new AttackBuff(parameters) }
    };

    public static BaseBuff CreateBuff(List<int> parameters)
    {
        BuffType eventType = (BuffType)parameters[0];
        if (_buffFactories.TryGetValue(eventType, out Func<List<int>, BaseBuff> factory))
        {
            return factory(parameters);
        }
        return null;
    }

}