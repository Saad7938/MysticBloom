using UnityEngine;
using UnityEngine.InputSystem;

public class App : MonoBehaviour
{
    private InputAction exitAction;

    private void OnEnable()
    {
        exitAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        exitAction.performed += OnExitPerformed;
        exitAction.Enable();
    }

    private void OnDisable()
    {
        exitAction.performed -= OnExitPerformed;
        exitAction.Disable();
    }

    private void OnExitPerformed(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
