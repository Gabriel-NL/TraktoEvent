using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalEvents : MonoBehaviour
{
    // Define a static event
    public static event System.Action OnClickTile;

    // Method to invoke the event
    public static void ClickTile(){
        OnClickTile?.Invoke();
    }

}
