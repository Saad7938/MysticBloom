using Farm.FSM;
using Farm.FSM.States.CellStates;
using Farm.Audio;
using Food;
using UnityEngine;
using GameData;

namespace Farm.Grid
{
    public sealed class CellLogic : MonoBehaviour
    {
        private IState _freeState;
        private IState _plantedState;
        private StateMachine _stateMachine;
        private MeshRenderer _meshRenderer;
        public bool IsFree => _stateMachine.CurrentState == _freeState;
        public FoodBase CurrentFood { get; private set; }

        private void Awake()
        {
            _freeState = new FreeState();
            _plantedState = new PlantedState();
            _stateMachine = new StateMachine(_freeState);
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            CellSelector.OnCellHighlight += OnCellHighlightHandler;
        }

        private void OnDestroy()
        {
            CellSelector.OnCellHighlight -= OnCellHighlightHandler;
        }

        private void OnCellHighlightHandler(CellLogic cell)
        {
            if (!cell || cell != this)
            {
                Unselect();
                return;
            }

            Select();
        }

        private void ChangeState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void Plant(FoodBase foodBase)
        {
            if (!IsFree)
            {
                return;
            }
            if (GameDataManager.getWaterCount() < 3)
            {
                return;
            }
            //_audioManager.playSFX(_audioManager.Watering);

            CurrentFood = Instantiate(foodBase, transform.position, Quaternion.identity, transform);
            GameDataManager.ReduceWater(2);
            CurrentFood.Initialize(this); 
            ChangeState(_plantedState);
        }

        public void Harvest()
        {
            Debug.Log("Harvesting food");
            if (IsFree)
            {
                return;
            }

            if (CurrentFood.Interact())
            {
                ChangeState(_freeState);
            }
        }

        private void Select()
        {
            _meshRenderer.material.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }

        private void Unselect()
        {
            _meshRenderer.material.color = Color.white;
        }
        public void OnFoodDestroyed()
        {
            CurrentFood = null;
            ChangeState(_freeState);
        }

    }
}