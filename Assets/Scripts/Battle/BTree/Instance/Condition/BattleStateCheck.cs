using UnityEngine;
using static BTConditionalHelper;

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