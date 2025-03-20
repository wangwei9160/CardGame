using System.Collections.Generic;
using UnityEngine;

namespace EchoEvent
{
    public class EchoEventManager
    {
        private static Dictionary<EchoEventType, EchoEventConfig> m_Dic;

        public static List<EchoEventType> eventBaseTypeList;

        static EchoEventManager()
        {
            m_Dic = new Dictionary<EchoEventType, EchoEventConfig>
            {
                {EchoEventType.UnKnow , new EchoEventConfig("" ,"暂时还没选择") },
                {EchoEventType.FightEvent , new EchoEventConfig("争斗","“扫清前进的道路”-普通战斗" , "FightEvent" , "BattleUI")},
                {EchoEventType.ChanceEvent , new EchoEventConfig("机遇", "“出门在外，要备些盘缠傍身”-获取金币或卡牌")},
                {EchoEventType.FortuneEvent , new EchoEventConfig("财运", "“出门在外，要备些盘缠傍身”-获取金币或卡牌")},
                {EchoEventType.PeaceEvent , new EchoEventConfig("宁静", "“劳逸结合”-回血")},
                {EchoEventType.DoubleFightEvent , new EchoEventConfig("争斗x2", "“我打的就是精锐”-精英怪")},
                {EchoEventType.DoubleChanceEvent , new EchoEventConfig("机遇x2", "“有些事，可遇而不可求”-稀有事件")},
                {EchoEventType.DoubleFortuneEvent , new EchoEventConfig("财运x2", "“天上真会掉点什么”-获得大量金币或遗物或卡牌")},
                {EchoEventType.DoublePeaceEvent , new EchoEventConfig("宁静x2", "“屏息凝神，超凡入圣”-提升主角血量上限并且回满血")},
                {EchoEventType.FightChaneEvent , new EchoEventConfig("争斗x机遇", "“未知的挑战”-包含战斗的特殊事件")},
                {EchoEventType.FightFortuneEvent , new EchoEventConfig("争斗x财运", "“富贵险中求”-保护商人、杀人越货，卖血换钱")},
                {EchoEventType.FightPeaceEvent , new EchoEventConfig("争斗x宁静", "“置死地而后生”-耐力测试")},
                {EchoEventType.ChanceFortuneEvent , new EchoEventConfig("机遇x财运", "\"小赌怡情\"-抽奖")},
                {EchoEventType.ChancePeaceEvent , new EchoEventConfig("机遇x宁静", "\"命运有时候也能掌握在自己手里\"-获得特定类型的卡牌或卡牌配件")},
                {EchoEventType.FortunePeaceEvent , new EchoEventConfig("财运x宁静", "\"有舍才有得\"-商人")},
            };
            eventBaseTypeList = new List<EchoEventType> {
                EchoEventType.FightEvent ,
                EchoEventType.ChanceEvent ,
                EchoEventType.FortuneEvent ,
                EchoEventType.PeaceEvent
            };
        }

        public static EchoEventConfig GetEchoEventConfigByType(EchoEventType type)
        {
            return m_Dic[type];
        }


    }
}