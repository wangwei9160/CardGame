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

}


