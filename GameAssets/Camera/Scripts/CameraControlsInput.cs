using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControlsInput : MonoBehaviour, PlayerControls.ICameraMotionMapActions
{

    public PlayerControls PlayerControls { get; private set; }

    public Vector2 CameraMovementInput { get; private set; }

    public Vector2 MousePos { get; private set; }

    public bool MouseClicked { get; private set; }

    public bool MousePeformed { get; private set; }

    public bool MouseJustClicked { get; private set; }

    public bool MouseReleased { get; private set; }

    public bool ModPressed { get; private set; }

    public bool RightMouseClicked { get; private set; }

    public bool RightMouseReleased { get; private set; }

    private void OnEnable()
    {
        PlayerControls = new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.CameraMotionMap.Enable();
        PlayerControls.CameraMotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.CameraMotionMap.Disable();
        PlayerControls.CameraMotionMap.RemoveCallbacks(this);
    }


    public void OnMovement(InputAction.CallbackContext context)
    {
        CameraMovementInput = context.ReadValue<Vector2>();
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }

    public void OnMouseClicked(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Mouse Clicked");
            MouseClicked = true;
            MouseReleased = false;

        }
        if (context.canceled)
        {
            Debug.Log("Mouse Released");
            MouseClicked = false;
            MouseReleased = true;
        }
        // MouseClicked = context.ReadValueAsButton();
    }

    public void OnModKey(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ModPressed = true;
        }
        else
        {
            ModPressed = false;
        }
    }

    public void OnRightMouseClicked(InputAction.CallbackContext context)
    {
         if (context.started)
        {
            RightMouseClicked = true;
            RightMouseReleased = false;

        }
        if (context.canceled)
        {
            RightMouseClicked = false;
            RightMouseReleased = true;
        }
    }

    public void OnLeftMouseClicked(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
