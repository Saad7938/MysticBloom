using UnityEngine;
using UnityEngine.InputSystem;

public class InsectClickHandler : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private LayerMask _insectLayer;

    private Camera _mainCamera;
    private InputAction _selectAction;
    private InputAction _pointerPositionAction;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        _selectAction = _playerInput.actions.FindAction("Gameplay/Select");
        _pointerPositionAction = _playerInput.actions.FindAction("Gameplay/PointerPosition");
    }

    private void OnEnable()
    {
        if (_selectAction != null)
        {
            _selectAction.performed += OnSelectPerformed;
        }
    }

    private void OnDisable()
    {
        if (_selectAction != null)
        {
            _selectAction.performed -= OnSelectPerformed;
        }
    }

    private void OnSelectPerformed(InputAction.CallbackContext context)
    {
        Vector2 pointerPosition = _pointerPositionAction.ReadValue<Vector2>();
        Ray ray = _mainCamera.ScreenPointToRay(pointerPosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _insectLayer))
        {
            Insect insect = hit.collider.GetComponent<Insect>();
            if (insect != null)
            {
                Debug.Log("Insect clicked: " + insect.name);
                insect.HandleClick();
            }
        }
    }
}
