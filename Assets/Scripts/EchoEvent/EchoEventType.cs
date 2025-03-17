using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EchoEvent{
    public enum EchoEventType
    {
        UnKnow = 0,             // 空
        FightEvent = 1 << 0,    // 斗争
        ChanceEvent = 1 << 2,   // 机遇
        FortuneEvent = 1 << 4,  // 财运
        PeaceEvent = 1 << 6,    // 宁静

        DoubleFightEvent = FightEvent + FightEvent,
        DoubleChanceEvent = ChanceEvent + ChanceEvent,
        DoubleFortuneEvent = FortuneEvent + FortuneEvent,
        DoublePeaceEvent = PeaceEvent + PeaceEvent,

        FightChaneEvent = FightEvent + ChanceEvent,
        FightFortuneEvent = FightEvent + FortuneEvent,
        FightPeaceEvent = FightEvent + PeaceEvent,

        ChanceFortuneEvent = ChanceEvent + FortuneEvent,
        ChancePeaceEvent = ChanceEvent + PeaceEvent,
        FortunePeaceEvent = FortuneEvent + PeaceEvent,
    }

    public class EchoEventConfig {
        public string name;
        public string message;

        public EchoEventConfig(string _name , string msg) {
            name = _name;
            message = msg;
        }

    }

    public class EchoEventManager 
    {
        private static Dictionary<EchoEventType , EchoEventConfig> m_Dic;

        public static List<EchoEventType> eventBaseTypeList;
        

        static EchoEventManager()
        {
            m_Dic = new Dictionary<EchoEventType, EchoEventConfig>
            {
                {EchoEventType.UnKnow , new EchoEventConfig("" ,"暂时还没选择") },
                {EchoEventType.FightEvent , new EchoEventConfig("争斗","扫清前进的道路")},
                {EchoEventType.ChanceEvent , new EchoEventConfig("机遇", "机会总是留给有准备的人")},
                {EchoEventType.FortuneEvent , new EchoEventConfig("财运", "出门在外，要备些盘缠傍身")},
                {EchoEventType.PeaceEvent , new EchoEventConfig("宁静", "劳逸结合")},
                {EchoEventType.DoubleFightEvent , new EchoEventConfig("精英怪", "我打的就是精锐")},
                {EchoEventType.DoubleChanceEvent , new EchoEventConfig("稀有事件", "有些事，可遇而不可求")},
                {EchoEventType.DoubleFortuneEvent , new EchoEventConfig("获得大量金币或遗物或卡牌", "天上真会掉点什么")},
                {EchoEventType.DoublePeaceEvent , new EchoEventConfig("", "\"屏息凝神，超凡入圣\"-提升主角血量上限并且回满血")},
                {EchoEventType.FightChaneEvent , new EchoEventConfig("", "\"未知的挑战\"-包含战斗的特殊事件")},
                {EchoEventType.FightFortuneEvent , new EchoEventConfig("", "\"富贵险中求\"-保护商人、杀人越货，卖血换钱")},
                {EchoEventType.FightPeaceEvent , new EchoEventConfig("", "\"置死地而后生\"-耐力测试")},
                {EchoEventType.ChanceFortuneEvent , new EchoEventConfig("", "\"小赌怡情\"-抽奖")},
                {EchoEventType.ChancePeaceEvent , new EchoEventConfig("", "\"命运有时候也能掌握在自己手里\"-获得特定类型的卡牌或卡牌配件")},
                {EchoEventType.FortunePeaceEvent , new EchoEventConfig("", "\"有舍才有得\"-商人")},
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


