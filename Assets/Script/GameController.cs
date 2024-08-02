using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    private TouchManager touchManager;

    private float speelSlots=3;

    void Awake () {
        touchManager = GetComponent<TouchManager>();
    }

    public void SwitchAction (int actionId) {
        if (actionId != touchManager.currentAction) {
            touchManager.currentAction = actionId;
            Debug.Log("Ação: " + actionId);
        } else {
            touchManager.currentAction = 0;
            Debug.Log("Ação resetada");
        }
    }

    private void PopulateSpellSlots(){

        float x1=50,x2=0,x3=-50;
        float margin=25;
        float elementSize=25;

        

    }




}
