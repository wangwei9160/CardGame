using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CharacterType
{
    Unknown = 0,
    Player,
    Enemy
}

public class BaseCharacter : MonoBehaviour
{
    public virtual CharacterType Type => CharacterType.Unknown;

    public int HpUIIndex = -1;
    protected virtual void Start()
    {
        UIManager.Instance.Show("HpUI", gameObject , ref HpUIIndex);
    }

}
