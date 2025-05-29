using UnityEngine;

[CreateAssetMenu(fileName = "NewCardDataSO", menuName = "Cards/Card Data SO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;
    public int value;

    [SerializeField] public int cardIndex;

    // Getter dla indeksu karty
    public int GetCardIndex() => cardIndex;

    // Metoda testowa – wypisuje dane karty w konsoli
    public void SayHello()
    {
        Debug.Log($"Karta: {cardName}, HP: {value}");
    }

    // Dodatkowo – getter dla wartoœci karty
    public int GetValue() => value;

    // Dodatkowo – getter dla nazwy karty
    public string GetName() => cardName;
}
