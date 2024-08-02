using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    private TouchManager touchManager;

    void Awake () {
        touchManager = GetComponent<TouchManager>();
    }

    public void switchAction (int actionId) {
        touchManager.currentAction = actionId;
    }

}
