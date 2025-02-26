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
        // ��ʱʹ�����ַ������ез��غϵ��л�
        time += Time.deltaTime;
        if(time > 2f)
        {
            time = 0f;
            GameManager.Instance.stateMachine.ChangeState(GameManager.Instance.playerTurn);
        }
    }

}
