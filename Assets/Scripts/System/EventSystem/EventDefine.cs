public enum EventDefine 
{
    OnStartSceneUIShow,         // 战斗状态机切换
    OnHpChangeByName,           // 卡牌变化
    OnPlayerAttributeChange,    // 角色属性变化

    
    OnEnemyTurn,    // 临时使用，用于切换到敌方回合时的敌方自动掉血
    OnEnemyDeath,   // 敌方随从死亡
    // 法力值修正
    OnMagicPowerChange,
    OnMoneyChange,          // 金币数量修正

    AfterEffectShowReward,  // 奖励界面动画结束后提供奖励
    SelectMoneyReward,      // 是否拿取钱的奖励，提前点前往将放弃奖励
    SelectCardReward,       // 是否拿取卡牌的奖励，提前点前往将放弃奖励

    // 游戏进程相关
    OnBattleStart,          // 进入战斗
    OnBeforePlayerTurn,     // 进入一个新的回合
    OnPlayerTurnStart,      // 玩家可操作回合开始
    OnFinishPlayerTurn,     // 玩家回合结束
    OnMergePanelShow,

    // 调整分辨率，修改布局
    AdjustPosition,

    // ui事件
    ON_CARD_DRAG_START,     // 通知DragCard开始拖拽了
    ON_CARD_DRAG,           // 拖拽卡牌
    ON_CARD_DRAG_STOP,      // 结束拖拽
}

