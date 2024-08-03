using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlUI : MonoBehaviour
{
    [Header("For spell")]
    [SerializeField]
    private RectTransform image_icon;

    [SerializeField]
    private RectTransform slots_bar;
    private int n_slots_spells=3;

    
    

    public void SetSlotsSize(float n_slots_spells)
    {
        float base_width = image_icon.rect.width;
        Vector2 offsetMin = slots_bar.offsetMin;

        switch (n_slots_spells)
        {
            case 0:
                offsetMin.x = 225;
                break;
            case 1:
                offsetMin.x = 150;
                break;
            case 2:
                offsetMin.x = 75;
                break;
            case 3:
                offsetMin.x = 0;
                break;

            default:
                break;
        }
        slots_bar.offsetMin = offsetMin;
    }

    public void ConsumeSlots(){
        n_slots_spells--;
        SetSlotsSize(n_slots_spells);
        Debug.Log("Consumindo slot...");
    }

    public int GetSlotsSize(){
       return n_slots_spells; 
    }
}
