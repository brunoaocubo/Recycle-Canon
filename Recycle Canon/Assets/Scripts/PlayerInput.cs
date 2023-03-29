using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //private PlayerInputActions playerInputActions;
    [SerializeField] private Joystick leftJoystick;
    [SerializeField] private Joystick rightJoystick;

    private void Awake()
    {
         
    }

    public Vector2 GetMovementVectorNormalizedRight()
    {
        Vector2 inputVector = new Vector2(rightJoystick.Direction.x, rightJoystick.Direction.y);
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public Vector2 GetAimVectorNormalizedLeft()
    {
        Vector2 inputVector = new Vector2(leftJoystick.Direction.x, leftJoystick.Direction.y);
        inputVector = inputVector.normalized;
        return inputVector;
    }

    /* Input Novo
public Vector2 GetMovementVectorNormalizedRight()
{
    Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    inputVector = inputVector.normalized;
    return inputVector;
}

    public Vector2 GetAimVectorNormalizedLeft() 
{
    Vector2 inputVector = playerInputActions.Player.Aim.ReadValue<Vector2>();
    inputVector = inputVector.normalized; 
    return inputVector;
}*/

    public Touch GetTouchScreen() 
    {
        Touch touch = Input.GetTouch(0);      
        return touch;
    }
}
