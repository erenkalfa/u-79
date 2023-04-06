using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CheckInputs()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
      
        look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        jump = Input.GetButton("Jump");
        sprint = Input.GetKey(KeyCode.LeftShift);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !hasFocus;
    }
}
