using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    private CardData playerCard, enemyCard;

    public List<CardData> deck;
    private int cardsDealt = 0;

    public List<CardData> enemy_deck;
    public List<CardData> enemy_rewards;
    public List<CardData> player_deck;
    public List<CardData> player_rewards;

    private List<CardData> playerTiebreaker = new();
    private List<CardData> enemyTiebreaker = new();
    private List<int> playerOrder = new();
    private List<int> enemyOrder = new();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard(true);
            DrawCard(false);
            Count();
        }
    }

    void DrawCard(bool isPlayer)
    {
        CardData selectedCard = null;

        if (deck.Count > 0)
        {
            selectedCard = deck[Random.Range(0, deck.Count)];
            deck.Remove(selectedCard);
            Debug.Log("Dobieranie z głównej talii.");
        }
        else
        {
            if (isPlayer)
            {
                if (player_deck.Count == 0 && player_rewards.Count > 0)
                {
                    player_deck = new List<CardData>(player_rewards);
                    player_rewards.Clear();
                    Debug.Log("player_rewards → player_deck");
                }

                if (player_deck.Count > 0)
                {
                    selectedCard = player_deck[Random.Range(0, player_deck.Count)];
                    player_deck.Remove(selectedCard);
                    Debug.Log("Dobieranie z player_deck.");
                }
            }
            else
            {
                if (enemy_deck.Count == 0 && enemy_rewards.Count > 0)
                {
                    enemy_deck = new List<CardData>(enemy_rewards);
                    enemy_rewards.Clear();
                    Debug.Log("enemy_rewards → enemy_deck");
                }

                if (enemy_deck.Count > 0)
                {
                    selectedCard = enemy_deck[Random.Range(0, enemy_deck.Count)];
                    enemy_deck.Remove(selectedCard);
                    Debug.Log("Dobieranie z enemy_deck.");
                }
            }
        }

        if (selectedCard == null)
        {
            Debug.LogError("Brak dostępnych kart dla: " + (isPlayer ? "player" : "enemy"));
            return;
        }

        if (isPlayer)
            playerCard = selectedCard;
        else
            enemyCard = selectedCard;

        selectedCard.DrawCard();
        cardsDealt++;
    }

    void Count()
    {
        int playerValue = playerCard.GetValue();
        int enemyValue = enemyCard.GetValue();

        string playerName = playerCard.GetName();
        string enemyName = enemyCard.GetName();

        Debug.Log($"Gracz zagrał: {playerName}, HP: {playerValue}");
        Debug.Log($"Wróg zagrał: {enemyName}, HP: {enemyValue}");

        int result = playerValue - enemyValue;

        switch (result)
        {
            case < 0:
                Debug.Log("Enemy Win!");
                enemy_rewards.Add(playerCard);
                enemy_rewards.Add(enemyCard);
                break;

            case > 0:
                Debug.Log("Player Win!");
                player_rewards.Add(playerCard);
                player_rewards.Add(enemyCard);
                break;

            case 0:
                Debug.Log("Remis – rozpoczęcie dogrywki");
                StartCoroutine(HandleTiebreaker(playerCard, enemyCard));
                break;
        }
    }

    IEnumerator HandleTiebreaker(CardData currentPlayerCard, CardData currentEnemyCard)
    {
        int cardCount = 3;

        while (true)
        {
            playerTiebreaker.Clear();
            enemyTiebreaker.Clear();

            playerTiebreaker.Add(currentPlayerCard);
            enemyTiebreaker.Add(currentEnemyCard);

            for (int i = 1; i < cardCount; i++)
            {
                var pc = DrawFromDeck(player_deck, player_rewards);
                var ec = DrawFromDeck(enemy_deck, enemy_rewards);

                if (pc != null) playerTiebreaker.Add(pc);
                if (ec != null) enemyTiebreaker.Add(ec);
            }

            yield return StartCoroutine(WaitForPlayerOrder(cardCount));

            enemyOrder = new List<int>();
            for (int i = 0; i < cardCount; i++) enemyOrder.Add(i);
            Shuffle(enemyOrder);

            int playerPoints = 0;
            int enemyPoints = 0;

            for (int i = 0; i < cardCount; i++)
            {
                var pCard = playerTiebreaker[playerOrder[i]];
                var eCard = enemyTiebreaker[enemyOrder[i]];

                int p = pCard.GetValue();
                int e = eCard.GetValue();

                Debug.Log($"Runda {i + 1} – Gracz: {pCard.GetName()} ({p}) vs Wróg: {eCard.GetName()} ({e})");

                if (p > e) playerPoints++;
                else if (p < e) enemyPoints++;
            }

            Debug.Log($"Dogrywka: {playerPoints}:{enemyPoints}");

            if (playerPoints > enemyPoints)
            {
                Debug.Log("Gracz wygrał dogrywkę");
                player_rewards.AddRange(playerTiebreaker);
                player_rewards.AddRange(enemyTiebreaker);
                yield break;
            }
            else if (enemyPoints > playerPoints)
            {
                Debug.Log("Wróg wygrał dogrywkę");
                enemy_rewards.AddRange(playerTiebreaker);
                enemy_rewards.AddRange(enemyTiebreaker);
                yield break;
            }

            Debug.Log("Remis w dogrywce – dobieranie kolejnej karty...");
            cardCount++;
        }
    }

    CardData DrawFromDeck(List<CardData> deck, List<CardData> rewards)
    {
        if (deck.Count == 0 && rewards.Count > 0)
        {
            deck.AddRange(rewards);
            rewards.Clear();
        }

        if (deck.Count == 0)
            return null;

        int index = Random.Range(0, deck.Count);
        var card = deck[index];
        deck.RemoveAt(index);
        return card;
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    IEnumerator WaitForPlayerOrder(int cardCount)
    {
        playerOrder.Clear();

        // docelowo – interfejs gracza do ustawiania kolejności
        for (int i = 0; i < cardCount; i++)
            playerOrder.Add(i); // domyślna kolejność

        yield return null;
    }
}
