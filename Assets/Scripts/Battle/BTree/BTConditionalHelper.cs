using System.Collections.Generic;

public delegate bool ConditionFunc(object[] obj);

public class BTConditionalHelper
{
    public enum ConditionType
    {
        Normal,    // 测试普通条件
        BattleEnd,    // 战斗结束
    };

    public static Dictionary<ConditionType, ConditionFunc> Type2Func { get; } = new Dictionary<ConditionType, ConditionFunc>
    {
        [ConditionType.Normal] = NormalConditionCheck,
        [ConditionType.BattleEnd] = IsBattleEnd,
    };

    public static ConditionFunc GetConditionFunc(ConditionType conditionType , params object[] parameters)
    {
        if(Type2Func.TryGetValue(conditionType , out var baseFunc) )
        {
            return (parameters) => baseFunc(parameters);
        }
        return (parameters) =>false;
    }

    // === 实现 ===

    private static bool IsBattleEnd(object[] parameter)
    {
        return false;
    }

    private static bool NormalConditionCheck(object[] parameter)
    {
        return true;
    }


}