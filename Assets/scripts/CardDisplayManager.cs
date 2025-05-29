using UnityEngine;
using System.Collections.Generic;

public class CardDisplayManager : MonoBehaviour
{
    public Transform playerSlot, enemySlot;
    public GameObject[] allCardPrefabs;

    public Transform[] tiebreakerPlayerSlots; // 7 slotów gracza (dogrywka)
    public Transform[] tiebreakerEnemySlots;  // 7 slotów przeciwnika (dogrywka)

    private GameObject currentPlayerCard, currentEnemyCard;

    public void ShowCards(CardData playerCardData, CardData enemyCardData)
    {
        if (currentPlayerCard != null)
        {
            Destroy(currentPlayerCard);
            Debug.Log("🗑️ Usunięto starą kartę gracza");
        }

        if (currentEnemyCard != null)
        {
            Destroy(currentEnemyCard);
            Debug.Log("🗑️ Usunięto starą kartę przeciwnika");
        }

        int playerIndex = playerCardData.cardDataSO.cardIndex;
        int enemyIndex = enemyCardData.cardDataSO.cardIndex;

        if (playerIndex < 0 || playerIndex >= allCardPrefabs.Length ||
            enemyIndex < 0 || enemyIndex >= allCardPrefabs.Length)
        {
            Debug.LogError("❌ Nieprawidłowy indeks karty!");
            return;
        }

        currentPlayerCard = Instantiate(allCardPrefabs[playerIndex], playerSlot.position, Quaternion.identity, playerSlot);
        currentEnemyCard = Instantiate(allCardPrefabs[enemyIndex], enemySlot.position, Quaternion.identity, enemySlot);

        Debug.Log($"✅ Utworzono kartę gracza: {playerCardData.GetName()}");
        Debug.Log($"✅ Utworzono kartę przeciwnika: {enemyCardData.GetName()}");
    }

    public void ShowTiebreakerCards(List<CardData> playerCards, List<CardData> enemyCards, List<int> playerOrder, List<int> enemyOrder)
    {
        ClearTiebreakers();

        int count = Mathf.Min(playerCards.Count, playerOrder.Count, tiebreakerPlayerSlots.Length);
        for (int i = 0; i < count; i++)
        {
            int index = playerCards[playerOrder[i]].cardDataSO.cardIndex;
            Instantiate(allCardPrefabs[index], tiebreakerPlayerSlots[i].position, Quaternion.identity, tiebreakerPlayerSlots[i]);
        }

        count = Mathf.Min(enemyCards.Count, enemyOrder.Count, tiebreakerEnemySlots.Length);
        for (int i = 0; i < count; i++)
        {
            int index = enemyCards[enemyOrder[i]].cardDataSO.cardIndex;
            Instantiate(allCardPrefabs[index], tiebreakerEnemySlots[i].position, Quaternion.identity, tiebreakerEnemySlots[i]);
        }

        Debug.Log("🃏 Wyświetlono karty dogrywki.");
    }

    public void ClearTiebreakers()
    {
        foreach (var slot in tiebreakerPlayerSlots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var slot in tiebreakerEnemySlots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
