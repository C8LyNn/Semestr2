using UnityEditor;
using UnityEngine;

public class CardIndexerEditor : EditorWindow
{
    [MenuItem("Tools/Ustaw tylko Card Index dla kart")]
    public static void AssignCardIndexes()
    {
        string path = "Assets/CardsPrefabs";
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { path });

        int index = 0;

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject cardPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (cardPrefab == null)
                continue;

            CardData cardData = cardPrefab.GetComponent<CardData>();
            if (cardData == null)
            {
                Debug.LogWarning($"Prefab {cardPrefab.name} nie ma komponentu CardData.");
                continue;
            }

            // Pobierz przypisany ScriptableObject
            CardDataSO cardSO = GetCardDataSO(cardData);
            if (cardSO == null)
            {
                Debug.LogWarning($"Brak przypisanego CardDataSO w prefabie {cardPrefab.name}");
                continue;
            }

            // Ustaw tylko cardIndex, zostawiaj¹c name i value
            cardSO.cardIndex = index;
            EditorUtility.SetDirty(cardSO);

            Debug.Log($"Przypisano cardIndex={index} dla: {cardPrefab.name}");
            index++;
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Zakoñczono przypisywanie indeksów.");
    }

    private static CardDataSO GetCardDataSO(CardData cardData)
    {
        var so = new SerializedObject(cardData);
        var property = so.FindProperty("_cardDataSO");
        return property.objectReferenceValue as CardDataSO;
    }
}
