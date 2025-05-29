using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class FixCardPrefabs : EditorWindow
{
    private static string cardPrefabsPath = "Assets/CardsPrefabs";
    private static string managerName = "CardDisplayManager";

    [MenuItem("Tools/Fix Card Prefabs")]
    public static void FixPrefabs()
    {
        GameObject manager = GameObject.Find(managerName);
        if (manager == null)
        {
            Debug.LogError($"Nie znaleziono obiektu '{managerName}' w scenie.");
            return;
        }

        var display = manager.GetComponent<CardDisplayManager>();
        if (display == null)
        {
            Debug.LogError("Brak komponentu CardDisplayManager na obiekcie.");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Prefab", new[] { cardPrefabsPath });
        var loadedPrefabs = guids
            .Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)))
            .Where(p => p != null)
            .OrderBy(p => ExtractCardIndex(p.name))
            .ToArray();

        if (loadedPrefabs.Length != 52)
        {
            Debug.LogWarning($"Znaleziono {loadedPrefabs.Length} prefabów, oczekiwano 52.");
        }

        foreach (var prefab in loadedPrefabs)
        {
            var hasMesh = prefab.GetComponent<MeshRenderer>() != null || prefab.GetComponentInChildren<MeshRenderer>() != null;
            if (!hasMesh)
            {
                Debug.LogWarning($"Prefab '{prefab.name}' nie ma MeshRenderera. SprawdŸ go.");
            }

            var data = prefab.GetComponent<CardData>();
            if (data == null)
            {
                Debug.LogWarning($"Prefab '{prefab.name}' nie ma przypisanego CardData. Popraw prefab.");
            }
        }

        Undo.RecordObject(display, "Fix Card Prefabs");
        display.allCardPrefabs = loadedPrefabs;
        EditorUtility.SetDirty(display);
        AssetDatabase.SaveAssets();

        Debug.Log("CardDisplayManager zaktualizowany automatycznie.");
    }

    private static int ExtractCardIndex(string name)
    {
        // Obs³uguje nazwy typu "Color_A (1)", "Color_B (13)" itd.
        string numberPart = new string(name.Where(char.IsDigit).ToArray());
        int.TryParse(numberPart, out int number);
        return number - 1; // indeks 0-based
    }
}
