//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Configs/ControlsSettings.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Fleetio.Core.Input
{
    public partial class @ControlsSettings : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @ControlsSettings()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""ControlsSettings"",
    ""maps"": [
        {
            ""name"": ""BattleFieldControl"",
            ""id"": ""90f9953d-f2cc-4237-ae9e-d65e84213eec"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""cdf505dc-436c-44a6-862b-e7ffd9810485"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""a4ba8cdd-9f6f-4c1c-ba98-d55616c61079"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SelectOne"",
                    ""type"": ""Button"",
                    ""id"": ""3208bc04-a740-4e53-9d36-8dc78bf965ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BulkSelection"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e0b40186-a1d8-42e3-987b-204dbe4d01fc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""427c4c92-2b54-4f8c-a8ef-349a7809fc70"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df19c7ee-b83e-49a0-a7c9-295cf8cfef9d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d761152-2f3a-487a-b453-bf79b8242216"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectOne"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Mouse Drag"",
                    ""id"": ""1ba15a20-6a12-4b36-8ef9-c6144c1b91ca"",
                    ""path"": ""MouseDrag"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulkSelection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""315b14ef-d71b-4dd7-81f6-aea35531c54f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulkSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Delta"",
                    ""id"": ""ef2ffeef-c82f-4ea0-b708-120ee5e4a3d4"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulkSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Position"",
                    ""id"": ""29010e7e-47af-4a72-b75b-7177378e368b"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulkSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // BattleFieldControl
            m_BattleFieldControl = asset.FindActionMap("BattleFieldControl", throwIfNotFound: true);
            m_BattleFieldControl_MousePosition = m_BattleFieldControl.FindAction("MousePosition", throwIfNotFound: true);
            m_BattleFieldControl_Move = m_BattleFieldControl.FindAction("Move", throwIfNotFound: true);
            m_BattleFieldControl_SelectOne = m_BattleFieldControl.FindAction("SelectOne", throwIfNotFound: true);
            m_BattleFieldControl_BulkSelection = m_BattleFieldControl.FindAction("BulkSelection", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // BattleFieldControl
        private readonly InputActionMap m_BattleFieldControl;
        private IBattleFieldControlActions m_BattleFieldControlActionsCallbackInterface;
        private readonly InputAction m_BattleFieldControl_MousePosition;
        private readonly InputAction m_BattleFieldControl_Move;
        private readonly InputAction m_BattleFieldControl_SelectOne;
        private readonly InputAction m_BattleFieldControl_BulkSelection;
        public struct BattleFieldControlActions
        {
            private @ControlsSettings m_Wrapper;
            public BattleFieldControlActions(@ControlsSettings wrapper) { m_Wrapper = wrapper; }
            public InputAction @MousePosition => m_Wrapper.m_BattleFieldControl_MousePosition;
            public InputAction @Move => m_Wrapper.m_BattleFieldControl_Move;
            public InputAction @SelectOne => m_Wrapper.m_BattleFieldControl_SelectOne;
            public InputAction @BulkSelection => m_Wrapper.m_BattleFieldControl_BulkSelection;
            public InputActionMap Get() { return m_Wrapper.m_BattleFieldControl; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BattleFieldControlActions set) { return set.Get(); }
            public void SetCallbacks(IBattleFieldControlActions instance)
            {
                if (m_Wrapper.m_BattleFieldControlActionsCallbackInterface != null)
                {
                    @MousePosition.started -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnMousePosition;
                    @Move.started -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnMove;
                    @SelectOne.started -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnSelectOne;
                    @SelectOne.performed -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnSelectOne;
                    @SelectOne.canceled -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnSelectOne;
                    @BulkSelection.started -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnBulkSelection;
                    @BulkSelection.performed -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnBulkSelection;
                    @BulkSelection.canceled -= m_Wrapper.m_BattleFieldControlActionsCallbackInterface.OnBulkSelection;
                }
                m_Wrapper.m_BattleFieldControlActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @SelectOne.started += instance.OnSelectOne;
                    @SelectOne.performed += instance.OnSelectOne;
                    @SelectOne.canceled += instance.OnSelectOne;
                    @BulkSelection.started += instance.OnBulkSelection;
                    @BulkSelection.performed += instance.OnBulkSelection;
                    @BulkSelection.canceled += instance.OnBulkSelection;
                }
            }
        }
        public BattleFieldControlActions @BattleFieldControl => new BattleFieldControlActions(this);
        public interface IBattleFieldControlActions
        {
            void OnMousePosition(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
            void OnSelectOne(InputAction.CallbackContext context);
            void OnBulkSelection(InputAction.CallbackContext context);
        }
    }
}