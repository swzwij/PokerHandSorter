using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandRandomIzer : MonoBehaviour
{
    private HandSorter _handSorter;

    [Range(0, 10)]
    [SerializeField] private int handAmount;

    [SerializeField] private List<Card> cards = new List<Card>();
    private List<Card> currentCards = new List<Card>();
    private Dictionary<string, List<List<Card>>> hands = new Dictionary<string, List<List<Card>>>();

    [Space]
    [SerializeField] private List<GameObject> spawnPointGroups = new List<GameObject>();

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    private void Awake()
    {
        _handSorter = GetComponent<HandSorter>();

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

    private void Randomize()
    {
        currentCards = cards;

        // Shuffle deck
        for (int i = 0; i < currentCards.Count; i++)
        {
            Card temp = currentCards[i];
            int randomIndex = UnityEngine.Random.Range(i, currentCards.Count);
            currentCards[i] = currentCards[randomIndex];
            currentCards[randomIndex] = temp;
        }

        // Fill amount of hands with cards
        for (int i = 0; i < handAmount; i++)
        {
            List<Card> currentHand = new List<Card>();

            for (int j = 0; j < 5; j++)
            {
                int r = UnityEngine.Random.Range(0, cards.Count);
                currentHand.Add(currentCards[r]);
                currentCards.Remove(currentCards[r]);
            }

            string currentType = _handSorter.GetHandType(currentHand).ToString();

            hands[currentType].Add(currentHand);
        }
    }

    private void ShowCards()
    {
        int groupCount = 0;
        string[] handTypeNames = System.Enum.GetNames(typeof(HandTypes));

        // Loop through handTypes
        for (int i = 0; i < handTypeNames.Length; i++)
        {
            // Loop through list of hands corresponding with handType
            for (int k = 0; k < hands[handTypeNames[i]].Count; k++)
            {
                List<Card> item = hands[handTypeNames[i]][k];
                SpawnpointGroup currentGroup = spawnPointGroups[groupCount].GetComponent<SpawnpointGroup>(); // todo: maybe move this code up

                // Loop through each card in hand and make it visual
                for (int j = 0; j < item.Count; j++)
                {
                    Transform currentPos = currentGroup.spawnPoints[j];
                    Card card = item[j];
                    GameObject obj = new GameObject(card.suit + card.value);
                    SpriteRenderer objRenderer = obj.AddComponent<SpriteRenderer>();
                    objRenderer.sprite = card.art;
                    obj.transform.position = currentPos.position;
                }
                groupCount++;
            }
        }
    }
}
