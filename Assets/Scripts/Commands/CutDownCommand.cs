using System.Diagnostics;
using Farm.Grid;
using Farm.Helpers;
using Farm.Player;
using UnityEngine;

namespace Farm.Commands
{
    public sealed class CutDownCommand : ICommand
    {
        private readonly CellLogic _cell;

        public CutDownCommand(CellLogic cell)
        {
            _cell = cell;
        }

        public void Execute()
        {
            UnityEngine.Debug.Log($"Cutting down {nameof(_cell)}"); // Co
            void HarvestAction() => _cell.Harvest();

            // TODO new state
            var nextState = PlayerController.Instance.PickupState;

            // TODO new animation hash
            var newTask = new Task(_cell.transform.position, nextState, AnimatorHash.Pickup, HarvestAction);
            PlayerController.Instance.SetTask(newTask);
        }
    }
}