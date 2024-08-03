using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    public Sprite[] sprite_list;

    private Sprite[] estados=new Sprite[5];

    public Sprite GetSprite(int index){
        return sprite_list[index];
    }
    public Sprite GetTest(int index){
        return estados[index];
    }
    void Start()
    {
        estados = new Sprite[5];

        // Define cores para cada sprite
        Color[] colors = new Color[5]
        {
            Color.gray,
            Color.red,
            Color.green,
            Color.black,
            Color.blue
        };

        // Inicializa cada sprite com uma cor diferente
        for (int i = 0; i < estados.Length; i++)
        {
            estados[i] = CreateColoredSprite(colors[i], 100, 100);
        }
    }

    private Sprite CreateColoredSprite(Color color, int width, int height)
    {
        // Cria uma nova textura com a cor especificada
        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        texture.SetPixels(pixels);
        texture.Apply();

        // Cria um sprite a partir da textura
        return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }



}
