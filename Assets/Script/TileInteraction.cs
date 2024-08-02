using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileInteraction : MonoBehaviour
{
    private Image image;
    public Color originalColor;
    public int x, y;
    public (int, int) coordinates;
    public Color hoverColor = Color.gray; // Color to use when hovering

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            originalColor = image.color;
        }
        else
        {
            Debug.LogError("No Image component found on this GameObject.");
        }
    }

    public void SetColor(Color selected_color)
    {
        image.color = selected_color;
    }

}
