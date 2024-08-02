using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class PlantController {

    public static PlantType[] plantTypes = {
        null,//Para representar o id=0 (vaso vazio)
        new PlantType(
            "Rosa",
            1, 2,//Nível de luz
            1, 2//Nível de água
        ),
        new PlantType(
            "Girassol",
            2, 99,//Nível de luz
            1, 2//Nível de água
        ),
        new PlantType(
            "Cacto",
            3, 99,//Nível de luz
            0, 1//Nível de água
        ),
        new PlantType(
            "Carnivora",
            0, 0,//Nível de luz
            0, 0//Nível de água
        ),
        new PlantType(
            "Lótus",
            0, 1,//Nível de luz
            3, 99//Nível de água
        ),
    };
}