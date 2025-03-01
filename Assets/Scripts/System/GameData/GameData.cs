using Unity.Mathematics;

public class GameData 
{
    public int CurrentLvel;     // ��ǰΪCurrentLvel�ؿ�
    public int CurrentStage;    // ��ǰ�ؿ��ĵ�CurrentStage�׶�
    

    // ������� �������Դ浽playerattribute����
    public int hp;              // ��ǰ����ֵ
    public int maxHp;           // �������ֵ
    public int money;           // ��ǰ���н������
    public int magicPower;      // ����ֵ

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
