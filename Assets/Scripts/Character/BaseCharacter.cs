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

public class BaseCharacter : MonoBehaviour
{
    public CharacterType type = CharacterType.Unknown;
    public virtual CharacterType Type => type;
    public void ResetCharacterType(CharacterType tp) {type = tp;}

    public int ID;
    public void ResetID(int _id) {ID = _id;}

    public int HpUIIndex = -1;
    public int maxHp = 100;
    public int hp = 50;
    protected virtual void Start() { }

    public virtual void OnHeal(int val){}
    public virtual void OnHurt(int val){}
    public virtual void OnDeadCheck(){}
    public virtual void ReSetPosition(){}
}
