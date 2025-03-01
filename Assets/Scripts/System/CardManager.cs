using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FightType{
    player,
    enemy,
    playerTurn,
    enemyTurn,
    stateMachine,
}


public class CardManager{
    public static CardManager Instance = new CardManager();
    public List<string> cardList; //卡牌集合
    public List<string> usedCarList; //弃牌堆

    public IFightState fightUnit;

    public void Init(){
        cardList = new List<string>();
        usedCarList = new List<string>();

        List<string> tempList = new List<string>(); //临时集合
        //tempList.AddRange(IFightState.Instance.cardList); //玩家的卡存储到临时集合

        while(tempList.Count > 0){ 
            int tempIndex = Random.Range(0, tempList.Count); //随机下标

            cardList.Add(tempList[tempIndex]); //添加到卡堆

            tempList.RemoveAt(tempIndex); //临时结合删除

        }
        Debug.Log(cardList.Count);
    }

    //是否有卡
    public bool HasCard(){
        return cardList.Count > 0;
    }

    //抽卡
    public string DrawCard(){
        int num =  UnityEngine.Random.Range(0, cardList.Count);
        string id = cardList[num];

        cardList.RemoveAt(num);

        return id;
    }

}