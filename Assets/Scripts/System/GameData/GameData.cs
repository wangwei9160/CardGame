using System.Collections.Generic;
using Unity.Mathematics;

public class GameData 
{
    public int CurrentLvel;                         // 当前为CurrentLvel关卡
    public int CurrentStage;                        // 当前关卡的第CurrentStage阶段
    public int CurrentTurn;                         // 当前阶段的CurrentTurn回合
    public EchoEventType EchoEventType;   // 下一个关卡的类型


    // 玩家属性 后续可以存到playerattribute类内
    public int hp;              // 当前生命值
    public int maxHp;           // 最大生命值
    public int money;           // 当前持有金币数量

    public int Money
    {
        get { return money; } 
        set { money = value; EventCenter.Broadcast(EventDefine.OnMoneyChange , money); }
    }
    
    public int magicPower;      // 法力值
    public int maxMagicPower;   // 最大法力值

    // 关卡通关奖励,在没有或者选择后应该为空; 暂时使用这种方式实现,后续看是否由策划表统一
    public int MoneyReward;             // 金币奖励
    public List<int> CardReward;        // 卡牌奖励
    public List<TreasureBase> treasureList;

    public int MagicPower
    {
        get { return magicPower; }
        set { magicPower = math.min(value, maxMagicPower); } 
    }

    public int MaxMagicPower
    {
        get { return maxMagicPower; }
        set { maxMagicPower = math.min(value , 9); }
    }

    public GameData()
    {
        CurrentLvel = 1;
        CurrentStage = 0;
        money = 0;
        maxHp = 100;
        hp = 50;
        magicPower = 0;
        CardReward = new List<int>();
        EchoEventType = EchoEventType.FightEvent;
        treasureList = new List<TreasureBase>();
    }
    
}
