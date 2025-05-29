using UnityEngine;

public class CardDisplayManager : MonoBehaviour
{
    public Transform playerSlot, enemySlot;
    public GameObject[] allCardPrefabs; // 52 prefabów kart w odpowiedniej kolejnoœci

    private GameObject currentPlayerCard, currentEnemyCard;

    /// <summary>
    /// Wyœwietla dwie karty w scenie na podstawie danych (z klasy CardData).
    /// </summary>
    public void ShowCards(CardData playerCardData, CardData enemyCardData)
    {
        // Zniszcz stare karty jeœli s¹
        if (currentPlayerCard != null)
            Destroy(currentPlayerCard);

        if (currentEnemyCard != null)
            Destroy(currentEnemyCard);

        // Upewnij siê, ¿e indeksy s¹ poprawne
        int playerIndex = playerCardData.cardDataSO.cardIndex;
        int enemyIndex = enemyCardData.cardDataSO.cardIndex;

        if (playerIndex < 0 || playerIndex >= allCardPrefabs.Length ||
            enemyIndex < 0 || enemyIndex >= allCardPrefabs.Length)
        {
            Debug.LogError("Nieprawid³owy indeks karty!");
            return;
        }

        // Tworzenie kart w odpowiednich miejscach jako dzieci slotów
        currentPlayerCard = Instantiate(
            allCardPrefabs[playerIndex],
            playerSlot.position,
            Quaternion.identity,
            playerSlot // <- wa¿ne! przypisz parent
        );

        currentEnemyCard = Instantiate(
            allCardPrefabs[enemyIndex],
            enemySlot.position,
            Quaternion.identity,
            enemySlot // <- wa¿ne! przypisz parent
        );
    }
}
