using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    
    private PlantType plantType=null;
    private GameController gameController;
    
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
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
    }

    void SelectSprite () {
        //Redefine a sprite de uma mplanta dependendo do seu estágio
        if (plantType != null) {
            plantTypeName = plantType.name;
        } else {
            plantTypeName = "";
        };
    }

    void Interact (int actionId) {
        //Função chamada quando o jogador clica num vaso
        if (plantType != null) {
            switch (actionId) {
                case 1://Sol
                    //(VISUAL) Tocar animação

                    receiveLight = true;
                    break;
                case 2://Água
                    //(VISUAL) Tocar animação

                    receiveWater = true;
                    break;
                case 3://Magia
                    //(VISUAL) Tocar animação

                    AdvanceTime();
                    break;
                default://Nenhuma ação
                    break;
            }
        } else {
            InsertSeed(PlantController.plantTypes[1]);
        }
        Debug.Log("Ação " + actionId + " em " + plantTypeName);
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
            if (currentStage.x <= 0) {
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
        toNextGrowth = plantType.growthRate;
        waterPoints = 0;
        lightPoints = 0;
    }

    void React (int action) {
        //Mostra uma reação a uma determinada ação, que pode ser
        //1. sol 2. águe

        //Reações:
        //1. Muito feliz!
        //2. Feliz
        //3. Triste
        //4. Raiva!
        int reactionOutput = 0;

        switch (action) {
            case 1:
                if (waterPoints >= plantType.maxWater) {
                    reactionOutput = 4;
                } if (plantType.maxWater >= (plantType.growthRate / 2)) {
                    reactionOutput = 3;
                } if (plantType.maxWater <= waterPoints && waterPoints <= plantType.maxWater) {
                    reactionOutput = 2;
                }
                break;
            case 2:
                if (lightPoints >= plantType.maxLight) {
                    reactionOutput = 4;
                } if (plantType.maxLight >= (plantType.growthRate / 2)) {
                    reactionOutput = 3;
                } if (plantType.maxLight <= lightPoints && lightPoints <= plantType.maxLight) {
                    reactionOutput = 2;
                }
                break;
        }
        if ((plantType.minWater <= waterPoints && waterPoints <= plantType.maxWater)
        && (plantType.minLight <= lightPoints && lightPoints <= plantType.maxLight)) {
            reactionOutput = 1;
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
