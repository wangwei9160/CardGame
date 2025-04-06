using System;
using System.Collections.Generic;

[Serializable]
public class TreasureClass
{
    public int ID;
    public string Name;
    public string Icon;
    public int Type;
    public string Description;
    public string Ps;
}

public class TreasureManager
{
    public static Dictionary<int, TreasureClass> m_Dic;

    static TreasureManager()
    {
        m_Dic = new Dictionary<int, TreasureClass>()
        {
            {1001, new TreasureClass() { ID = 1001, Name = "神圣守护", Icon = "1", Type = 1, Description = "战斗开始时，巫真获得1玻璃。", Ps = "" } },
            {1002, new TreasureClass() { ID = 1002, Name = "加固装置", Icon = "1", Type = 1, Description = "战斗开始时，巫真获得1防御。", Ps = "" } },
            {1003, new TreasureClass() { ID = 1003, Name = "应急护盾", Icon = "1", Type = 1, Description = "战斗开始时，巫真获得5护盾。", Ps = "" } },
            {1004, new TreasureClass() { ID = 1004, Name = "皮手套", Icon = "1", Type = 1, Description = "战斗开始时，额外抽2张牌。", Ps = "" } },
            {1005, new TreasureClass() { ID = 1005, Name = "绿色水晶", Icon = "1", Type = 1, Description = "战斗开始时，额外获得2点法力值。", Ps = "" } },
            {1006, new TreasureClass() { ID = 1006, Name = "医疗包", Icon = "1", Type = 1, Description = "战斗开始时，巫真受到5活力治疗。", Ps = "" } },
            {1007, new TreasureClass() { ID = 1007, Name = "防滑手柄", Icon = "1", Type = 1, Description = "每场战斗中，第一次使用武器不需要消耗法力值。", Ps = "" } },
            {1008, new TreasureClass() { ID = 1008, Name = "丝绸手帕", Icon = "1", Type = 1, Description = "每场战斗中，第一次使用饰品不需要消耗法力值。", Ps = "" } },
            {1009, new TreasureClass() { ID = 1009, Name = "口哨", Icon = "1", Type = 1, Description = "每场战斗中，第一张随从牌的法力值消耗减少2。", Ps = "" } },
            {1010, new TreasureClass() { ID = 1010, Name = "虎皮披风", Icon = "1", Type = 1, Description = "手牌中的随从获得+x/+x（x等于友方战场上剩余的空位）。", Ps = "1" } },
            {1011, new TreasureClass() { ID = 1011, Name = "望远镜", Icon = "1", Type = 1, Description = "你的法术牌在对最后方的敌人释放时获得+2/+2。", Ps = "" } },
            {1012, new TreasureClass() { ID = 1012, Name = "细长法杖", Icon = "1", Type = 1, Description = "你的法术牌获得+x/+x（x等于友方战场上剩余的空位）。", Ps = "1" } },
            {1013, new TreasureClass() { ID = 1013, Name = "压缩空气", Icon = "1", Type = 1, Description = "如果巫真前方没有友方随从，则巫真获得2防御。", Ps = "" } },
            {1014, new TreasureClass() { ID = 1014, Name = "含羞草", Icon = "1", Type = 1, Description = "巫真的防御-3，但巫真每次受到攻击都会获得1防御。", Ps = "" } },
            {1015, new TreasureClass() { ID = 1015, Name = "金元宝", Icon = "1", Type = 1, Description = "获得此灵物时，获得100金币。", Ps = "" } },
            {1016, new TreasureClass() { ID = 1016, Name = "残损部件", Icon = "1", Type = 1, Description = "获得此灵物时，可获得一次卡牌配件奖励。", Ps = "" } },
            {1017, new TreasureClass() { ID = 1017, Name = "鱼叉", Icon = "1", Type = 1, Description = "每当一名敌人受到攻击时，一名随机的其他敌人受到1剑攻击。", Ps = "" } },
            {1018, new TreasureClass() { ID = 1018, Name = "蜡烛", Icon = "1", Type = 1, Description = "每场战斗中，第一个死亡的友方随从将回到牌堆中。", Ps = "" } },
            {1019, new TreasureClass() { ID = 1019, Name = "羊皮卷", Icon = "1", Type = 1, Description = "手牌中的随从获得+x/+x（x等于墓地中牌的数量）。", Ps = "" } },
            {1020, new TreasureClass() { ID = 1020, Name = "金树叶", Icon = "1", Type = 1, Description = "每次击杀一名敌人，获得5金币。", Ps = "" } },
            {2001, new TreasureClass() { ID = 2001, Name = "剑鞘", Icon = "1", Type = 2, Description = "战斗开始时，巫真获得装备中的武器卡的属性值。", Ps = "" } },
            {2002, new TreasureClass() { ID = 2002, Name = "首饰盒", Icon = "1", Type = 2, Description = "战斗开始时，巫真获得装备中的饰品卡的属性值。", Ps = "" } },
            {2003, new TreasureClass() { ID = 2003, Name = "狂妄", Icon = "1", Type = 2, Description = "你的所有卡牌的攻击将发动两次，但治疗将不再生效。", Ps = "" } },
            {2004, new TreasureClass() { ID = 2004, Name = "青铜海星", Icon = "1", Type = 2, Description = "你的所有卡牌获得+1/+1。", Ps = "" } },
            {2005, new TreasureClass() { ID = 2005, Name = "磨刀石", Icon = "1", Type = 2, Description = "你每回合可以使用无限次武器。", Ps = "" } },
            {2006, new TreasureClass() { ID = 2006, Name = "臭豆腐", Icon = "1", Type = 2, Description = "巫真的活力上限固定为20，但每走一步都会完全恢复。", Ps = "" } },
            {2007, new TreasureClass() { ID = 2007, Name = "沉重石板", Icon = "1", Type = 2, Description = "战斗开始时，巫真的活力减少10（不致命），获得10防御。", Ps = "" } },
            {2008, new TreasureClass() { ID = 2008, Name = "猩红水晶", Icon = "1", Type = 2, Description = "获得此灵物时，巫真的所有活力将转化为玻璃，且不能再获得活力。", Ps = "" } },
            {2009, new TreasureClass() { ID = 2009, Name = "黑魔典", Icon = "1", Type = 2, Description = "战斗开始时，巫真的活力变为1，将损失的活力转化为2倍的护盾。", Ps = "" } },
            {2010, new TreasureClass() { ID = 2010, Name = "牛肉饺子", Icon = "1", Type = 2, Description = "每场战斗结束后，巫真的活力上限+5。", Ps = "" } },
            {2011, new TreasureClass() { ID = 2011, Name = "指南针", Icon = "1", Type = 2, Description = "选择道路时，你的选项增加一个。", Ps = "" } },
            {2012, new TreasureClass() { ID = 2012, Name = "狼皮披风", Icon = "1", Type = 2, Description = "手牌中的随从获得+x/+x（x等于友方战场上的随从数量）。", Ps = "" } },
            {2013, new TreasureClass() { ID = 2013, Name = "三色豆糕", Icon = "1", Type = 2, Description = "你每回合释放的第三张法术牌的所有属性值翻倍。", Ps = "" } },
            {2014, new TreasureClass() { ID = 2014, Name = "自转沙漏", Icon = "1", Type = 2, Description = "所有（包括敌人）回合开始时效果和回合结束时效果多触发一次。", Ps = "" } },
            {2015, new TreasureClass() { ID = 2015, Name = "号角", Icon = "1", Type = 2, Description = "你的战斗开始时效果触发两次。", Ps = "" } },
            {2016, new TreasureClass() { ID = 2016, Name = "右旋贝", Icon = "1", Type = 2, Description = "友方的所有回合结束时效果多触发一次，但所有的回合开始时效果不再生效。", Ps = "" } },
            {2017, new TreasureClass() { ID = 2017, Name = "左旋贝", Icon = "1", Type = 2, Description = "友方的所有回合开始时效果多触发一次，但所有的回合结束时效果不再生效。", Ps = "" } },
            {2018, new TreasureClass() { ID = 2018, Name = "莲蓬", Icon = "1", Type = 2, Description = "你的法术造成的伤害将随机分散给所有敌人。", Ps = "" } },
            {2019, new TreasureClass() { ID = 2019, Name = "古代小说", Icon = "1", Type = 2, Description = "你每回合开始时都将获得5点法力值，但法力值上限为5。", Ps = "" } },
            {2020, new TreasureClass() { ID = 2020, Name = "头骨", Icon = "1", Type = 2, Description = "所有死亡的友方随从都将回到牌堆。", Ps = "" } },
            {2021, new TreasureClass() { ID = 2021, Name = "巨大宝石", Icon = "1", Type = 2, Description = "你的法力值上限变为20。", Ps = "" } },
            {2022, new TreasureClass() { ID = 2022, Name = "十字镐", Icon = "1", Type = 2, Description = "你每回合开始时获得的法力值随回合数最多可增加到10点。", Ps = "" } },
        };
    }
    public static TreasureClass GetTreasureClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
