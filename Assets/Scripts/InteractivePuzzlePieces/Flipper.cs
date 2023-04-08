using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : InteractivePuzzlePiece<HingeJoint>
{
    [Range(500f, 2000f)]
    public float power = 700f;
    public bool state;

    void Awake ()
    {
        JointMotor flipperMotor = physicsComponent.motor;
        flipperMotor.targetVelocity = power;
        physicsComponent.motor = flipperMotor;
    }
    
    private void Update()
    {
        state = Input.GetKey(KeyCode.X);
        physicsComponent.useMotor = state;
    }
}
