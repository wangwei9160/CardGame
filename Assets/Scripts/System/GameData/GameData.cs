using Unity.Mathematics;

public class GameData 
{
    public int CurrentLvel;     // 当前为CurrentLvel关卡
    public int CurrentStage;    // 当前关卡的第CurrentStage阶段
    

    // 玩家属性 后续可以存到playerattribute类内
    public int hp;              // 当前生命值
    public int maxHp;           // 最大生命值
    public int money;           // 当前持有金币数量
    public int magicPower;      // 法力值

    public int MagicPower
    {
        get { return magicPower; }
        set { magicPower = math.min(value, 5); } 
    }

    public GameData()
    {
        CurrentLvel = 1;
        CurrentStage = 0;
        money = 0;
        maxHp = 100;
        hp = maxHp;
        magicPower = 0;
    }
    
}
