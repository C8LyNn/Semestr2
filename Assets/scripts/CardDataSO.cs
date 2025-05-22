using UnityEngine;

[CreateAssetMenu(fileName = "NewCardDataSO", menuName = "Cards/Card Data SO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;    
    public int value;           

    public void SayHello()
    {
        Debug.Log($"Karta: {cardName}, HP: {value}");
    }
}
