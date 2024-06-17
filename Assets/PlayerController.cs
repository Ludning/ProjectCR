using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private void OnEnable()
    {
        InputHandler.Instance.RegisterInputHandler(InputActionType.Move, OnMove);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 vec = context.ReadValue<Vector2>();
        Debug.Log(vec);
    }
}
