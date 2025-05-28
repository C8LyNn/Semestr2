using UnityEngine;
using UnityEditor;
using System.IO;

public class CardIndexerEditor : EditorWindow
{
    [MenuItem("Tools/Przypisz CardIndex automatycznie")]
    static void AssignCardIndexes()
    {
        string path = "Assets/cards"; // Œcie¿ka do folderu z CardDataSO
        string[] guids = AssetDatabase.FindAssets("t:CardDataSO", new[] { path });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            CardDataSO cardData = AssetDatabase.LoadAssetAtPath<CardDataSO>(assetPath);

            string name = Path.GetFileNameWithoutExtension(assetPath); // np. "card 5"
            if (name.StartsWith("card "))
            {
                string numberStr = name.Substring(5); // "5"
                if (int.TryParse(numberStr, out int index))
                {
                    cardData.cardIndex = index - 1; // bo indeksujemy od 0
                    EditorUtility.SetDirty(cardData);
                    Debug.Log($"Przypisano index {index - 1} dla {name}");
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Zakoñczono przypisywanie indeksów kart.");
    }
}
