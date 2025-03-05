using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterType
{
    Unknown = 0,
    Player,
    Card
}

public class BaseCharacter : MonoBehaviour
{
    public virtual CharacterType Type => CharacterType.Unknown;

    public int HpUIIndex = -1;
    public int maxHp = 100;
    public int hp = 50;
    protected virtual void Start() { }

}
