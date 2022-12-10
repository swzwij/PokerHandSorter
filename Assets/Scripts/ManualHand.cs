using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualHand : MonoBehaviour
{
    private HandSorter _handSorter;
    [SerializeField] private List<Card> hand = new List<Card>();

    private void Awake()
    {
        _handSorter = GetComponent<HandSorter>();
    }

    public void CheckHand()
    {
        print(_handSorter.GetHandType(hand));
    }

    public void AddCard(string cardName)
    {

    }
}
