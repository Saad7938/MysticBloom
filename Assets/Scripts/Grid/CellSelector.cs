using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Farm.Grid
{
    public sealed class CellSelector : MonoBehaviour
    {
        [SerializeField] private LayerMask _cellLayerMask;
        [SerializeField] private PlayerInput _playerInput;

        private Camera _mainCamera;
        private InputAction _selectAction;
        private InputAction _pointerPositionAction;

        public static System.Action<CellLogic> OnCellClicked;
        public static System.Action<CellLogic> OnCellHighlight;

        private void Awake()
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null)
            {
                Debug.LogError("Main Camera not found. Ensure a Camera is tagged as 'MainCamera'.", this);
                return;
            }

            if (_playerInput == null)
            {
                _playerInput = GetComponent<PlayerInput>();
                if (_playerInput == null)
                {
                    Debug.LogError("PlayerInput component missing on CellSelector. Please add it.", this);
                    return;
                }
            }

            if (_playerInput.actions == null)
            {
                Debug.LogError("PlayerInput actions asset not assigned. Assign 'FarmInputActions' to PlayerInput.", this);
                return;
            }

            _selectAction = _playerInput.actions.FindAction("Gameplay/Select");
            _pointerPositionAction = _playerInput.actions.FindAction("Gameplay/PointerPosition");

            if (_selectAction == null || _pointerPositionAction == null)
            {
                Debug.LogError("Select or PointerPosition action not found in 'Gameplay' Action Map. Check FarmInputActions.", this);
            }
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

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject() || _pointerPositionAction == null)
            {
                OnCellHighlight?.Invoke(null);
                return;
            }

            // Get pointer position (mouse or touch)
            Vector2 pointerPosition = _pointerPositionAction.ReadValue<Vector2>();
            CellLogic cell = GetCellAtPointer(pointerPosition);

            // Trigger highlight event
            OnCellHighlight?.Invoke(cell);
        }

        private void OnSelectPerformed(InputAction.CallbackContext context)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // Get cell at pointer position when select action is performed
            Vector2 pointerPosition = _pointerPositionAction.ReadValue<Vector2>();
            CellLogic cell = GetCellAtPointer(pointerPosition);

            // Trigger click event
            OnCellClicked?.Invoke(cell);
            //cell?.TryKillInsect();
        }

        private CellLogic GetCellAtPointer(Vector2 pointerPosition)
        {
            CellLogic cell = null;
            Ray ray = _mainCamera.ScreenPointToRay(pointerPosition);
            if (Physics.Raycast(ray, out var hitData, Mathf.Infinity, _cellLayerMask))
            {
                cell = hitData.transform.GetComponent<CellLogic>();
            }
            return cell;
        }
    }
}