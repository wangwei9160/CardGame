using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PerformActionType
{
    UnitAction = 0,
    UIAction = 1,
    Summon = 2,
}

public class PerformAction
{
    public PerformActionType actionType;
    public BattleLogicUnit onwer;   // 施法者
    public BattleLogicUnit target;  // 目标
    public int damage;              // 伤害
    public string name;             // 动画名称
    public Vector3 postion;         // 位置
    public System.Action BeforeAnimationStart;   // 动画播放前,
    public System.Action OnAnimationEnd;   // 动画播放结束,
}

public class PerformModule
{
    public PerformModule(BaseBattlePlayer parent)
    {
        this.Parent = parent;
        PerformActionsQueue = new();
        EventCenter.AddListener(EventDefine.OnAnimationEnd , OnAnimationEnd);
    }

    ~PerformModule()
    {
        EventCenter.RemoveListener(EventDefine.OnAnimationEnd, OnAnimationEnd);
    }
    public BaseBattlePlayer Parent { get; set; }

    public bool isPlaying = false;
    public PerformAction currentAction;
    public Queue<PerformAction> PerformActionsQueue { get; set; }
    public System.Action OnAllAnimationFinish;

    public void AddPerformAction(PerformAction action)
    {
        PerformActionsQueue.Enqueue(action);
    }

    // AnimationController
    public void PlayNextAnimation()
    {
        if(isPlaying == false && PerformActionsQueue.Count > 0)
        {
            currentAction = PerformActionsQueue.Dequeue();
            currentAction.BeforeAnimationStart?.Invoke();

            isPlaying = true;
            if(currentAction.actionType == PerformActionType.UnitAction)
            {
                BattlePerformUnit _unit = currentAction.onwer.PerformUnit;
                _unit.ChangeAnimation(currentAction.name);
            }else if(currentAction.actionType == PerformActionType.UIAction)
            {
                OnAnimationEnd();
            }else if(currentAction.actionType == PerformActionType.Summon)
            {
                AnimationController.Instance.ShowArtFunction(currentAction.name, currentAction.onwer.PerformUnit.transform.position);
                OnAnimationEnd();
            }
        }
    }

    public void OnAnimationEnd()
    {
        if (isPlaying)
        {
            Debug.Log("OnAnimationEnd");
            currentAction.OnAnimationEnd?.Invoke();
            isPlaying = false;
            if(PerformActionsQueue.Count > 0)
            {
                PlayNextAnimation();
            }else
            {
                OnAllAnimationFinish?.Invoke();
            }
        }
    }

    public void GetMessage(BattleEvent _event)
    {
        if(_event == BattleEvent.FinishEnemyTurn)
        {
            // 逻辑层结算完毕进入播放状态
            if(PerformActionsQueue.Count > 0)
            {
                OnAllAnimationFinish = () =>
                {
                    SendMessage(BattleEvent.FinishEnemyTurn);
                    OnAllAnimationFinish = null;
                };
            }else
            {
                SendMessage(BattleEvent.FinishEnemyTurn);
            }
        }
    }

    public void SendMessage(BattleEvent _event)
    {
        Parent.LogicModule.GetMessage(_event);
    }
}