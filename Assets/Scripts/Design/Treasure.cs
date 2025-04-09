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
    public string PS;
}

public class TreasureManager
{
    public static Dictionary<int, TreasureClass> m_Dic;

    static TreasureManager()
    {
        m_Dic = new Dictionary<int, TreasureClass>()
        {
            {1001, new TreasureClass() { ID = 1001, Name = "神圣守护", Icon = "1001神圣守护", Type = 1, Description = "战斗开始时，巫真获得1玻璃增益。", PS = "" } },
            {1002, new TreasureClass() { ID = 1002, Name = "加固装置", Icon = "1002加固装置", Type = 1, Description = "战斗开始时，巫真获得1防御增益。", PS = "" } },
            {1003, new TreasureClass() { ID = 1003, Name = "应急护盾", Icon = "1003应急护盾", Type = 1, Description = "战斗开始时，巫真获得5护盾。", PS = "" } },
            {1004, new TreasureClass() { ID = 1004, Name = "皮手套", Icon = "1004皮手套", Type = 1, Description = "战斗开始时，额外抽2张牌。", PS = "" } },
            {1005, new TreasureClass() { ID = 1005, Name = "绿色水晶", Icon = "1005绿色水晶", Type = 1, Description = "战斗开始时，额外获得2点法力值。", PS = "" } },
            {1006, new TreasureClass() { ID = 1006, Name = "医疗包", Icon = "1006医疗包", Type = 1, Description = "战斗开始时，巫真受到5活力治疗。", PS = "" } },
            {1007, new TreasureClass() { ID = 1007, Name = "防滑手柄", Icon = "1007防滑手柄", Type = 1, Description = "每场战斗中，第一次使用武器不需要消耗法力值。", PS = "" } },
            {1008, new TreasureClass() { ID = 1008, Name = "丝绸手帕", Icon = "1008丝绸手帕", Type = 1, Description = "每场战斗中，第一次使用饰品不需要消耗法力值。", PS = "" } },
            {1009, new TreasureClass() { ID = 1009, Name = "口哨", Icon = "1009口哨", Type = 1, Description = "每场战斗中，第一张随从牌的法力值消耗减少2。", PS = "" } },
            {1010, new TreasureClass() { ID = 1010, Name = "虎皮披风", Icon = "1010虎皮披风", Type = 1, Description = "手牌中的随从获得+x/+x（x等于友方战场的随从数量）。", PS = "" } },
            {1011, new TreasureClass() { ID = 1011, Name = "望远镜", Icon = "1011望远镜", Type = 1, Description = "你的法术牌在对最后方的敌人释放时获得+2/+2。", PS = "" } },
            {1012, new TreasureClass() { ID = 1012, Name = "细长法杖", Icon = "1012细长法杖", Type = 1, Description = "你的法术牌获得+x/+x（x等于友方战场上剩余的空位）。", PS = "" } },
            {1013, new TreasureClass() { ID = 1013, Name = "压缩空气", Icon = "1013压缩空气", Type = 1, Description = "如果巫真前方没有友方随从，则巫真获得2防御。", PS = "" } },
            {1014, new TreasureClass() { ID = 1014, Name = "含羞草", Icon = "1014含羞草", Type = 1, Description = "战斗开始时，巫真获得3防御减益，但巫真每次受到攻击都会获得1防御增益。", PS = "" } },
            {1015, new TreasureClass() { ID = 1015, Name = "金元宝", Icon = "1015金元宝", Type = 1, Description = "获得此灵物时，获得300金币。", PS = "" } },
            {1016, new TreasureClass() { ID = 1016, Name = "残损部件", Icon = "1016残损部件", Type = 1, Description = "获得此灵物时，可获得两次卡牌配件奖励。", PS = "" } },
            {1017, new TreasureClass() { ID = 1017, Name = "鱼叉", Icon = "1017鱼叉", Type = 1, Description = "每当一名敌人受到攻击时，一名随机的友方随从获得1剑增益。", PS = "" } },
            {1018, new TreasureClass() { ID = 1018, Name = "蜡烛", Icon = "1018蜡烛", Type = 1, Description = "每场战斗中，第一个死亡的友方随从将不会进入墓地，而是回到手牌中。", PS = "" } },
            {1019, new TreasureClass() { ID = 1019, Name = "羊皮卷", Icon = "1019羊皮卷", Type = 1, Description = "手牌中的随从获得+x/+x（x等于墓地中牌的数量）。", PS = "" } },
            {1020, new TreasureClass() { ID = 1020, Name = "金树叶", Icon = "1020金树叶", Type = 1, Description = "每有一名敌人死亡，获得5金币。", PS = "" } },
            {2001, new TreasureClass() { ID = 2001, Name = "剑鞘", Icon = "2001剑鞘", Type = 2, Description = "战斗开始时，巫真获得装备中的武器卡的属性值。", PS = "" } },
            {2002, new TreasureClass() { ID = 2002, Name = "首饰盒", Icon = "2002首饰盒", Type = 2, Description = "战斗开始时，巫真获得装备中的饰品卡的属性值。", PS = "" } },
            {2003, new TreasureClass() { ID = 2003, Name = "狂妄", Icon = "2003狂妄", Type = 2, Description = "你的所有卡牌的攻击将发动两次，但治疗将不再生效。", PS = "" } },
            {2004, new TreasureClass() { ID = 2004, Name = "青铜海星", Icon = "2004青铜海星", Type = 2, Description = "你的所有卡牌获得+3/+3。", PS = "" } },
            {2005, new TreasureClass() { ID = 2005, Name = "磨刀石", Icon = "2005磨刀石", Type = 2, Description = "你每回合可以使用无限次武器。", PS = "" } },
            {2006, new TreasureClass() { ID = 2006, Name = "臭豆腐", Icon = "2006臭豆腐", Type = 2, Description = "巫真的活力上限固定为20，但每场战斗结束时都会获得20活力治疗。", PS = "" } },
            {2007, new TreasureClass() { ID = 2007, Name = "沉重石板", Icon = "2007沉重石板", Type = 2, Description = "战斗开始时，巫真受到10剑攻击，获得10防御增益。", PS = "" } },
            {2008, new TreasureClass() { ID = 2008, Name = "猩红水晶", Icon = "2008猩红水晶", Type = 2, Description = "获得此灵物时，巫真的所有活力将转化为玻璃，且不能再获得活力。", PS = "" } },
            {2009, new TreasureClass() { ID = 2009, Name = "黑魔典", Icon = "2009黑魔典", Type = 2, Description = "战斗开始时，巫真的剩余活力变为1，将损失的活力转化为2倍的护盾。", PS = "" } },
            {2010, new TreasureClass() { ID = 2010, Name = "牛肉饺子", Icon = "2010牛肉饺子", Type = 2, Description = "每场战斗结束后，巫真的活力上限+5。", PS = "和2006臭豆腐同时持有时，活力上限将固定为20不会增长。" } },
            {2011, new TreasureClass() { ID = 2011, Name = "指南针", Icon = "2011指南针", Type = 2, Description = "选择道路时，你可以刷新一次。", PS = "这样就不用考虑UI变化的问题，加个刷新按钮就好。" } },
            {2012, new TreasureClass() { ID = 2012, Name = "狼皮披风", Icon = "2012狼皮披风", Type = 2, Description = "手牌中的随从获得+x/+x（x等于友方战场上剩余的空位）。", PS = "" } },
            {2013, new TreasureClass() { ID = 2013, Name = "三色豆糕", Icon = "2013三色豆糕", Type = 2, Description = "你每回合释放的第三张法术牌的所有属性值翻倍。", PS = "" } },
            {2014, new TreasureClass() { ID = 2014, Name = "自转沙漏", Icon = "2014自转沙漏", Type = 2, Description = "所有（包括敌人的）回合开始时效果和回合结束时效果多触发一次。", PS = "" } },
            {2015, new TreasureClass() { ID = 2015, Name = "号角", Icon = "2015号角", Type = 2, Description = "你的战斗开始时效果多触发一次。", PS = "" } },
            {2016, new TreasureClass() { ID = 2016, Name = "右旋贝", Icon = "2016右旋贝", Type = 2, Description = "友方的所有回合结束时效果多触发一次，但回合开始时效果不再生效。", PS = "" } },
            {2017, new TreasureClass() { ID = 2017, Name = "左旋贝", Icon = "2017左旋贝", Type = 2, Description = "友方的所有回合开始时效果多触发一次，但回合结束时效果不再生效。", PS = "" } },
            {2018, new TreasureClass() { ID = 2018, Name = "莲蓬", Icon = "2018莲蓬", Type = 2, Description = "你的法术进行的攻击将随机分散给所有敌人。", PS = "如10剑攻击，则拆分成10个1剑攻击，每个1剑攻击随机选择一个敌人进行攻击。" } },
            {2019, new TreasureClass() { ID = 2019, Name = "古代小说", Icon = "2019古代小说", Type = 2, Description = "从第一回合开始，你就能每回合获得5点法力值，但法力值上限减少5。", PS = "" } },
            {2020, new TreasureClass() { ID = 2020, Name = "头骨", Icon = "2020头骨", Type = 2, Description = "所有死亡的友方随从都将回到牌堆。", PS = "" } },
            {2021, new TreasureClass() { ID = 2021, Name = "巨大宝石", Icon = "2021巨大宝石", Type = 2, Description = "你的法力值上限增加10。", PS = "" } },
            {2022, new TreasureClass() { ID = 2022, Name = "十字镐", Icon = "2022十字镐", Type = 2, Description = "你每回合开始时获得的法力值随回合数最多可增加到10点。", PS = "如果和2019古代小说（虽然在没有2021巨大宝石的时候并没有实际效果）同时持有，则按照每回合获取法力值增加1的规则，第二回合开始时就能获取6点法力值，第五回合开始时就能获取10点法力值。" } },
        };
    }
    public static TreasureClass GetTreasureClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
