using UnityEngine;

// To jest WZÓR na pliki, które tworzymy spod ppm, a linijka ni¿ej to œcie¿ka
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