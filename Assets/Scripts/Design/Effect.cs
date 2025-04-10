using System;
using System.Collections.Generic;

[Serializable]
public class EffectClass
{
    public int op_ids;
    public string op_name;
    public string op_dialog;
    public int effect_ids;
    public string effect_types;
    public string effect;
    public int to_id;
}

public class EffectManager
{
    public static Dictionary<int, EffectClass> m_Dic;

    static EffectManager()
    {
        m_Dic = new Dictionary<int, EffectClass>()
        {
            {11, new EffectClass() { op_ids = 11, op_name = "尝试治疗他", op_dialog = "不能就这样见死不救，你决定帮助他。但就在你触碰到他的一瞬间，铸造师突然暴起，他的眼神死盯着你，似要把你撕成两半。", effect_ids = 111, effect_types = "combat", effect = "铸造师狂暴，触发一场战斗（敌人：狂暴铸造师）。 战斗胜利后铸造师加入队伍，成为随从。", to_id = 10007 } },
            {12, new EffectClass() { op_ids = 12, op_name = "结束他的痛苦，带走他的装备", op_dialog = "在这样的侵蚀之下活着就是一种巨大的痛苦。你紧闭双眼，将剑插入他的胸膛，狄悲哀地呜咽着…你带走了他的装备，继续踏上复仇的路。", effect_ids = 112, effect_types = "relics", effect = "获得遗物：蚀工（战斗时，第一回合对敌人造成伤害＋1，下回合对敌人造成伤害-1，以此循环）", to_id = 1021 } },
            {13, new EffectClass() { op_ids = 13, op_name = "离开", op_dialog = "你很想留下帮助他，但风险太大，时间太紧，你再三权衡利弊后，只给他进行了简单的治疗，希望他能够挺过侵蚀。这可真是没办法，你救不了所有人…", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {21, new EffectClass() { op_ids = 21, op_name = "哪来的野怪，刷了练级", op_dialog = "听完他絮絮叨叨了半天，你才发现这是个调皮的器灵，正好剑痒难耐，放它一直在这终究也不是个事，就把它刷了练练手吧。", effect_ids = 111, effect_types = "combat", effect = "进入战斗（敌人：餮器灵）", to_id = 10006 } },
            {22, new EffectClass() { op_ids = 22, op_name = "发什么呆，快跑", op_dialog = "你不是没反应，只是吓傻了！喂，醒醒，快跑吧！正当你想溜之大吉时，器灵淡淡一笑：“哼，想逃？”，电光火石之间，它已附身到了你的身上！", effect_ids = 114, effect_types = "buff", effect = "获得被附身效果：最大生命值-70%，但是你的每回合获得法力值翻倍。同时后续触发相关事件", to_id = 0 } },
            {23, new EffectClass() { op_ids = 23, op_name = "跟他讲道理", op_dialog = "你跟他念了一会“为什么不能在这里吓人“的大道理，“你”很快就捂着脑袋呜咽了起来：“大师别念了…我只是个贪玩的器灵…也没害人…我错了…你放我走吧…“。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {31, new EffectClass() { op_ids = 31, op_name = "伸手摸向符文阵", op_dialog = "你伸手触碰的瞬间，牛像突然“哞——”地大叫起来，看来它不打算让你离开。", effect_ids = 111, effect_types = "combat", effect = "进入战斗（敌人：舞牛魑）", to_id = 0 } },
            {32, new EffectClass() { op_ids = 32, op_name = "教它跳舞", op_dialog = "“反正它动作这么笨……”你踩着符文阵的节奏拍手，牛像居然跟着扭动起来!/n你跟它玩了一会奇怪的符文阵，正想离开时，它也跟了上来，看来它想跟你走。", effect_ids = 115, effect_types = "friends", effect = "获得随从：舞牛痴", to_id = 300001 } },
            {33, new EffectClass() { op_ids = 33, op_name = "绕路而行", op_dialog = "你偷偷地钻进了一旁的灌木丛，溜走了。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {41, new EffectClass() { op_ids = 41, op_name = "买个苹果", op_dialog = "又大又脆的苹果！这谁能够拒绝呢？你欣然地买下苹果，大口啃了起来。", effect_ids = 116, effect_types = "trade", effect = "花费20金币，恢复1/3的生命值", to_id = 0 } },
            {42, new EffectClass() { op_ids = 42, op_name = "买点装备", op_dialog = "工欲善其事，必先利其器。你买了一些装备，希望能用上它们。", effect_ids = 116, effect_types = "trade", effect = "花费20金币，获得两个随机卡", to_id = 0 } },
            {43, new EffectClass() { op_ids = 43, op_name = "没什么想买的", op_dialog = "你想了想，还是省些钱，用在更重要的地方吧。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {51, new EffectClass() { op_ids = 51, op_name = "请求她帮你占卜", op_dialog = "“多谢惠顾。”老婆婆仍然笑着，用干枯的手迅速摸走了你递出的钱，笑意不改，口中念念有词起来…末了，她瞥了你一眼：“你的路途还算畅通，但若有不慎…”", effect_ids = 117, effect_types = "buff", effect = "下场战斗胜利后额外获得三张卡牌，但是有可能（25%）获得一个负面遗物", to_id = 0 } },
            {52, new EffectClass() { op_ids = 52, op_name = "请她教你技艺", op_dialog = "“有趣…”她的眼睛眯的更弯了，“教你一些也无妨，伸出你的手吧。”她干枯的手抓住了你的双手，你闭上眼，感到一股暖流涌上脑海…你看到某个角落里，有些装备始终派不上用场，既然带着它们很费劲，不如现在就把它们丢了吧。", effect_ids = 118, effect_types = "delete", effect = "删除一张卡牌", to_id = 0 } },
            {53, new EffectClass() { op_ids = 53, op_name = "还是离开吧", op_dialog = "这个老婆婆一看就感觉很可怕，你逃也似的关上门，离开了。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {61, new EffectClass() { op_ids = 61, op_name = "当然不能放过你！", op_dialog = "做了坏事就得付出代价，你当然不能放过这只狐狸。你正想着该怎么处置它时，它用力挣开了你的手落在地上，眼睛轻蔑地盯着你，好像在说：“打一场！”。既然它这样挑衅你，你自然没有不应战的道理。只是，你隐约感觉有哪里不对…", effect_ids = 111, effect_types = "combat", effect = "进入一场战斗（敌人：狐狸，血量较高，二回合后召唤精英怪：虎，当虎被击败后，狐狸逃跑，随后获得精英怪报酬）", to_id = 10009 } },
            {62, new EffectClass() { op_ids = 62, op_name = "还是放过它吧", op_dialog = "这狐狸年纪还小，你就不能让着它一点嘛？想到这，你放开了它，你希望好人有好报，但只收获了它向你抛来的嘲笑眼神以及它飞快逃离时向你掷来的泥沙。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {71, new EffectClass() { op_ids = 71, op_name = "买下一个药瓶", op_dialog = "你花了些小钱买下药瓶，用药粉治疗伤口后，发现药粉居然真的止血神速。", effect_ids = 116, effect_types = "trade", effect = "花费25金币，购买获得秘宝“医疗包”", to_id = 1006 } },
            {72, new EffectClass() { op_ids = 72, op_name = "揭穿他的骗局", op_dialog = "“你这药既能止血，右手为什么还在血流不止？”被戳穿骗局的假郎中恼羞成怒，掀开药箱的布，拿起刀就向你冲来。", effect_ids = 111, effect_types = "combat", effect = "触发战斗（敌人：人类盗贼）", to_id = 10011 } },
            {73, new EffectClass() { op_ids = 73, op_name = "继续赶路", op_dialog = "你选择无视他，三步并作两步离开了。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {81, new EffectClass() { op_ids = 81, op_name = "饮茶歇脚", op_dialog = "老翁不声不响递来一只粗陶碗，茶汤入喉，你顿觉疲惫稍减。", effect_ids = 118, effect_types = "delete", effect = "随机删除两张卡", to_id = 0 } },
            {82, new EffectClass() { op_ids = 82, op_name = "快步离开", op_dialog = "你总觉这里不是开茶摊的地方，定有什么猫腻，还是先走一步吧。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {91, new EffectClass() { op_ids = 91, op_name = "随便翻翻", op_dialog = "你在营地找到了许多有用的东西，该继续上路了。", effect_ids = 119, effect_types = "reward", effect = "获得两张随机卡牌与两个随机组件", to_id = 0 } },
            {92, new EffectClass() { op_ids = 92, op_name = "使用铁砧", op_dialog = "尽管铁砧应该不是很乐意让你使用它，但是你也听不到它的苦诉，谁知道呢？", effect_ids = 119, effect_types = "reward", effect = "选择加强一张卡牌", to_id = 0 } },
            {93, new EffectClass() { op_ids = 93, op_name = "离开此处", op_dialog = "营地帐篷实在破的无法使用了，你没有办法在这里歇息，继续前进吧。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
            {101, new EffectClass() { op_ids = 101, op_name = "吃点好的", op_dialog = "三十文仅仅管饱可不够，你还想吃的更好。老板自然有这种服务，给你加了一盘四十文钱的豆糕。", effect_ids = 116, effect_types = "trade", effect = "花费40金币，获得秘宝“三色豆糕”", to_id = 2013 } },
            {102, new EffectClass() { op_ids = 102, op_name = "随便吃点", op_dialog = "你还是屈从了你的胃，吃饱就上路吧。", effect_ids = 116, effect_types = "trade", effect = "花费30金币，获得秘宝“臭豆腐”", to_id = 2006 } },
            {103, new EffectClass() { op_ids = 103, op_name = "什么也不吃", op_dialog = "如果饭是钢，那么钱就是万钢之钢。想到这，你选择什么也不吃，接着上路。", effect_ids = 113, effect_types = "skip", effect = "跳过事件", to_id = 0 } },
        };
    }
    public static EffectClass GetEffectClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
