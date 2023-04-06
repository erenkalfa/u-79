using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMovementController movementController;
    private PlayerInputManager inputManager;
    
    private void Awake()
    {
        movementController = GetComponent<PlayerMovementController>();
        inputManager = GetComponent<PlayerInputManager>();
    }
    
    private void Update()
    {
        inputManager.CheckInputs();
        
        movementController.Move();
        movementController.GroundedCheck();
        movementController.JumpAndGravity();
    }

    private void LateUpdate()
    {
        movementController.CameraRotation();
    }
}
