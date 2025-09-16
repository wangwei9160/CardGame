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
    public CharacterType type = CharacterType.Unknown;
    public virtual CharacterType Type => type;
    public void ResetCharacterType(CharacterType tp) {type = tp;}

    public int ID;
    public void ResetID(int _id) {ID = _id;}

    public int HpUIIndex = -1;
    public int maxHp = 100;
    public int hp = 50;

    public int leftAttribute = 0;
    public int rightAttribute = 0;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void OnHeal(int val){}
    public virtual void OnHurt(int val){}
    public virtual void OnDeadCheck(){}
    public virtual void ReSetPosition(){}
}
