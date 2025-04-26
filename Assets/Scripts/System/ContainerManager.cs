using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : Singleton<ContainerManager>
{
    public Transform Players;
    public Transform Enemies;
    public Transform HandCard;

    protected override void Awake()
    {
        base.Awake();
        Enemies = transform.Find("Enemies");
        Players = transform.Find("Players");
    }

    public BaseCharacter AddPlayer()
    {
        GameObject enemy = ResourceUtil.GetEnemy("Player");
        var go = Instantiate(enemy, Players);
        BaseCharacter obj = go.GetComponent<BaseCharacter>();
        AdjustPlayerPostion();
        return obj;
    }

    public BaseEnemy AddEnemy()
    {
        GameObject enemy = ResourceUtil.GetEnemy("Enemy");
        var go = Instantiate(enemy, Enemies);
        BaseEnemy obj = go.GetComponent<BaseEnemy>();
        obj.Index = Enemies.childCount;
        AdjustEnemyPostion();
        return obj;
    }

    public void AdjustPlayerPostion()
    {
        int num = Players.childCount;
        float middleOffset = (num - 1) / 2f;
        for(int i = 0 ; i < num ; i++)
        {
            float xPos = (i - middleOffset) * 3f;
            Vector2 position = new Vector2(xPos, 0);
            Players.GetChild(i).localPosition = position;
        }
    }

    public void AdjustEnemyPostion()
    {
        int num = Enemies.childCount;
        float middleOffset = (num - 1) / 2f;
        for(int i = 0 ; i < num ; i++)
        {
            float xPos = (i - middleOffset) * 3f;
            Vector2 position = new Vector2(xPos, 0);
            Enemies.GetChild(i).localPosition = position;
        }
    }

}
