
using UnityEngine;

public class UnitSkillChooseTarget : BTAction
{

    public override bool PerformAction()
    {
        Debug.Log("UnitSkillChooseTarget PerformAction=========");
        BattleUnit currentUnit = null;
        currentUnit = GetParameter("Unit", currentUnit);
        if(currentUnit != null)
        {
            if (currentUnit.IsOwnerTeam())
            {
                var target = currentUnit.GetSkillTarget();
                return target != null;
            }else
            {
                currentUnit.SetSkillTarget();
                return true;
            }
        }
        return true;
    }
}