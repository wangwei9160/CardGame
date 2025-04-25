public interface ISkillTrigger
{
    void Init(string args);

    void Reset();

    void Execute(string args , float curTime);

}

public abstract class AbstractSkillTrigger : ISkillTrigger
{
    public abstract void Init(string args);

    public virtual void Reset(){}

    /// <summary>
    /// 触发相关技能
    /// </summary>
    /// <param name="args">数据类json格式</param>
    /// <param name="curTime">触发事件</param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void Execute(string args, float curTime){}
}