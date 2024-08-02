using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeProgression : MonoBehaviour
{
    public int currentDay = 1;
    public int deadlineDay = 9;

    public int maxMagicUses = 3;
    public int currentMagicUses = 3;
    public int magicRefreshRate = 1;
    public int magicRefreshDays = 1;
    public int refreshCountdown = 0;
    
    private Image dayProgressBar=null;
    private float stageWidth;

    void Awake () {
        AdjustProgressBar();
    }

    void AdjustProgressBar () {
        //(VISUAL) Ajusta a largura da imagem 'progress bar' dependendo do dia atual

        //dayProgressBar.rectTransform.sizeDelta = new Vector2(currentDay * stageWidth, dayProgressBar.rectTransform.sizeDelta.y);
    }

    public void NextDay () {
        //(VISUAL) Chamar alguma transição para indicar o fim do dia

        //Manda a mensagem de avançar tempo para todas as plantas da cena
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Interact");
        foreach (GameObject o in objects) {
            o.BroadcastMessage("AdvanceTime");
        }
        currentDay += 1;

        //Faz 
        refreshCountdown += 1;
        if (refreshCountdown == magicRefreshDays) {
            if (magicRefreshRate != 0) {
                currentMagicUses += magicRefreshRate;
            } else {
                currentMagicUses = maxMagicUses;
            }
            refreshCountdown = 0;
        }

        Debug.Log("Day " + currentDay);
    }

}
