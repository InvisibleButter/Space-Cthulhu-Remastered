using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    private static Stack _instance;

    public static Stack Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Transform start;
    public Transform goal;

    public GameObject CardPrefab;
    public Transform belt;
    public Sprite[] CardSprites;
    public int maxCardOnBelt;
    public int[] startCardsAmount;
    public float cardDistance;
    [HideInInspector]
    public float tickTimer;

    CardController[] beltCards;
    List<Card> deck;
    List<Card> conveyorBelt;
    List<Card> discard;
    List<int> inactiveCards;
    Card[] negativeEffects;

    public void Initiate()
    {
        deck = new List<Card>();
        conveyorBelt = new List<Card>();
        discard = new List<Card>();
        inactiveCards = new List<int>();

        beltCards = new CardController[maxCardOnBelt];
        for (int i = 0; i < beltCards.Length; i++)
        {
            beltCards[i] = Instantiate(CardPrefab, belt).GetComponent<CardController>();
            beltCards[i].Initialise(CardSprites,start.position,goal.position,i);
            inactiveCards.Add(i);
        }

        for (int i = 0; i < startCardsAmount.Length; i++)
        {
            for (int j = 0; j < startCardsAmount[i]; j++)
            {
                AddCardToDeck(i);
            }
        }
        tickTimer = 0;

        //i know that could be better but its fast and easy (gamejam :P)
        negativeEffects = new Card[] {
            new DMGCard(),new ResDownCard(),new SlowCard(), new TickStopCard()
            };
    }

    public void StackUpdate()
    {
        
        if (deck.Count == 0)
        {
            shufleDiscardIntoDeck();
        }

        if (tickTimer < 0)
        {
            tickTimer = cardDistance;
            PutCardFromDeckOnBelt();
        }
        tickTimer -= Time.deltaTime * RessourceManager.Instance.tickSpeed;

        for (int i = 0; i < beltCards.Length; i++)
        {
            if (!inactiveCards.Contains(beltCards[i].CardId))
                beltCards[i].CardControlerUpdate();
        }
    }
    
    public void RemoveAllBadCard()
    {
        //deck
        for (int i = 0; i < deck.Count; i++)
        {
            if (!deck[i].isPositive())
                deck.RemoveAt(i);
        }
        //belt
        for (int i = 0; i < conveyorBelt.Count; i++)
        {
            if (!conveyorBelt[i].isPositive())
                conveyorBelt.RemoveAt(i);

        }
        for (int i = 0; i < beltCards.Length; i++)
        {
            if (!inactiveCards.Contains(i)&&!beltCards[i].isPositive)
            {
                beltCards[i].RemoveFromBelt();
                inactiveCards.Remove(i);
            }
        }

        //discard
        for (int i = 0; i < discard.Count; i++)
        {
            if (!discard[i].isPositive())
                discard.RemoveAt(i);
        }
    }
    
    public void ResolveCardFromBelt(int cardID)
    {
        discard.Add(conveyorBelt[0]);
        conveyorBelt[0].Action();
        conveyorBelt.RemoveAt(0);
        inactiveCards.Add(cardID);
    }

    public void AddNegativeEffect()
    {
        int randIndx = (int)Random.Range(0,negativeEffects.Length);
        AddNewCardToBelt(negativeEffects[randIndx]);
    }
    public void AddNewCardToBelt(Card c)
    {
        int i = inactiveCards[0];
        conveyorBelt.Add(c);
        beltCards[i].AddToBelt((int)c.getCardType(), c.isPositive());
        inactiveCards.RemoveAt(0);
        tickTimer = cardDistance;
    }

    private void PutCardFromDeckOnBelt()
    {
        int i = inactiveCards[0];
        conveyorBelt.Add(deck[0]);
        beltCards[i].AddToBelt((int)deck[0].getCardType(), deck[0].isPositive());
        deck.RemoveAt(0);
        inactiveCards.RemoveAt(0);
    }
    private void AddCardToDeck(Card card)
    {
        deck.Add(card);
    }
    private void AddCardToDeck(int i)
    {
        switch (i)
        {
            case 0:
                //amunition
                deck.Add(new AmunitionCard());
                break;
            case 1:
                //healing
                deck.Add(new HealingCard());
                break;
            case 2:
                //light
                deck.Add(new LightCard());
                break;
            case 3:
                //speed
                deck.Add(new SpeedCard());
                break;
               
           case 4:
                //shield
               deck.Add(new ShieldCard());
               break;
           case 5:
                //repair
               deck.Add(new RepairCard());
               break;
           case 6:
                //overclocking
               deck.Add(new OverclockingCard());
               break;
           
        }
    }

    private void shufleDiscardIntoDeck()
    {
        while (discard.Count > 0)
        {
            int randomCardIdx = Random.Range(0, discard.Count);
            deck.Add(discard[randomCardIdx]);
            discard.RemoveAt(randomCardIdx);
        }
    }
    
}
