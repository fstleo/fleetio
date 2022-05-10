using System;
using Fleetio.Core.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fleetio.Input
{
    public class MouseInputListener : ControlsSettings.IBattleFieldControlActions
    {
        private bool _selectionHappening;
        private Bounds _selectionBounds;
        
        public void OnMousePosition(InputAction.CallbackContext context)
        {
        }
        
        public void OnSelectOne(InputAction.CallbackContext context)
        {


        }

        public void OnBulkSelection(InputAction.CallbackContext context)
        {

        }
        
        public void OnMove(InputAction.CallbackContext context)
        {

        }
    }
}