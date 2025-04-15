using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



// 卡牌管理器
public class CardManager : MonoBehaviour
{
    // 单例模式
    public static CardManager Instance { get; private set; }
    
    // 卡牌预制体
    public GameObject weaponCardPrefab;
    public GameObject accessoryCardPrefab;
    public GameObject minionCardPrefab;
    public GameObject spellCardPrefab;
    
    // 玩家卡牌
    private WeaponCard playerWeapon;           // 玩家武器牌
    private List<AccessoryCard> playerAccessories = new List<AccessoryCard>();  // 玩家饰品牌
    private List<HandCard> playerDeck = new List<HandCard>();      // 玩家牌组
    private List<HandCard> playerHand = new List<HandCard>();      // 玩家手牌
    private List<HandCard> playerDiscardPile = new List<HandCard>();  // 玩家弃牌堆
    
    // 初始手牌数量
    public int initialHandSize = 5;
    
    // 每回合抽牌数量
    public int cardsPerTurn = 1;
    
    private void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // 初始化玩家卡牌
    public void InitializePlayerCards()
    {
        // 创建初始武器牌
        CreateInitialWeapon();
        
        // 创建初始饰品牌
        CreateInitialAccessory();
        
        // 创建初始牌组
        CreateInitialDeck();
        
        // 抽初始手牌
        DrawInitialHand();
    }
    
    // 创建初始武器牌
    private void CreateInitialWeapon()
    {
        GameObject weaponObj = Instantiate(weaponCardPrefab);
        playerWeapon = weaponObj.GetComponent<WeaponCard>();
        playerWeapon.InitializeWeapon("初始武器", "玩家的初始武器", 0, null, 2, 10);
        
        // 设置武器牌位置
        playerWeapon.SetPosition(new Vector3(-7f, -3f, 0f));
    }
    
    // 创建初始饰品牌
    private void CreateInitialAccessory()
    {
        GameObject accessoryObj = Instantiate(accessoryCardPrefab);
        AccessoryCard accessory = accessoryObj.GetComponent<AccessoryCard>();
        
        List<string> effects = new List<string>();
        effects.Add("提供基础防御");
        
        accessory.InitializeAccessory("初始饰品", "玩家的初始饰品", 0, null, 2, 10);
        
        // 设置饰品牌位置
        accessory.SetPosition(new Vector3(-7f, -4f, 0f));
        
        // 添加到玩家饰品牌列表
        playerAccessories.Add(accessory);
    }
    
    // 创建初始牌组
    private void CreateInitialDeck()
    {
        // 这里可以添加初始牌组的卡牌
        // 示例：添加一些随从牌和法术牌
        for (int i = 0; i < 5; i++)
        {
            // 创建随从牌
            GameObject minionObj = Instantiate(minionCardPrefab);
            MinionCard minionCard = minionObj.GetComponent<MinionCard>();
            minionCard.InitializeMinion("随从 " + (i + 1), "基础随从", 2, null, 2, 3, null);
            
            // 添加到牌组
            playerDeck.Add(minionCard);
        }
        
        for (int i = 0; i < 3; i++)
        {
            // 创建法术牌
            GameObject spellObj = Instantiate(spellCardPrefab);
            SpellCard spellCard = spellObj.GetComponent<SpellCard>();
            spellCard.InitializeSpell("法术 " + (i + 1), "基础法术", 1, null, "造成2点伤害", false);
            
            // 添加到牌组
            playerDeck.Add(spellCard);
        }
        
        // 洗牌
        ShuffleDeck();
    }
    
    // 洗牌
    private void ShuffleDeck()
    {
        for (int i = 0; i < playerDeck.Count; i++)
        {
            HandCard temp = playerDeck[i];
            int randomIndex = Random.Range(i, playerDeck.Count);
            playerDeck[i] = playerDeck[randomIndex];
            playerDeck[randomIndex] = temp;
        }
    }
    
    // 抽初始手牌
    private void DrawInitialHand()
    {
        for (int i = 0; i < initialHandSize; i++)
        {
            DrawCard();
        }
    }
    
    // 抽一张牌
    public void DrawCard()
    {
        if (playerDeck.Count > 0)
        {
            // 从牌组顶部抽一张牌
            HandCard drawnCard = playerDeck[0];
            playerDeck.RemoveAt(0);
            
            // 添加到手牌
            playerHand.Add(drawnCard);
            
            Debug.Log("抽到卡牌: " + drawnCard.cardName);
        }
        else if (playerDiscardPile.Count > 0)
        {
            // 牌组为空，洗切弃牌堆
            ReshuffleDiscardPile();
            DrawCard();
        }
        else
        {
            Debug.Log("牌组和弃牌堆都为空，无法抽牌");
        }
    }
    
    // 洗切弃牌堆
    private void ReshuffleDiscardPile()
    {
        playerDeck.AddRange(playerDiscardPile);
        playerDiscardPile.Clear();
        ShuffleDeck();
        
        Debug.Log("洗切弃牌堆，牌组数量: " + playerDeck.Count);
    }
    
    // 使用卡牌
    public bool PlayCard(HandCard card, GameObject target = null)
    {
        if (card == null)
        {
            Debug.Log("无效的卡牌");
            return false;
        }
        
        bool success = false;
        
        if (target != null)
        {
            success = card.OnPlayWithTarget(target);
        }
        else
        {
            success = card.OnPlay();
        }
        
        if (success)
        {
            // 从手牌中移除
            playerHand.Remove(card);
            
            // 如果是随从牌，不进入弃牌堆
            if (!(card is MinionCard))
            {
                // 添加到弃牌堆
                playerDiscardPile.Add(card);
            }
            
            Debug.Log("使用卡牌: " + card.cardName);
        }
        
        return success;
    }
    
    // 回合开始时抽牌
    public void DrawCardsForTurn()
    {
        for (int i = 0; i < cardsPerTurn; i++)
        {
            DrawCard();
        }
    }
    
    // 回合开始时重置固有牌状态
    public void ResetEquipmentCards()
    {
        if (playerWeapon != null)
        {
            playerWeapon.ResetTurnState();
        }
        
        foreach (AccessoryCard accessory in playerAccessories)
        {
            accessory.ResetTurnState();
        }
    }
    
    // 获取玩家武器牌
    public WeaponCard GetPlayerWeapon()
    {
        return playerWeapon;
    }
    
    // 获取玩家饰品牌列表
    public List<AccessoryCard> GetPlayerAccessories()
    {
        return playerAccessories;
    }
    
    // 获取玩家手牌列表
    public List<HandCard> GetPlayerHand()
    {
        return playerHand;
    }
    
    // 获取玩家牌组数量
    public int GetDeckCount()
    {
        return playerDeck.Count;
    }
    
    // 获取玩家弃牌堆数量
    public int GetDiscardPileCount()
    {
        return playerDiscardPile.Count;
    }
    
    // 添加卡牌到牌组
    public void AddCardToDeck(HandCard card)
    {
        playerDeck.Add(card);
        Debug.Log("添加卡牌到牌组: " + card.cardName);
    }
    
    // 添加饰品牌
    public void AddAccessoryCard(AccessoryCard accessory)
    {
        playerAccessories.Add(accessory);
        Debug.Log("添加饰品牌: " + accessory.cardName);
    }

    public void CreateCardFromData(int cardId)
    {
        CardData data = CardDataManager.GetCardDataById(cardId);
        if (data == null)
        {
            Debug.LogError($"找不到卡牌数据: {cardId}");
            return;
        }
        
        GameObject cardObj = null;
        Card card = null;
        
        switch (data.cardType.ToLower())
        {
            case "weapon":
                cardObj = Instantiate(weaponCardPrefab);
                WeaponCard weapon = cardObj.GetComponent<WeaponCard>();
                weapon.InitializeWeapon(
                    data.cardName,
                    data.description,
                    data.manaCost,
                    GetCardSprite(data.cardName),
                    data.attackValue,
                    data.durability
                );
                card = weapon;
                break;
                
            case "accessory":
                cardObj = Instantiate(accessoryCardPrefab);
                AccessoryCard accessory = cardObj.GetComponent<AccessoryCard>();
                accessory.InitializeAccessory(
                    data.cardName,
                    data.description,
                    data.manaCost,
                    GetCardSprite(data.cardName),
                    data.bonusValue,
                    data.durability
                );
                card = accessory;
                break;
                
            case "minion":
                cardObj = Instantiate(minionCardPrefab);
                MinionCard minion = cardObj.GetComponent<MinionCard>();
                minion.InitializeMinion(
                    data.cardName,
                    data.description,
                    data.manaCost,
                    GetCardSprite(data.cardName),
                    data.attackValue,
                    data.healthValue,
                    minionCardPrefab
                );
                card = minion;
                break;
                
            case "spell":
                cardObj = Instantiate(spellCardPrefab);
                SpellCard spell = cardObj.GetComponent<SpellCard>();
                spell.InitializeSpell(
                    data.cardName,
                    data.description,
                    data.manaCost,
                    GetCardSprite(data.cardName),
                    data.effects,
                    false
                );
                card = spell;
                break;
        }
        
        if (card != null)
        {
            // 设置属性
            card.SetBaseAttributes(
                ParseAttributeType(data.leftAttribute),
                ParseAttributeType(data.rightAttribute)
            );
            
            // 添加到玩家手牌
            if (card is HandCard handCard)
            {
                playerHand.Add(handCard);
            }
        }
    }
    
    private AttributeType ParseAttributeType(string attributeName)
    {
        if (System.Enum.TryParse<AttributeType>(attributeName, out AttributeType result))
        {
            return result;
        }
        return AttributeType.None;
    }
    
    private List<string> ParseEffects(string effectsString)
    {
        if (string.IsNullOrEmpty(effectsString))
            return new List<string>();
            
        return new List<string>(effectsString.Split(';'));
    }
    
    private Sprite GetCardSprite(string cardName)
    {
        return Resources.Load<Sprite>($"CardImages/{cardName}");
    }
} 