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
        if (deck.Count == 0)
        {
            Debug.Log("Talia siê skoñczy³a.");
            return;
        }

        var selectedPrefab = deck[Random.Range(0, deck.Count)];
        if (isPlayer == true)
        {
            playerCard = selectedPrefab;
        } else
        {
            enemyCard = selectedPrefab;
        } 
        selectedPrefab.DrawCard();

        deck.Remove(selectedPrefab);
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
