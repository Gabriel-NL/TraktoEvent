using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    
    public PlantType plantType=null;
    public int lightPoints=0, waterPoints=0;
    public int age=0;
    public int toNextGrowth=99;
    public bool receiveLight=false, receiveWater=false;
    public Vector2 currentStage=new Vector2(0,0);

    private SpriteRenderer spriteRenderer;

    void Awake () {
        SelectSprite();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void SelectSprite () {
        //Redefine a sprite de uma mplanta dependendo do seu estágio
    }

    void Interact () {
        //Função chamada quando o jogador clica num vaso
    }

    void InsertSeed (PlantType seed) {
        //Plantar semente num vaso vazio
        if (plantType != null) {
            plantType = seed;
            SelectSprite();
        } else {
            Debug.Log("Não é possível plantar num vaso cheio");
        }
    }

    void AdvanceTime () {
        //Função chamada quando 1. o dia avança 2. time skip é usado
        if (receiveLight) lightPoints += 1;
        if (receiveWater) waterPoints += 1;
        
        toNextGrowth -= 1;
        if (toNextGrowth <= 0) {
            AdvanceStage();
        }
    }

    void AdvanceStage () {
        //Calcula para qual estado a planta deve seguir e ajusta sua sprite
        
    }

    void Remove () {
        //Remove a planta, a revertendo para um vaso vazio
        plantType = null;
        lightPoints=0;
        waterPoints=0;
        age=0;
        toNextGrowth=99;
        receiveLight=false;
        receiveWater=false;
        currentStage=new Vector2(0,0);

        SelectSprite();
    }

}
