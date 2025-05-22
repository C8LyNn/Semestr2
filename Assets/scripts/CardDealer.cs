using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{
    private CardData playerCard, enemyCard;
    public List<CardData> deck;
    private int cardsDealt = 0;
    public List<CardData> enemy_deck;
    public List<CardData> player_deck;



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
            // Dobierz kartê z g³ównej talii
            selectedCard = deck[Random.Range(0, deck.Count)];
            deck.Remove(selectedCard);
            Debug.Log("Dobieranie z g³ównej talii.");
        }
        else
        {
            // Dobierz z odpowiedniej w³asnej talii
            if (isPlayer && player_deck.Count > 0)
            {
                selectedCard = player_deck[Random.Range(0, player_deck.Count)];
                player_deck.Remove(selectedCard);
                Debug.Log("Dobieranie z player_deck.");
            }
            else if (!isPlayer && enemy_deck.Count > 0)
            {
                selectedCard = enemy_deck[Random.Range(0, enemy_deck.Count)];
                enemy_deck.Remove(selectedCard);
                Debug.Log("Dobieranie z enemy_deck.");
            }
            else
            {
                Debug.Log("Brak kart do dobrania dla " + (isPlayer ? "gracza" : "przeciwnika"));
                return;
            }
        }

        // Przypisz kartê i aktywuj
        if (isPlayer)
        {
            playerCard = selectedCard;
        }
        else
        {
            enemyCard = selectedCard;
        }

        selectedCard.DrawCard();
        cardsDealt++;
    }

    

    void Shuffle(List<GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    void Count()
    {
        int result = playerCard.GetValue() - enemyCard.GetValue();

        switch (result)
        {
            case < 0:
                Debug.Log("Enemy Win!");
                enemy_deck.Add(playerCard);
                enemy_deck.Add(enemyCard);
                break;


            case > 0:
                Debug.Log("Player Win!");
                player_deck.Add(playerCard);
                player_deck.Add(enemyCard);
                break;

            case 0:
                Debug.Log("Draw");
                break;
        }
    }
}
