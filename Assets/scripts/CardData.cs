using UnityEngine;

public class CardData : MonoBehaviour
{
    [SerializeField] private CardDataSO _cardDataSO;


    public void DrawCard()
    {

        _cardDataSO.SayHello();
    }

    public int GetValue() {

        return _cardDataSO.value;


    }
}
