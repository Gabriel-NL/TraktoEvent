using UnityEngine;

public class PlantType {

    public string name;
    public int minLight, maxLight;
    public int minWater, maxWater;
    public int growthRate = 3;//Número de dias para que a planta cresça
    public Sprite[] spriteList;
    
    public PlantType(string _name, int _minLight, int _maxLight, int _minWater, int _maxWater, Sprite[] _spriteList) {
        name = _name;
        minLight = _minLight;
        maxLight = _maxLight;
        minWater = _minWater;
        maxWater = _maxWater;
        spriteList = _spriteList;
    }

    public void monsterAction () {
        //Ação feita pela planta ao se tornar um monstro
    }

}