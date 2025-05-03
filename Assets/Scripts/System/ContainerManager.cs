using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ContainerManager : Singleton<ContainerManager>
{
    public Transform Players;
    public Transform Enemies;
    public Transform HandCard;

    // 随从交互用的幽灵角色
    public Transform ghostFollower;

    protected override void Awake()
    {
        base.Awake();
        Enemies = transform.Find("Enemies");
        Players = transform.Find("Players");
        ghostFollower = transform.Find("ghost");
        ghostFollower.gameObject.SetActive(false);
        EventCenter.AddListener<Vector2>(EventDefine.ON_FOLLOWER_DRAG , OnFollowerDrag);
        EventCenter.AddListener(EventDefine.ON_FOLLOWER_DRAG_END , OnFollowerDragEnd);
        EventCenter.AddListener(EventDefine.ON_FOLLOWER_SET , OnFollowerSet);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<Vector2>(EventDefine.ON_FOLLOWER_DRAG , OnFollowerDrag);
        EventCenter.RemoveListener(EventDefine.ON_FOLLOWER_DRAG_END , OnFollowerDragEnd);
        EventCenter.RemoveListener(EventDefine.ON_FOLLOWER_SET , OnFollowerSet);
    }

    public BaseCharacter AddPlayer()
    {
        GameObject enemy = ResourceUtil.GetEnemy("Player");
        var go = Instantiate(enemy, Players);
        BaseCharacter obj = go.GetComponent<BaseCharacter>();
        AdjustPlayerPostion();
        return obj;
    }

    public void AddPlayerInPosition(int pos)
    {
        GameObject enemy = ResourceUtil.GetEnemy("Enemy");
        var go = Instantiate(enemy, Players);
        go.transform.SetSiblingIndex(pos);
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
            Players.GetChild(i).GetComponent<BaseCharacter>().ReSetPosition();
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

    #region  随从交互

    private int _selectPos = -1;
    public void OnFollowerDrag(Vector2 pos)
    {
        _selectPos = -1;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(pos);
        ghostFollower.position = new Vector3(mousePos.x, 0, 0);
        ghostFollower.gameObject.SetActive(false);
        AdjustPlayerPostion();
        _selectPos = FollowerPostionCheck();
        AdjustPlayerPostionWithEmptyPos(_selectPos);
    }

    public void OnFollowerDragEnd()
    {
        _selectPos = -1;
        ghostFollower.gameObject.SetActive(false);
        AdjustPlayerPostion();
    }

    public void OnFollowerSet()
    {
        if(_selectPos == -1) return ;
        AddPlayerInPosition(_selectPos);
        ghostFollower.gameObject.SetActive(false);
        _selectPos = -1;
        AdjustPlayerPostion();
    }

    public int FollowerPostionCheck()
    {
        var ghostPos = ghostFollower.position;
        int num = Players.childCount;
        for(int i = 0 ; i < num ; i++)
        {
            if(i == 0) 
            {
                var left = Players.GetChild(i).position;
                Debug.Log($"{i} -- {left.x} -- {ghostPos.x}");
                if(ghostPos.x <= left.x) 
                {
                    return 0;
                }
            }else {
                var left = Players.GetChild(i - 1).position;
                var right = Players.GetChild(i).position;
                Debug.Log($"{i} -- {left.x} -- {ghostPos.x} -- {right.x}");
                if(ghostPos.x >= left.x && ghostPos.x <= right.x) 
                {
                    return i;
                }
            }
        }
        // 默认最右边
        return num;
    }

    // 调整友方单位位置用来放置随从
    public void AdjustPlayerPostionWithEmptyPos(int pos)
    {
        int num = Players.childCount;
        float middleOffset = num / 2f;
        for(int i = 0 ; i <= num ; i++)
        {
            int curPos = i;
            if(i == pos) {
                float gPos = (curPos - middleOffset) * 3f;
                ghostFollower.gameObject.SetActive(true);
                Vector3 ghostPos = new(Players.position.x + gPos, Players.position.y , 0);
                ghostFollower.localPosition = ghostPos;
                continue;
            }
            if (i > pos) curPos--;
            float xPos = (i - middleOffset) * 3f;
            Vector2 position = new Vector2(xPos, 0);
            Players.GetChild(curPos).localPosition = position;
            Players.GetChild(curPos).GetComponent<BaseCharacter>().ReSetPosition();
        }
    }

    #endregion
}
