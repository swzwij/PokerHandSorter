using UnityEngine;

[CreateAssetMenu(fileName = "Cards", menuName = "Card")]
public class Card : ScriptableObject
{
    public string suit;
    public int value;

    public Sprite art;
}
