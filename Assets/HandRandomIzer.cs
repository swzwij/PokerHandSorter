using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandRandomIzer : MonoBehaviour
{
    [SerializeField] private HandSorter _handSorter;

    [SerializeField] private List<Card> cards = new List<Card>();
    private List<Card> usedCards = new List<Card>();

    [SerializeField] private List<HandTypes> handTypes = new List<HandTypes>();
    [SerializeField] private List<List<Card>> handList= new List<List<Card>>();
    private List<Card> currentHand = new List<Card>();

    [Range(0,10)]
    [SerializeField] private int handAmount;

    [SerializeField] private List<HandTypes> handInfos = new List<HandTypes>();

    private void Awake()
    {
        Randomize();
    }

    public void Randomize()
    {
        usedCards = cards;

        // Shuffle deck
        for (int i = 0; i < usedCards.Count; i++)
        {
            Card temp = usedCards[i];
            int randomIndex = UnityEngine.Random.Range(i, usedCards.Count);
            usedCards[i] = usedCards[randomIndex];
            usedCards[randomIndex] = temp;
        }

        // Fill amount of hands with cards
        for (int i = 0; i < handAmount; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int r = UnityEngine.Random.Range(0, cards.Count);
                currentHand.Add(usedCards[r]);
                usedCards.Remove(usedCards[r]);
            }

            HandTypes currentType = _handSorter.GetHandType(currentHand);

            handTypes.Add(currentType);
            handList.Add(currentHand);

            currentHand.Clear();
        }
    }
}
