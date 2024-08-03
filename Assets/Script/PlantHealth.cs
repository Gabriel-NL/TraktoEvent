using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    
    private PlantType plantType=null;
    private GameController gameController;
    private ControlUI controlUI;
    private TimeProgression timeProgression;
    private SpriteRenderer reactionImage;
    
    public string plantTypeName="";
    public int lightPoints=0, waterPoints=0;
    public int age=0;
    public int toNextGrowth=3;
    public bool receiveLight=false, receiveWater=false;
    public Vector2 currentStage=new Vector2(0,0);

    private SpriteRenderer plantSprite;

    void Awake () {
        reactionImage = transform.Find("Reaction").GetComponent<SpriteRenderer>();
        plantSprite = transform.Find("Plant").gameObject.GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        controlUI= GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<ControlUI>();
        timeProgression = gameController.gameObject.GetComponent<TimeProgression>();
        
        if (plantType != null) {
            SelectSprite();
        };
        NewDay();

        int newPlantId = int.Parse(gameObject.name.Split(' ')[1]);
        InsertSeed(Constants.plantTypes[newPlantId]);
    }

    void SelectSprite () {
        //Redefine a sprite de uma mplanta dependendo do seu estágio

        switch (currentStage.y) {
            case 0:
                Debug.Log(plantType.spriteList);
                plantSprite.sprite = plantType.spriteList[0];
                break;
            case 1:
                plantSprite.sprite = plantType.spriteList[1];
                break;
            case 2:
                if (currentStage.x >= 0) {
                    plantSprite.sprite = plantType.spriteList[2];
                } else {
                    plantSprite.sprite = plantType.spriteList[3];
                };
            break;
        };
    }

    void Interact (int actionId) {
        //Função chamada quando o jogador clica num vaso
        if (plantType != null && currentStage.y < 2) {
            Transform childTransform;
            switch (actionId) {
                case 1://Sol
                    //(VISUAL) Tocar animação
                    childTransform = transform.Find("Sun");
                    Debug.Log("" + childTransform.name);
                        
                    receiveLight=!receiveLight;
                    childTransform.gameObject.SetActive(receiveLight);
                    React(actionId);

                    break;
                case 2://Água
                    //(VISUAL) Tocar animação

                    if (!receiveWater)
                    {
                        
                        receiveWater = true;
                        childTransform = transform.Find("Water");
                        Debug.Log("" + childTransform.name);
                        childTransform.gameObject.SetActive(receiveWater);
                        React(actionId);
                    }

                    break;
                case 3://Magia
                    //(VISUAL) Tocar animação
                    if (controlUI.GetSlotsSize()>0)
                    {
                        controlUI.ConsumeSlots();
                        AdvanceTime();

                    }else {
                        Debug.Log("A magia acabou :(");
                    }
                    break;
                default://Nenhuma ação
                    break;
            }
            
        } else {
            InsertSeed(Constants.plantTypes[1]);
        }

        Debug.Log("Ação " + actionId + " em " + plantTypeName);
    }

    void InsertSeed (PlantType seed) {
        //Plantar semente num vaso vazio
        if (plantType == null) {
            plantType = seed;
            toNextGrowth = plantType.growthRate;

            //Atualiza o nome público da planta
            if (plantType != null) {
                plantTypeName = plantType.name;
            } else {
                plantTypeName = "";
            };

            SelectSprite();
        } else {
            Debug.Log("Não é possível plantar num vaso cheio");
        }
    }

    void AdvanceTime () {
        gameController.PlaySE(5);

        //Função chamada quando 1. o dia avança 2. time skip é usado
        if (receiveLight) lightPoints += 1;
        if (receiveWater) waterPoints += 1;

        toNextGrowth -= 1;
        if (toNextGrowth <= 0 && currentStage.y < 2) {
            Debug.Log("Advance");
            AdvanceStage();
        }
    }

    void AdvanceStage () {
        //Calcula para qual estado a planta deve seguir e ajusta sua sprite

        gameController.PlaySE(3);
        if ((plantType.minWater <= waterPoints && waterPoints <= plantType.maxWater)
        && (plantType.minLight <= lightPoints && lightPoints <= plantType.maxLight)) {
            if (currentStage.x < 0) {
                //Branch ruim -> Sucesso
                currentStage.x = 1;
            } else {
                //Branch boa -> Sucesso
                currentStage.x = 1;
                currentStage.y += 1;
            }
        } else {
            if (currentStage.x > 0) {
                //Branch boa -> Falha
                currentStage.x = -1;
            } else {
                //Branch ruim -> Falha
                currentStage.x = -1;
                currentStage.y += 1;
            }
        }
        toNextGrowth = plantType.growthRate;
        waterPoints = 0;
        lightPoints = 0;

        SelectSprite();
    }

    void React (int action) {
        //Mostra uma reação a uma determinada ação, que pode ser
        //1. sol 2. águe

        //Reações:
        //1. Muito feliz!
        //2. Feliz
        //3. Triste
        //4. Raiva!
        int reactionOutput = 2;

        switch (action) {
            case 1:
                if (waterPoints >= plantType.maxWater) {
                    reactionOutput = 4;
                } if (waterPoints >= (plantType.maxWater / 2)) {
                    reactionOutput = 3;
                } if (plantType.maxWater <= 1) {
                    reactionOutput = 3;
                }
                break;
            case 2:
                if (lightPoints >= plantType.maxLight) {
                    reactionOutput = 4;
                } if (lightPoints >= (plantType.maxLight / 2)) {
                    reactionOutput = 3;
                } if (plantType.maxLight <= 1) {
                    reactionOutput = 3;
                }
                break;
        }
        if ((plantType.minWater <= waterPoints && waterPoints <= plantType.maxWater)
        && (plantType.minLight <= lightPoints && lightPoints <= plantType.maxLight)) {
            reactionOutput = 1;
        };
        if (plantType.name == "Carnivora") {
            reactionOutput = 4;
        }
        StartCoroutine(ReactAndWait(reactionOutput));
    }

    IEnumerator ReactAndWait (int reactionOutput) {
        gameController.PlaySE(4);
        reactionImage.sprite = gameController.reactionSprites[reactionOutput];
        yield return new WaitForSeconds(1);
        reactionImage.sprite = gameController.reactionSprites[0];
    }

    void Remove () {
        //Remove a planta, a revertendo para um vaso vazio
        plantType = null;
        lightPoints=0;
        waterPoints=0;
        age=0;
        toNextGrowth=3;
        receiveLight=false;
        receiveWater=false;
        currentStage=new Vector2(0,0);

        SelectSprite();
    }

    public void NewDay(){
        // Debug.Log("OHAYO SEKAI GOOD MORNING WORLD!!!");
        receiveLight=false;
        receiveWater=false;
        GameObject sunTransform = transform.Find("Sun").gameObject;
        sunTransform.SetActive(receiveLight);
        GameObject waterTransform = transform.Find("Water").gameObject;
        waterTransform.gameObject.SetActive(receiveWater);
    }




}
