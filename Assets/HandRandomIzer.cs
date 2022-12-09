using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandRandomIzer : MonoBehaviour
{
    [SerializeField] private HandSorter _handSorter;

    [SerializeField] private List<Card> cards = new List<Card>();
    private List<Card> usedCards = new List<Card>();
    private Dictionary<string, List<List<Card>>> hands = new Dictionary<string, List<List<Card>>>();

    [SerializeField] private List<GameObject> spawnPointGroups = new List<GameObject>();

    [Range(0,10)]
    [SerializeField] private int handAmount;

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    private void Awake()
    {
        initDict();
        Randomize();
        ShowCards();
    }

    private void initDict()
    {
        string[] handTypeNames = System.Enum.GetNames(typeof(HandTypes));
        for (int i = 0; i < handTypeNames.Length; i++)
        {
            List<List<Card>> hands = new List<List<Card>>();
            this.hands.Add(handTypeNames[i], hands);
        }
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
            List<Card> currentHand = new List<Card>();

            for (int j = 0; j < 5; j++)
            {
                int r = UnityEngine.Random.Range(0, cards.Count);
                currentHand.Add(usedCards[r]);
                usedCards.Remove(usedCards[r]);
            }

            string currentType = _handSorter.GetHandType(currentHand).ToString();

            hands[currentType].Add(currentHand);

        }
    }

    private void ShowCards()
    {
        int groupCount = 0;
        string[] handTypeNames = System.Enum.GetNames(typeof(HandTypes));
        for (int i = 0; i < handTypeNames.Length; i++)
        {
            for (int k = 0; k < hands[handTypeNames[i]].Count; k++)
            {
                var item = hands[handTypeNames[i]][k];
                var currentGroup = spawnPointGroups[groupCount].GetComponent<SpawnpointGroup>();

                for (int j = 0; j < item.Count; j++)
                {
                    var currentPos = currentGroup.spawnPoints[j];
                    var card = item[j];

                    print(card);

                    var obj = new GameObject(card.suit + card.value);
                    obj.AddComponent<SpriteRenderer>();
                    obj.GetComponent<SpriteRenderer>().sprite = card.art;
                    obj.transform.position = currentPos.position;
                }
                groupCount++;

            }
        }
    }
}
