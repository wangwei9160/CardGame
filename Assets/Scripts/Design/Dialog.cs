using System;
using System.Collections.Generic;

[Serializable]
public class DialogClass
{
    public int id;
    public string content;
    public List<int> op_ids;
}

public class DialogManager
{
    public static Dictionary<int, DialogClass> m_Dic;

    static DialogManager()
    {
        m_Dic = new Dictionary<int, DialogClass>()
        {
            {1, new DialogClass() { id = 1, content = "你看到一些受侵蚀的铸造装备横七竖八地散落在一棵树下，装备的主人——一名受伤的铸造师倒在路边，他的手臂上爬满了饕餮纹，眼神时而清醒时而疯狂，口中喃喃有词：“杀了我…”", op_ids = new List<int>() { 11,12,13 } } },
            {2, new DialogClass() { id = 2, content = "毫无预兆地，你的面具突然发出一道强光！这强光指引你来到一处沼泽地中，定睛一看，沼泽地中间竟飘着一个“你”。你大喊了一声，“你”被惊的掉入沼泽，又飘了起来，大叫到：“你喊什么啦！害得我全身都是泥…不对，我身上咋会沾泥…？啊，你好啊，哦不，‘我’好啊…喂，你咋没啥反应啊？”", op_ids = new List<int>() { 21,22,23 } } },
            {3, new DialogClass() { id = 3, content = "一座硕大的青铜牛像拦在路中央，眼中泛着绿光。牛像正用蹄子笨拙地刨土，你隐约能看出它正在刨的东西……好像是一个发光的奇怪符文阵，更奇怪的是，你感觉它的刨土动作还挺有节奏…", op_ids = new List<int>() { 31,32,33 } } },
            {4, new DialogClass() { id = 4, content = "不知不觉间，你已走到一处地势较低的小镇入口前。累坏了的你决定进小镇歇歇脚。沐浴在小镇集市的叫卖声中，你不禁想起了你那被毁坏的村子和幸福的时光…你决定好好补给一下再继续出发。", op_ids = new List<int>() { 41,42,43 } } },
            {5, new DialogClass() { id = 5, content = "在一块地势平坦处，你看到一个被四周被高耸的古树藏起的小木房。从远处看，房子仿佛被森林吞没，只有烟囱里偶尔升起的炊烟，才让人意识到这里有人居住。你怀着好奇心，敲开了这神秘小木房的门。“天有阴阳，地有五行，人在其中，方知其运。”你定睛一看，一个老婆婆佝偻着坐在一张小破桌后，眼角略弯，朝着你神秘地微笑。", op_ids = new List<int>() { 51,52,53 } } },
            {6, new DialogClass() { id = 6, content = "你在一片澄澈如镜的池塘边停歇，正享受着池水的甘甜，回头一看，只见一个狐狸十分飞快地叼走了你的包袱！狄立刻大吼着追了上去。在狄的帮助下，你很快就追了上去，毫不费力地抓到了那只狐狸。它深情地望着你，那楚楚可怜的眼神明白地写着四个字：“放过我吧QAQ…”", op_ids = new List<int>() { 61,62 } } },
            {7, new DialogClass() { id = 7, content = "一个背着药箱的郎中拦住了你的去路，虽然周围只有你一个人，他的左手仍然做作地举着青瓷瓶高声吆喝：“祖传金疮药！三帖止血，五帖生肌！”但你却发现他在刻意遮掩右手的伤势。", op_ids = new List<int>() { 71,72,73 } } },
            {8, new DialogClass() { id = 8, content = "老远就寻着茶香，你望见这茫茫石漠中居然支着间简陋茶摊。走近细看，布幡上绣着“清心”二字。一个老翁在店旁扇着炉火，陶壶咕嘟作响，飘出略带药草味的茶香。正好你口渴难耐，何不进去稍歇？", op_ids = new List<int>() { 81,82 } } },
            {9, new DialogClass() { id = 9, content = "在一个不起眼的角落里，一堆熄灭的篝火旁散落着断箭和破碗，一旁的帐篷被野兽撕开豁口。一柄生锈的短剑插在木桩上，一块铁砧苦诉着被锻打的过往。", op_ids = new List<int>() { 91,92,93 } } },
            {10, new DialogClass() { id = 10, content = "俗话说“人是铁饭是钢”，如果有一间小吃摊可以平息你的饥肠，你会作何选择？容不得你在温饱和钱币之间做抉择，恍然间你已站到了“三十文管饱”的幡旗下。", op_ids = new List<int>() { 101102103 } } },
        };
    }
    public static DialogClass GetDialogClassByKey(int key)
    {
        if(m_Dic.ContainsKey(key)) return m_Dic[key];
        return null;
    }
}
