using UnityEngine;

public class CardData : MonoBehaviour
{
    [SerializeField] private CardDataSO _cardDataSO;

    public CardDataSO cardDataSO
    {
        get => _cardDataSO;
        set => _cardDataSO = value;
    }

    public void DrawCard()
    {
    }

    public int GetValue()
    {
        return _cardDataSO.value;
    }

    public string GetName()
    {
        return _cardDataSO.cardName;
    }
}
