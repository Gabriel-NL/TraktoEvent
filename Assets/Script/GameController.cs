using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    private TouchManager touchManager;

    public AudioSource SEPlayer;

    void Awake () {
        touchManager = GetComponent<TouchManager>();
        SEPlayer = GameObject.FindGameObjectsWithTag("SE Player")[0].GetComponent<AudioSource>();
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

    public AudioClip[] soundEffects = {
        //Preenchido no inspector
    };

    public void PlaySE (int id) {
        SEPlayer.clip = soundEffects[id];
        SEPlayer.Play();
    }

    public Sprite[] spriteList1 = {
        //Preenchido no inspector
    };

    public Sprite[] spriteList2 = {
        //Preenchido no inspector
    };

    public Sprite[] spriteList3 = {
        //Preenchido no inspector
    };
}
