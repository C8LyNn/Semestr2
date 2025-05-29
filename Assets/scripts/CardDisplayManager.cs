using UnityEngine;

public class CardDisplayManager : MonoBehaviour
{
    public Transform playerSlot, enemySlot;
    public GameObject[] allCardPrefabs; // 52 prefab�w kart w odpowiedniej kolejno�ci

    private GameObject currentPlayerCard, currentEnemyCard;

    /// <summary>
    /// Wy�wietla dwie karty w scenie na podstawie danych (z klasy CardData).
    /// </summary>
    public void ShowCards(CardData playerCardData, CardData enemyCardData)
    {
        // Zniszcz stare karty je�li s�
        if (currentPlayerCard != null)
            Destroy(currentPlayerCard);

        if (currentEnemyCard != null)
            Destroy(currentEnemyCard);

        // Upewnij si�, �e indeksy s� poprawne
        int playerIndex = playerCardData.cardDataSO.cardIndex;
        int enemyIndex = enemyCardData.cardDataSO.cardIndex;

        if (playerIndex < 0 || playerIndex >= allCardPrefabs.Length ||
            enemyIndex < 0 || enemyIndex >= allCardPrefabs.Length)
        {
            Debug.LogError("Nieprawid�owy indeks karty!");
            return;
        }

        // Tworzenie kart w odpowiednich miejscach jako dzieci slot�w
        currentPlayerCard = Instantiate(
            allCardPrefabs[playerIndex],
            playerSlot.position,
            Quaternion.identity,
            playerSlot // <- wa�ne! przypisz parent
        );

        currentEnemyCard = Instantiate(
            allCardPrefabs[enemyIndex],
            enemySlot.position,
            Quaternion.identity,
            enemySlot // <- wa�ne! przypisz parent
        );
    }
}
