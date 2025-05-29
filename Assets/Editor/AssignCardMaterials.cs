using UnityEditor;
using UnityEngine;
using System.IO;

public static class AssignMaterialsToCards
{
    [MenuItem("Tools/Assign Materials to Card Prefabs")]
    public static void Assign()
    {
        string prefabFolder = "Assets/CardsPrefabs";
        string materialsRoot = "Assets/materials";

        string[] prefabPaths = Directory.GetFiles(prefabFolder, "*.prefab", SearchOption.TopDirectoryOnly);
        int assignedCount = 0;

        foreach (var prefabPath in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab == null) continue;

            string prefabName = Path.GetFileNameWithoutExtension(prefabPath); // np. "Color_A (1)"

            // Wyodrębnij kolor i numer z nazwy prefabu
            string[] parts = prefabName.Split(new char[] { '_', '(', ')', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3) continue;

            string colorLetter = parts[1]; // np. "A"
            string cardNumber = parts[2];  // np. "1"

            string colorFolder = colorLetter switch
            {
                "A" => "RedCard",
                "B" => "YellowCard",
                "C" => "GreenCard",
                "D" => "BlueCard",
                _ => null
            };

            if (colorFolder == null)
            {
                Debug.LogWarning($"Nieznany kolor: {colorLetter} w {prefabName}");
                continue;
            }

            string materialPath = $"{materialsRoot}/{colorFolder}/{colorFolder.Replace("Card", "")}Card_{cardNumber}.mat";
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

            if (mat == null)
            {
                Debug.LogWarning($"Nie znaleziono materiału: {materialPath}");
                continue;
            }

            // Edytuj prefab
            GameObject instance = PrefabUtility.LoadPrefabContents(prefabPath);
            var renderer = instance.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = mat;
                assignedCount++;
                PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
            }
            PrefabUtility.UnloadPrefabContents(instance);
        }

        Debug.Log($"✅ Przypisano materiały do {assignedCount} prefabów.");
    }
}
