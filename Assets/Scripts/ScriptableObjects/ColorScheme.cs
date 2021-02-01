using UnityEngine;

[CreateAssetMenu(fileName = "ColorScheme", menuName = "ScriptableObjects/ColorScheme", order = 1)]
public class ColorScheme : ScriptableObject
{
    public Color Main = Color.gray;
    public Color Bright = Color.white;
    public Color Dark = Color.black;
}
