using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    private TouchManager touchManager;

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

}
