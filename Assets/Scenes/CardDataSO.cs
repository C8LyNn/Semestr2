using UnityEngine;

// To jest WZ�R na pliki, kt�re tworzymy spod ppm, a linijka ni�ej to �cie�ka
[CreateAssetMenu(fileName = "CardDataSO", menuName = "Scriptable Objects/CardDataSO")]
public class CardDataSO : ScriptableObject
{
    public string cardName;
    public int hP;
    public int damage;
    [TextArea] public string designIdea;

    public void SayHello()
    {
        Debug.Log(cardName);
        Debug.Log(hP);
        Debug.Log(damage);
    }
}