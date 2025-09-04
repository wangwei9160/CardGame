using System.Collections.Generic;

public delegate bool ConditionFunc();

public class BTConditionalHelper
{
    public enum ConditionType
    {
        Normal,    // 测试普通条件
    };

    public static Dictionary<ConditionType, ConditionFunc> Type2Func { get; } = new Dictionary<ConditionType, ConditionFunc>
    {
        [ConditionType.Normal] = NormalConditionCheck,
    };

    public static ConditionFunc GetConditionFunc(ConditionType conditionType , params object[] parameters)
    {
        if(Type2Func.TryGetValue(conditionType , out var baseFunc) )
        {
            return baseFunc;
        }
        return ()=>false;
    }

    // === 实现 ===

    private static bool NormalConditionCheck()
    {
        return true;
    }


}