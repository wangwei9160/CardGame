using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase<GameManager>
{
    
    void Start()
    {
        UIManager.Instance.Show("BattleUI");
    }

}
