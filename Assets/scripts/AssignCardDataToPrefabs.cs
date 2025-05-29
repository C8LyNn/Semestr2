using UnityEngine;
using UnityEditor;
using System.IO;

public class CardDataAutoAssigner
{
    [MenuItem("Tools/Przypisz CardDataSO do prefabów")]
    public static void AssignCardData()
    {
        string prefabPath = "Assets/CardsPrefabs";
        string dataPath = "Assets/cards";

        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab", new[] { prefabPath });

        foreach (string prefabGuid in prefabGuids)
        {
            string prefabAssetPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabAssetPath);

            GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            CardData cardData = instance.GetComponent<CardData>();
            if (cardData == null)
            {
                cardData = instance.AddComponent<CardData>();
            }

            // Nazwa prefabów to np. Color_A (1), Color_B (7) itd.
            string name = prefab.name.ToLower();
            int number = int.Parse(System.Text.RegularExpressions.Regex.Match(name, @"\((\d+)\)").Groups[1].Value);
            string color = name.Contains("a") ? "A" :
                           name.Contains("b") ? "B" :
                           name.Contains("c") ? "C" :
                           name.Contains("d") ? "D" : "?";

            string cardSoName = $"card {number + (color[0] - 'A') * 13}";
            string[] matches = AssetDatabase.FindAssets(cardSoName + " t:CardDataSO", new[] { dataPath });

            if (matches.Length == 0)
            {
                Debug.LogWarning($"Nie znaleziono CardDataSO o nazwie {cardSoName}");
                Object.DestroyImmediate(instance);
                continue;
            }

            string soPath = AssetDatabase.GUIDToAssetPath(matches[0]);
            CardDataSO dataSO = AssetDatabase.LoadAssetAtPath<CardDataSO>(soPath);

            SerializedObject so = new SerializedObject(cardData);
            so.FindProperty("_cardDataSO").objectReferenceValue = dataSO;
            so.ApplyModifiedProperties();

            PrefabUtility.ApplyPrefabInstance(instance, InteractionMode.AutomatedAction);
            Object.DestroyImmediate(instance);

            Debug.Log($"✔ Przypisano {dataSO.name} do {prefab.name}");
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
