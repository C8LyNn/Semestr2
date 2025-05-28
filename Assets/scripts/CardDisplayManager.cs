using UnityEngine;

public class CardDisplayManager : MonoBehaviour
{
    public Transform playerSlot, enemySlot;
    public GameObject[] allCardPrefabs; // 52 prefab�w kart w odpowiedniej kolejno�ci

    private GameObject currentPlayerCard, currentEnemyCard;

    void Start()
    {
        // Przyk�ad: poka� kart� o indeksie 0 i 1 po uruchomieniu gry
        ShowCards(0, 1);
    }

    public void ShowCards(int playerIndex, int enemyIndex)
    {
        // Zabezpieczenie przed przekroczeniem zakresu tablicy
        if (playerIndex < 0 || playerIndex >= allCardPrefabs.Length ||
            enemyIndex < 0 || enemyIndex >= allCardPrefabs.Length)
        {
            Debug.LogError("Indeks karty poza zakresem!");
            return;
        }

        // Usu� poprzednie karty (je�li istniej�)
        if (currentPlayerCard) Destroy(currentPlayerCard);
        if (currentEnemyCard) Destroy(currentEnemyCard);

        // Stw�rz nowe instancje kart
        currentPlayerCard = Instantiate(allCardPrefabs[playerIndex], playerSlot.position, Quaternion.identity);
        currentEnemyCard = Instantiate(allCardPrefabs[enemyIndex], enemySlot.position, Quaternion.identity);
    }
}
