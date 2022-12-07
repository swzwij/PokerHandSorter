using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandSorter : MonoBehaviour
{
    private HandTypes _currentHandType;

    public HandTypes GetHandType(List<Card> hand)
    {
        hand = SortHand(hand);
       
        // Check for 2 identicle cards
        bool isValid = true;

        int l = hand.Count;
        for (int i = 0; i < l; i++)
        {
            Card currentCard = hand[i];
            int duplicates = -1;

            for (int j = 0; j < l; j++)
            {
                Card comparedCard = hand[j];

                if (currentCard == comparedCard) duplicates++;
            }

            if (duplicates > 0) isValid = false;
        }
        
        if (!isValid) _currentHandType = HandTypes.Invalid_Hand;
        else RoyalFlush(hand);

        return _currentHandType;
    }

    #region HandTypeChecks

    private void RoyalFlush(List<Card> hand)
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

        if (isValid) _currentHandType = HandTypes.Royal_Flush;
        else StraightFlush(hand);   
    }

    private void StraightFlush(List<Card> hand)
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

        if (isValid) _currentHandType = HandTypes.Straight_Flush;
        else FourOfAKind(hand);
    }

    private void FourOfAKind(List<Card> hand)
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
        if (r) _currentHandType = HandTypes.Four_Of_A_Kind;
        else FullHouse(hand);
    }

    private void FullHouse(List<Card> hand)
    {
        if (false) _currentHandType = HandTypes.Full_House;
        else Flush(hand);
    }

    private void Flush(List<Card> hand)
    {
        string currentSuit = hand[0].suit;

        bool isValid = hand[0].suit == currentSuit && hand[1].suit == currentSuit && hand[2].suit == currentSuit && hand[3].suit == currentSuit && hand[4].suit == currentSuit;
        if (isValid) _currentHandType = HandTypes.Flush;
        else Straight(hand);
    }

    private void Straight(List<Card> hand)
    {
        bool isValid = true;

        int l = hand.Count - 1;
        for (int i = 0; i < l; i++)
        {
            if (!isValid) break;

            if (hand[i + 1].value == hand[i].value + 1) isValid = true;
            else isValid = false;
        }

        if (isValid) _currentHandType = HandTypes.Straight;
        else ThreeOfAKind(hand);
    }

    private void ThreeOfAKind(List<Card> hand)
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
        if (r) _currentHandType = HandTypes.Three_Of_A_Kind;
        else TwoPair(hand);
    }

    private void TwoPair(List<Card> hand)
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
        if (r) _currentHandType = HandTypes.Two_Pair;
        else Pair(hand);
    }

    private void Pair(List<Card> hand)
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
        if (r) _currentHandType = HandTypes.Pair;
        else HighCard(hand);
    }

    private void HighCard(List<Card> hand)
    {
        if (true) _currentHandType = HandTypes.High_Card;
    }

    #endregion

    #region Hand Sorting

    private List<Card> SortHand(List<Card> hand)
    {
        hand.Sort(SortByValue);
        return hand;
    }

    private int SortByValue(Card a, Card b)
    {
        return a.value.CompareTo(b.value);
    }

    #endregion
}
