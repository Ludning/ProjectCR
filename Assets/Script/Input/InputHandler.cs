using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : SingleTonMono<InputHandler>
{
    private GameInput playerInput;

    private void Awake()
    {
        playerInput = new GameInput();
    }

    public void RegisterInputHandler(InputActionType actionType, Action<InputAction.CallbackContext> action)
    {
        InputAction inputAction = playerInput.FindAction(actionType.ToString());
        if (inputAction != null)
        {
            inputAction.started += action;
            inputAction.performed += action;
            inputAction.canceled += action;
            Debug.LogWarning("입력 액션 등록.");
        }
        else
        {
            Debug.LogWarning("입력 액션 참조가 유효하지 않습니다.");
        }
    }
    public void UnregisterInputHandler(InputActionType actionType, Action<InputAction.CallbackContext> action)
    {
        InputAction inputAction = playerInput.FindAction(actionType.ToString());
        if (inputAction != null)
        {
            inputAction.started -= action;
            inputAction.performed -= action;
            inputAction.canceled -= action;
        }
        else
        {
            Debug.LogWarning("입력 액션 참조가 유효하지 않습니다.");
        }
    }
}
