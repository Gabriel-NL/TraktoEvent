using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeProgression : MonoBehaviour
{
    public int currentDay = 0;
    public int deadlineDay = 9;
    
    private Image dayProgressBar=null;
    private float stageWidth;

    void Awake () {
        AdjustProgressBar();
    }

    void AdjustProgressBar () {
        //(VISUAL) Ajusta a largura da imagem 'progress bar' dependendo do dia atual
        dayProgressBar.rectTransform.sizeDelta = new Vector2(currentDay * stageWidth, dayProgressBar.rectTransform.sizeDelta.y);
    }

    public void NextDay () {
        //(VISUAL) Chamar alguma transição para indicar o fim do dia

        //Manda a mensagem de avançar tempo para todas as plantas da cena
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Interact");
        foreach (GameObject o in objects) {
            o.BroadcastMessage("AdvanceTime");
        }
    }
}
