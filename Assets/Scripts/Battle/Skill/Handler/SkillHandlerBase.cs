using System;
// 技能效果触发器基类
public abstract class SkillHandlerBase
{
    public abstract void Execute(SkillSelectorBase selector);
    public abstract void Execute(string cfg);

    public abstract string Description();
}