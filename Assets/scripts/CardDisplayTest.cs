using UnityEngine;

public class CardDisplayTest : MonoBehaviour
{
    public CardDisplayManager displayManager;
    public CardData[] testCards; // Wype³nij w Inspectorze rêcznie 2 karty

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (testCards.Length >= 2 && displayManager != null)
            {
                displayManager.ShowCards(testCards[0], testCards[1]);
                Debug.Log($"[TEST] Pokazano: {testCards[0].cardDataSO.cardName} vs {testCards[1].cardDataSO.cardName}");
            }
            else
            {
                Debug.LogWarning("Uzupe³nij testCards[0] i [1] oraz przypisz CardDisplayManager w Inspectorze.");
            }
        }
    }
}
