using System.Collections.Generic;
using UnityEngine;

public class CardDealer : MonoBehaviour
{

    private int playerCard, enemyCard;

    public List<CardData> deck;
    private int cardsDealt = 0;

   

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
            playerCard = selectedPrefab.GetValue();
        } else
        {
            enemyCard = selectedPrefab.GetValue();
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
        switch (playerCard - enemyCard)
        {
            case < 0:
                Debug.Log("Enemy Win!");
                break;

            case > 0:
                Debug.Log("Player Win!");
                break;

            case 0:
                Debug.Log("Draw");
                break;
        }
    }
}
