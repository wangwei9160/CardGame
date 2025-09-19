using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterType
{
    Unknown = 0,
    Player,
    Card,
    Enemy,
}

// 表现层

public class BattlePerformUnit : MonoBehaviour
{
    public Animator animator;   // 动画

    public void ChangeAnimation(string _name)
    {
        StartCoroutine(PlayAnimationSequence(_name));
    }

    private IEnumerator PlayAnimationSequence(string _name)
    {
        animator.SetBool(_name, true);

        yield return new WaitForSeconds(5f);

        animator.SetBool(_name, false);
        Debug.Log("EventCenter.Broadcast(EventDefine.OnAnimationEnd);");
        EventCenter.Broadcast(EventDefine.OnAnimationEnd);
    }

    public void OnAnimationEnd()
    {
        EventCenter.Broadcast(EventDefine.OnAnimationEnd);
    }

    public BattleLogicUnit BattleUnit { get; private set; } 
    public void SetBattleUnit(BattleLogicUnit battleUnit)
    {
        BattleUnit = battleUnit;
        BattleUnit.PerformUnit = this;
    }

    public List<int> GetAttributeShow(int type)
    {
        List<int> ret = new List<int>();
        if(BattleUnit != null)
        {
            if(type == 0)
            {
                Debug.Log(LeftAttribute + " " +  BattleUnit.Attributes[LeftAttribute]);
                ret.Add(LeftAttribute);
                ret.Add(BattleUnit.Attributes[LeftAttribute]);
            }else
            {
                Debug.Log(RightAttribute + " " + BattleUnit.Attributes[RightAttribute]);
                ret.Add(RightAttribute);
                ret.Add(BattleUnit.Attributes[RightAttribute]);
            }
        }
        return ret;
    }

    public CharacterType type = CharacterType.Unknown;
    public virtual CharacterType Type => type;
    public void ResetCharacterType(CharacterType tp) {type = tp;}

    public int ID;
    public void ResetID(int _id) {ID = _id;}

    public int HpUIIndex = -1;
    public int maxHp = 100;
    public int hp = 50;

    public int LeftAttribute => (int)(BattleUnit?.leftAttributeType);
    public int RightAttribute => (int)(BattleUnit?.rightAttributeType);

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void OnHeal(int val){}
    public virtual void OnHurt(int val){}
    public virtual void OnDeadCheck(){}
    public virtual void ReSetPosition(){}
}
