using System;
using System.Collections.Generic;

[Serializable]
public class EnemyClass
{
    public int enemt_id;
    public string enemy_name;
    public int enemy_hp;
    public int enemy_blade;
    public int enemy_magic;
    public int enemy_def;
    public int enemy_glass;
    public string enemy_passive;
}

public class EnemyConfig
{
    public static Dictionary<int, EnemyClass> m_Dic;

    static EnemyConfig()
    {
        m_Dic = new Dictionary<int, EnemyClass>()
        {
            {10001, new EnemyClass() { enemt_id = 10001, enemy_name = "狼", enemy_hp = 21, enemy_blade = 4, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "每过两个回合，使下个回合造成伤害增加至1.5倍" } },
            {10002, new EnemyClass() { enemt_id = 10002, enemy_name = "食人花", enemy_hp = 26, enemy_blade = 3, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "造成伤害的同时扣除（吃掉）玩家2点法力值，累计扣除4点法力值后下回合造成6点剑伤害" } },
            {10003, new EnemyClass() { enemt_id = 10003, enemy_name = "熊", enemy_hp = 35, enemy_blade = 6, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "每次受到攻击时对攻击者造成1点固定伤害" } },
            {10004, new EnemyClass() { enemt_id = 10004, enemy_name = "石堆怪物", enemy_hp = 28, enemy_blade = 0, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "一回合不攻击(蓄力），下一回合攻击造成10点剑伤害（战斗开始时回合为蓄力回合）" } },
            {10005, new EnemyClass() { enemt_id = 10005, enemy_name = "小石子", enemy_hp = 5, enemy_blade = 0, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "一回合不攻击(蓄力），下一回合攻击造成5点剑伤害并且扣除自身5点血量" } },
            {10006, new EnemyClass() { enemt_id = 10006, enemy_name = "餮器灵", enemy_hp = 20, enemy_blade = 2, enemy_magic = 3, enemy_def = 0, enemy_glass = 0, enemy_passive = "入场时获得5点玻璃，攻击时造成2点剑伤害与3点魔法伤害" } },
            {10007, new EnemyClass() { enemt_id = 10007, enemy_name = "狂暴铸造师", enemy_hp = 50, enemy_blade = 5, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "在特殊事件中出现，每回合结束扣除自身10点活力值，但是永久增加自身1点剑伤害" } },
            {10008, new EnemyClass() { enemt_id = 10008, enemy_name = "餮染藤", enemy_hp = 18, enemy_blade = 3, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "攻击时对我方全体造成伤害" } },
            {10009, new EnemyClass() { enemt_id = 10009, enemy_name = "狐狸", enemy_hp = 1, enemy_blade = 2, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "在特殊事件中出现，入场时获得15点玻璃，三回合后召唤敌人：虎。虎被击杀后，狐狸逃走" } },
            {10011, new EnemyClass() { enemt_id = 10011, enemy_name = "虎", enemy_hp = 62, enemy_blade = 6, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "入场时获得1点防御，攻击附加1层流血效果，流血：每回合结束时让该单位受到X（X=流血层数）剑伤害，上限99层" } },
            {10010, new EnemyClass() { enemt_id = 10010, enemy_name = "狮", enemy_hp = 60, enemy_blade = 8, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "当玩家法力值≥3时，狮子下次造成伤害后将掠夺主角1点法力值并转化自身5点护盾值。" } },
            {10011, new EnemyClass() { enemt_id = 10011, enemy_name = "人类强盗", enemy_hp = 15, enemy_blade = 2, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "根据对巫真造成的伤害偷取同等数值的金钱，根据偷取量获得同等数值的剑，对应人类强盗被击败后战斗结束返还对应被偷取金钱" } },
            {10012, new EnemyClass() { enemt_id = 10012, enemy_name = "人类守卫", enemy_hp = 20, enemy_blade = 4, enemy_magic = 0, enemy_def = 0, enemy_glass = 0, enemy_passive = "每回合获得5点护盾值" } },
            {10013, new EnemyClass() { enemt_id = 10013, enemy_name = "小树苗", enemy_hp = 5, enemy_blade = 0, enemy_magic = 1, enemy_def = 0, enemy_glass = 0, enemy_passive = "每回合对我方单体造成1点魔法伤害" } },
            {10014, new EnemyClass() { enemt_id = 10014, enemy_name = "巨型树木", enemy_hp = 80, enemy_blade = 5, enemy_magic = 3, enemy_def = 0, enemy_glass = 0, enemy_passive = "入场时获得10点防御。技能1：每回合召唤一个“小树苗”，最多存在两个，召唤不占用单独回合。击败小树苗后巨型树木的防御-1，巨型树木防御最低为0；技能2：造成伤害时，附带3点魔法伤害" } },
        };
    }
    public static EnemyClass GetEnemyClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
    public static List<EnemyClass> GetAll()
    {
        List<EnemyClass> ret = new List<EnemyClass>();
        foreach (var item in m_Dic) ret.Add(item.Value);
        return ret;
    }
    public static int GetAllNum() { return m_Dic.Count; }
}
