using UnityEngine;

public class EnemyTurn : IFightState
{
    protected override string name => "EnemyTurn";

    private float time = 0f;
    public override void OnEnter()
    {
        base.OnEnter();
        UIManager.Instance.Show("EnemyTurnTip");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        // 暂时使用这种方法进行敌方回合的切换
        time += Time.deltaTime;
        if(time > 2f)
        {
            time = 0f;
            GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.playerTurn);
        }
    }

}
