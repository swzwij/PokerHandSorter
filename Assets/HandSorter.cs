using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandSorter : MonoBehaviour
{
    public enum HandType
    {
        Royal_Flush,
        Straight_Flush,
        Four_Of_A_Kind,
        Full_House,
        Flush,
        Straight,
        Three_Of_A_Kind,
        Two_Pair,
        Pair,
        High_Card,
        NaN
    }

    [SerializeField] private List<Card> hand;

    private void Start()
    {
        hand = SortHand(hand);
        print(HandChecker(hand));
    }

    private List<Card> SortHand(List<Card> hand)
    {
        hand.Sort(SortByValue);
        return hand;
    }

    private HandType HandChecker(List<Card> hand)
    {
        if (RoyalFlush(hand)) return HandType.Royal_Flush;
        else if (StraightFlush(hand)) return HandType.Straight_Flush;
        else if (FourOfAKind(hand)) return HandType.Four_Of_A_Kind;
        //else if (FullHouse(hand)) return HandType.Full_House;
        else if (Flush(hand)) return HandType.Flush;
        else if (Straight(hand)) return HandType.Straight;
        else if (ThreeOfAKind(hand)) return HandType.Three_Of_A_Kind;
        else if (TwoPair(hand)) return HandType.Two_Pair;
        else if (Pair(hand)) return HandType.Pair;
        else if (HighCard(hand)) return HandType.High_Card;
        else return HandType.NaN;
    }

    #region HandTypeChecks

    private bool RoyalFlush(List<Card> hand)
    {
        bool isValid = true;
        string currentSuit = hand[0].suit;
        int combinedValue = 0;

        int l = hand.Count - 1;
        for (int i = 0; i < l; i++)
        {
            combinedValue += hand[i].value;

            if (!isValid) break;

            if (hand[i].suit == currentSuit && hand[i + 1].value == hand[i].value + 1) isValid = true;
            else isValid = false;
        }
        combinedValue+= hand[4].value;

        if (combinedValue != 60) isValid = false;

        return isValid;
    }

    private bool StraightFlush(List<Card> hand)
    {
        bool isValid = true;
        string currentSuit = hand[0].suit;

        int l = hand.Count -1;
        for (int i = 0; i < l; i++)
        {
            if (!isValid) break;

            if (hand[i].suit == currentSuit && hand[i + 1].value == hand[i].value + 1) isValid = true;
            else isValid = false;
        }

        return isValid;
    }

    private bool FourOfAKind(List<Card> hand)
    {
        int hasPair = 0;

        int l = hand.Count;
        for (int i = 0; i < l; i++)
        {
            Card currentCard = hand[i];

            for (int j = 0; j < l; j++)
            {
                Card comparedCard = hand[j];

                if (currentCard.value == comparedCard.value && currentCard != comparedCard)
                {
                    for (int h = 0; h < l; h++)
                    {
                        Card thirdCard = hand[h];

                        if (thirdCard.value == comparedCard.value && thirdCard != comparedCard)
                        {
                            for (int k = 0; k < l; k++)
                            {
                                Card fourCard = hand[k];

                                if (fourCard.value == comparedCard.value && fourCard != comparedCard)
                                {
                                    hasPair++;
                                }
                            }
                        }
                    }
                }
            }
        }

        bool r = hasPair == 108;
        return r;
    }

    private bool FullHouse(List<Card> hand)
    {
        if (ThreeOfAKind(hand) && Pair(hand)) return true;
        else return false;
    }

    private bool Flush(List<Card> hand)
    {
        string currentSuit = hand[0].suit;

        if (hand[0].suit == currentSuit && hand[1].suit == currentSuit && hand[2].suit == currentSuit && hand[3].suit == currentSuit && hand[4].suit == currentSuit) return true;
        else return false;
    }

    private bool Straight(List<Card> hand)
    {
        bool isValid = true;

        int l = hand.Count - 1;
        for (int i = 0; i < l; i++)
        {
            if (!isValid) break;

            if (hand[i + 1].value == hand[i].value + 1) isValid = true;
            else isValid = false;
        }

        return isValid;
    }

    private bool ThreeOfAKind(List<Card> hand)
    {
        int hasPair = 0;

        int l = hand.Count;
        for (int i = 0; i < l; i++)
        {
            Card currentCard = hand[i];

            for (int j = 0; j < l; j++)
            {
                Card comparedCard = hand[j];

                if (currentCard.value == comparedCard.value && currentCard != comparedCard)
                {
                    for (int h = 0; h < l; h++)
                    {
                        Card thirdCard = hand[h];

                        if (thirdCard.value == comparedCard.value && thirdCard != comparedCard)
                        {
                            hasPair++;
                        }
                    }
                }
            }
        }

        bool r = hasPair == 12;
        return r;
    }

    private bool TwoPair(List<Card> hand)
    {
        int hasPair =0;

        int l = hand.Count;
        for (int i = 0; i < l; i++)
        {
            Card currentCard = hand[i];

            for (int j = 0; j < l; j++)
            {
                Card comparedCard = hand[j];

                if (currentCard.value == comparedCard.value && currentCard != comparedCard)
                {
                    hasPair++;
                }
            }
        }

        bool r = hasPair == 4;
        return r;
    }

    private bool Pair(List<Card> hand)
    {
        int hasPair = 0;

        int l = hand.Count;
        for (int i = 0; i < l; i++)
        {
            Card currentCard = hand[i];

            for (int j = 0; j < l; j++)
            {
                Card comparedCard = hand[j];

                if(currentCard.value == comparedCard.value && currentCard != comparedCard)
                {
                    hasPair++;
                }
            }
        }

        bool r = hasPair == 2;
        return r;
    }

    private bool HighCard(List<Card> hand)
    {
        if (true)
        {
            return true;
        }
    }

    #endregion

    private int SortByValue(Card a, Card b)
    {
        return a.value.CompareTo(b.value);
    }
}
