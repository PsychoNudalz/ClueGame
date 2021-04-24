// GENERATED AUTOMATICALLY FROM 'Assets/Anson/GameControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameControls"",
    ""maps"": [
        {
            ""name"": ""Player Map"",
            ""id"": ""04e9545a-a40c-4da0-aba3-f9b388348a07"",
            ""actions"": [
                {
                    ""name"": ""MouseMoving"",
                    ""type"": ""Value"",
                    ""id"": ""b31892a1-5373-41c8-a4bb-be628f601481"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ControllerMoving"",
                    ""type"": ""Button"",
                    ""id"": ""306aaa66-e88e-41b6-81f5-f7d5313226f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveToken"",
                    ""type"": ""Button"",
                    ""id"": ""96101e2e-4a9d-4778-8e5c-b6c5b8735948"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestButton1"",
                    ""type"": ""Button"",
                    ""id"": ""ec3aa672-90ac-4480-bd17-ab2dfc4e8e5d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestButton2"",
                    ""type"": ""Button"",
                    ""id"": ""af807560-72d4-4ee9-99f9-f21e06cec307"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestButton3"",
                    ""type"": ""Button"",
                    ""id"": ""85065c9e-e088-4ce0-8f95-2f48d757e5f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TestButton4"",
                    ""type"": ""Button"",
                    ""id"": ""ded4a885-4f47-44a8-bf7c-d5b4169fdcb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7138706a-87fb-4426-9d21-7efc58e23e3a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseMoving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""20bc06a6-6aa3-4794-bd69-56f877a9ebf5"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControllerMoving"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4689544b-5775-4c95-9e5b-ccda187febe6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControllerMoving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6bbee908-9a6e-4822-a36c-80fd0a928b56"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControllerMoving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ca43b758-be0a-421a-b4a5-fd7c57d39d79"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControllerMoving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4dbcc264-30a8-481e-bcc7-705b35c4c181"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControllerMoving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4014ee00-24d6-46cf-9d61-7626bae82dc6"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27c522df-493a-4d40-a94a-5429df55414c"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff81d2e3-f419-4879-ae7a-db4c9cefbacd"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdaf691d-b39f-4161-9cd4-e0f59ad6fcc8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveToken"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""354c5bbd-20b3-4d57-9648-dc90799856e8"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TestButton4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player Map
        m_PlayerMap = asset.FindActionMap("Player Map", throwIfNotFound: true);
        m_PlayerMap_MouseMoving = m_PlayerMap.FindAction("MouseMoving", throwIfNotFound: true);
        m_PlayerMap_ControllerMoving = m_PlayerMap.FindAction("ControllerMoving", throwIfNotFound: true);
        m_PlayerMap_MoveToken = m_PlayerMap.FindAction("MoveToken", throwIfNotFound: true);
        m_PlayerMap_TestButton1 = m_PlayerMap.FindAction("TestButton1", throwIfNotFound: true);
        m_PlayerMap_TestButton2 = m_PlayerMap.FindAction("TestButton2", throwIfNotFound: true);
        m_PlayerMap_TestButton3 = m_PlayerMap.FindAction("TestButton3", throwIfNotFound: true);
        m_PlayerMap_TestButton4 = m_PlayerMap.FindAction("TestButton4", throwIfNotFound: true);
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

    // Player Map
    private readonly InputActionMap m_PlayerMap;
    private IPlayerMapActions m_PlayerMapActionsCallbackInterface;
    private readonly InputAction m_PlayerMap_MouseMoving;
    private readonly InputAction m_PlayerMap_ControllerMoving;
    private readonly InputAction m_PlayerMap_MoveToken;
    private readonly InputAction m_PlayerMap_TestButton1;
    private readonly InputAction m_PlayerMap_TestButton2;
    private readonly InputAction m_PlayerMap_TestButton3;
    private readonly InputAction m_PlayerMap_TestButton4;
    public struct PlayerMapActions
    {
        private @GameControls m_Wrapper;
        public PlayerMapActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMoving => m_Wrapper.m_PlayerMap_MouseMoving;
        public InputAction @ControllerMoving => m_Wrapper.m_PlayerMap_ControllerMoving;
        public InputAction @MoveToken => m_Wrapper.m_PlayerMap_MoveToken;
        public InputAction @TestButton1 => m_Wrapper.m_PlayerMap_TestButton1;
        public InputAction @TestButton2 => m_Wrapper.m_PlayerMap_TestButton2;
        public InputAction @TestButton3 => m_Wrapper.m_PlayerMap_TestButton3;
        public InputAction @TestButton4 => m_Wrapper.m_PlayerMap_TestButton4;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMapActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMapActions instance)
        {
            if (m_Wrapper.m_PlayerMapActionsCallbackInterface != null)
            {
                @MouseMoving.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMouseMoving;
                @MouseMoving.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMouseMoving;
                @MouseMoving.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMouseMoving;
                @ControllerMoving.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnControllerMoving;
                @ControllerMoving.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnControllerMoving;
                @ControllerMoving.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnControllerMoving;
                @MoveToken.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveToken;
                @MoveToken.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveToken;
                @MoveToken.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnMoveToken;
                @TestButton1.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton1;
                @TestButton1.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton1;
                @TestButton1.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton1;
                @TestButton2.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton2;
                @TestButton2.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton2;
                @TestButton2.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton2;
                @TestButton3.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton3;
                @TestButton3.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton3;
                @TestButton3.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton3;
                @TestButton4.started -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton4;
                @TestButton4.performed -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton4;
                @TestButton4.canceled -= m_Wrapper.m_PlayerMapActionsCallbackInterface.OnTestButton4;
            }
            m_Wrapper.m_PlayerMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MouseMoving.started += instance.OnMouseMoving;
                @MouseMoving.performed += instance.OnMouseMoving;
                @MouseMoving.canceled += instance.OnMouseMoving;
                @ControllerMoving.started += instance.OnControllerMoving;
                @ControllerMoving.performed += instance.OnControllerMoving;
                @ControllerMoving.canceled += instance.OnControllerMoving;
                @MoveToken.started += instance.OnMoveToken;
                @MoveToken.performed += instance.OnMoveToken;
                @MoveToken.canceled += instance.OnMoveToken;
                @TestButton1.started += instance.OnTestButton1;
                @TestButton1.performed += instance.OnTestButton1;
                @TestButton1.canceled += instance.OnTestButton1;
                @TestButton2.started += instance.OnTestButton2;
                @TestButton2.performed += instance.OnTestButton2;
                @TestButton2.canceled += instance.OnTestButton2;
                @TestButton3.started += instance.OnTestButton3;
                @TestButton3.performed += instance.OnTestButton3;
                @TestButton3.canceled += instance.OnTestButton3;
                @TestButton4.started += instance.OnTestButton4;
                @TestButton4.performed += instance.OnTestButton4;
                @TestButton4.canceled += instance.OnTestButton4;
            }
        }
    }
    public PlayerMapActions @PlayerMap => new PlayerMapActions(this);
    public interface IPlayerMapActions
    {
        void OnMouseMoving(InputAction.CallbackContext context);
        void OnControllerMoving(InputAction.CallbackContext context);
        void OnMoveToken(InputAction.CallbackContext context);
        void OnTestButton1(InputAction.CallbackContext context);
        void OnTestButton2(InputAction.CallbackContext context);
        void OnTestButton3(InputAction.CallbackContext context);
        void OnTestButton4(InputAction.CallbackContext context);
    }
}
