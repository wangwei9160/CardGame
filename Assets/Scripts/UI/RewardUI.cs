using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : UIViewBase
{

    public Image reward;

    public override void Init(string str, GameObject obj)
    {
        base.Init(str);
        reward.transform.position = Camera.main.WorldToScreenPoint(obj.transform.position); // 设置奖励的初始位置为最后一个死亡的角色的位置
    }


}
