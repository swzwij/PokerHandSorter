using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualCheck : MonoBehaviour
{
    [SerializeField] private HandSorter _handSorter;
    [SerializeField] private List<Card> hand = new List<Card>();

    public void CheckHand()
    {
        print(_handSorter.GetHandType(hand));
    }
}
