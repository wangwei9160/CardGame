using UnityEngine;
using static BTConditionalHelper;

// 所有战斗内行为树优先判断战斗状态
public class BattleStateCheck : BTConditional
{
    public override BTNodeState OnUpdate()
    {
        Debug.Log("BattleStateCheck");
        object[] objects = new object[1];
        var endBattle = BTConditionalHelper.GetConditionFunc(ConditionType.BattleEnd)(objects);
        return !endBattle ? BTNodeState.SUCCESS : BTNodeState.FAILURE;
    }
}