using UnityEngine;
using static BTConditionalHelper;

// 选择使用的技能并且消耗检测
public class UnitSkillCostCheck : BTConditional
{
    public override BTNodeState OnUpdate()
    {
        Debug.Log("UnitSkillCostCheck OnUpdate=========");
        BattleLogicUnit currentUnit = null;
        currentUnit = GetParameter("Unit", currentUnit);
        if(currentUnit != null)
        {
            bool canUse = currentUnit.CardCostCheck();
            return canUse ? BTNodeState.SUCCESS : BTNodeState.FAILURE;
        }
        return BTNodeState.FAILURE;
    }
}