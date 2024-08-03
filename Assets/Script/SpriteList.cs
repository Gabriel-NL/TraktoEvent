using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteList : MonoBehaviour
{
    public Sprite[] sprite_list;

    public Sprite GetSprite(int index){
        return sprite_list[index];
    }

}
