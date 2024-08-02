using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    
    private PlantType plantType=null;
    
    public string plantTypeName="";
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
        if (plantType != null) {
            plantTypeName = plantType.name;
        } else {
            plantTypeName = "";
        };
    }

    void Interact () {
        //Função chamada quando o jogador clica num vaso
        Debug.Log("Interacting w/" + gameObject.name);
        InsertSeed(PlantController.plantTypes[1]);
    }

    void InsertSeed (PlantType seed) {
        //Plantar semente num vaso vazio
        if (plantType == null) {
            plantType = seed;
            SelectSprite();
            toNextGrowth = plantType.growthRate;
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

        if ((plantType.minWater <= waterPoints && waterPoints <= plantType.maxWater)
        && (plantType.minLight <= lightPoints && lightPoints <= plantType.maxLight)) {
            if (currentStage.x < 0) {
                //Branch ruim -> Sucesso
                currentStage.x = 1;
            } else {
                //Branch boa -> Sucesso
                currentStage.y += 1;
            }
        } else {
            if (currentStage.x > 0) {
                //Branch boa -> Falha
                currentStage.x = -1;
            } else {
                //Branch ruim -> Falha
                currentStage.y += 1;
            }
        }
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
