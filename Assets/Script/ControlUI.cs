using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlUI : MonoBehaviour
{
    [Header("For spell")]
    [SerializeField]
    private RectTransform image_icon;

    [SerializeField]
    private Slider slots_bar;
    private int n_slots_spells=3;

    [SerializeField]
    private TMP_Text dayText;

    public void SetSlotsSize(float n_slots_spells)
    {
        slots_bar.value = n_slots_spells;
    }

    public void ConsumeSlots(){
        n_slots_spells--;
        SetSlotsSize(n_slots_spells);
        Debug.Log("Consumindo slot...");
    }

    public int GetSlotsSize(){
       return n_slots_spells;
    }

    public void UpdateDayName (int day) {
        if (day <= 6)
            dayText.SetText(Constants.dayNames[day]);
    }
}
