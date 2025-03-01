public enum EventDefine 
{
    OnStartSceneUIShow,         // 战斗状态机切换
    OnHpChangeByName,           // 卡牌变化
    OnPlayerAttributeChange,    // 角色属性变化

    
    OnEnemyTurn,    // 临时使用，用于切换到敌方回合时的敌方自动掉血
    OnEnemyDeath,   // 敌方随从死亡
    // 法力值修正
    OnMagicPowerChange,

    // 回合切换，临时使用
    ChangeState,

    // 调整分辨率，修改布局
    AdjustPosition,
}

