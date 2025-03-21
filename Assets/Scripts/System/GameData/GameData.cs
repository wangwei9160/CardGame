using System.Collections.Generic;
using Unity.Mathematics;

public class GameData 
{
    public int CurrentLvel;                         // ��ǰΪCurrentLvel�ؿ�
    public int CurrentStage;                        // ��ǰ�ؿ��ĵ�CurrentStage�׶�
    public int CurrentTurn;                         // ��ǰ�׶ε�CurrentTurn�غ�
    public EchoEventType EchoEventType;   // ��һ���ؿ�������


    // ������� �������Դ浽playerattribute����
    public int hp;              // ��ǰ����ֵ
    public int maxHp;           // �������ֵ
    public int money;           // ��ǰ���н������

    public int Money
    {
        get { return money; } 
        set { money = value; EventCenter.Broadcast(EventDefine.OnMoneyChange , money); }
    }
    
    public int magicPower;      // ����ֵ
    public int maxMagicPower;   // �����ֵ

    // �ؿ�ͨ�ؽ�������û�л���ѡ���Ӧ��Ϊ��; ��ʱʹ�����ַ�ʽʵ�֣��������Ƿ��ɲ߻���ͳһ
    public int MoneyReward;             // ��ҽ���
    public List<int> CardReward;        // ���ƽ���

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
        hp = maxHp;
        magicPower = 0;
        CardReward = new List<int>();
        EchoEventType = EchoEventType.FightEvent;
    }
    
}
