using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    
    
    [Header("Mobile Input")]
    private bool isDeviceMobile;
    public VariableJoystick joystick;

    private void Awake()
    {
        isDeviceMobile = GameManager.Instance.isDeviceMobile;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CheckInputs()
    {
        if (isDeviceMobile)
        {
            move = joystick.Direction;
            sprint = move.magnitude > 0.7f;
        }
        else
        {
            move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      
            look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            jump = Input.GetButton("Jump");
            sprint = Input.GetKey(KeyCode.LeftShift);
        }
        
    }

    public void LookInput(Vector2 newLookState)
    {
        look = newLookState;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }
    
    
    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !hasFocus;
    }
}
