using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    // Start is called before the first frame update
    
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct Plant
{
    public int Id { get; private set; }
    public bool Light { get; private set; }
    public int WaterLevel { get; private set; }
    public (int, bool) WaterGrowthLevel { get; private set; }

    public Plant(int id, bool light, int water_level, (int, bool) water_growth_level)
    {
        Id = id;
        Light = light;
        WaterLevel = water_level;
        WaterGrowthLevel = water_growth_level;
    }
}

