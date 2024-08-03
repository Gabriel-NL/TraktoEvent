using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {
    
    public const string title_state_name="TitleState";
    public const string play_state_name="PlayState";

    public static GameController gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();

    public static PlantType[] plantTypes = {
        null,//Para representar o id=0 (vaso vazio)
        new PlantType(
            "Rosa",
            1, 2,//Nível de luz
            1, 2,//Nível de água
            gameController.spriteList1
        ),
        new PlantType(
            "Girassol",
            2, 99,//Nível de luz
            1, 2,//Nível de água
            gameController.spriteList2
        ),
        new PlantType(
            "Cacto",
            3, 99,//Nível de luz
            0, 1,//Nível de água
            gameController.spriteList2
        ),
        new PlantType(
            "Carnivora",
            0, 0,//Nível de luz
            0, 0,//Nível de água
            gameController.spriteList3
        ),
        new PlantType(
            "Lótus",
            0, 1,//Nível de luz
            3, 99,//Nível de água
            gameController.spriteList2
        ),
    };

    public static string[] dayNames = {
        "Dia 1: Domingo",
        "Dia 2: Segunda-feira",
        "Dia 3: Terça-feira",
        "Dia 4: Quarta-feira",
        "Dia 5: Quinta-feira",
        "Dia 6: Sexta-feira",
        "Dia 7: Sábado"
    };

}
