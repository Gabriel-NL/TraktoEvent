using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFlipper : MonoBehaviour
{
    public int actionId;
    public Sprite offSprite;
    public Sprite onSprite;

    private TouchManager touchManager;

    void Awake () {
        touchManager = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<TouchManager>();
    }

    public void Flip () {
        touchManager.gameObject.GetComponent<GameController>().PlaySE(0);
        if (actionId == touchManager.currentAction) {
            GetComponent<Image>().sprite = onSprite;
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
            foreach (GameObject o in buttons) {
                if (o != gameObject) o.BroadcastMessage("Off");
            };
        } else {
            Off();
        };
    }

    public void Off () {
        GetComponent<Image>().sprite = offSprite;
    }
}
