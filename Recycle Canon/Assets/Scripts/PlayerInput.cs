using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalizedRight()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();


        inputVector = inputVector.normalized;

        return inputVector;
    }

    public Vector2 GetMovementVectorNormalizedLeft() 
    {
        Vector2 inputVector = playerInputActions.Player.Aim.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
