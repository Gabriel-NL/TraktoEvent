public class PlantType {

    public string name;
    public int minLight, maxLight;
    public int minWater, maxWater;
    public int growthRate = 3;//Número de dias para que a planta cresça
    
    public PlantType(string _name, int _minLight, int _maxLight, int _minWater, int _maxWater) {
        name = _name;
        minLight = _minLight;
        maxLight = _maxLight;
        minWater = _minWater;
        maxWater = _maxWater;
    }

    public void monsterAction () {
        //Ação feita pela planta ao se tornar um monstro
    }

}