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
    public struct PlayerMapActions
    {
        private @GameControls m_Wrapper;
        public PlayerMapActions(@GameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseMoving => m_Wrapper.m_PlayerMap_MouseMoving;
        public InputAction @ControllerMoving => m_Wrapper.m_PlayerMap_ControllerMoving;
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
            }
        }
    }
    public PlayerMapActions @PlayerMap => new PlayerMapActions(this);
    public interface IPlayerMapActions
    {
        void OnMouseMoving(InputAction.CallbackContext context);
        void OnControllerMoving(InputAction.CallbackContext context);
    }
}
