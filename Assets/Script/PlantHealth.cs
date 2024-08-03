using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantHealth : MonoBehaviour
{
    
    private PlantType plantType=null;
    private GameController gameController;
    private ControlUI controlUI;
    private SpriteList spriteList;
    private TimeProgression timeProgression;
    
    
    public string plantTypeName="";
    public int lightPoints=0, waterPoints=0;
    public int age=0;
    public int toNextGrowth=99;
    public bool receiveLight=false, receiveWater=false;
    public Vector2 currentStage=new Vector2(0,0);

    private List<Sprite> effect_list= new List<Sprite>();

    private SpriteRenderer spriteRenderer;

    void Awake () {
        SelectSprite();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        controlUI= GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<ControlUI>();
        spriteList = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<SpriteList>();
        
        timeProgression = gameController.gameObject.GetComponent<TimeProgression>();
        NewDay();
        

    }

    void SelectSprite () {
        //Redefine a sprite de uma mplanta dependendo do seu estágio

        if (currentStage== new Vector2(0,0))
        {
            spriteRenderer.sprite= spriteList.GetTest(0);
        }

        if (currentStage== new Vector2(1,0))
        {
            spriteRenderer.sprite= spriteList.GetTest(1);
        }
 
        
        if (plantType != null) {
            plantTypeName = plantType.name;
        } else {
            plantTypeName = "";
        };
    }

    void Interact (int actionId) {
        //Função chamada quando o jogador clica num vaso
        if (plantType != null) {
            Transform childTransform;
            switch (actionId) {
                case 1://Sol
                    //(VISUAL) Tocar animação
                    childTransform = transform.Find("Sun");
                    Debug.Log("" + childTransform.name);
                        
                    receiveLight=!receiveLight;
                    childTransform.gameObject.SetActive(receiveLight);
                    if (receiveLight)
                    {
                        Debug.Log("Apaga luz" );
                        
                    }else
                    {
                        Debug.Log("Acende luz" );
                    }

                    break;
                case 2://Água
                    //(VISUAL) Tocar animação

                    if (!receiveWater)
                    {
                        
                        receiveWater = true;
                        childTransform = transform.Find("Water");
                        Debug.Log("" + childTransform.name);
                        childTransform.gameObject.SetActive(receiveWater);
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

    public void NewDay(){
        Debug.Log("OHAYO SEKAI GOOD MORNING WORLD!!!");
        receiveLight=false;
        receiveWater=false;
        GameObject sunTransform = transform.Find("Sun").gameObject;
        sunTransform.SetActive(receiveLight);
        GameObject waterTransform = transform.Find("Water").gameObject;
        waterTransform.gameObject.SetActive(receiveWater);
    }




}
