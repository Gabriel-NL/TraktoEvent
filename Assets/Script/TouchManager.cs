using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction touchPosition;
    private InputAction touchPress;


    public int currentAction=0;

    private void Awake () {
        playerInput = GetComponent<PlayerInput>();
        touchPosition = playerInput.actions["TouchPosition"];
        touchPress = playerInput.actions["TouchPress"];
    }

    private void OnEnable () {
        touchPress.performed += TouchPressed;
    }

    private void OnDisable () {
        touchPress.performed -= TouchPressed;
    }

    private void TouchPressed (InputAction.CallbackContext context) {
        Vector2 screenPos = touchPosition.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hitData;
        Physics.Raycast(ray, out hitData);
        
        if (hitData.collider != null && hitData.collider.tag == "Interact") {
            hitData.collider.gameObject.BroadcastMessage("Interact", currentAction);
        }
    }



}
