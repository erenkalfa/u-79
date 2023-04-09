using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : InteractivePuzzlePiece<HingeJoint>
{
    [Range(500f, 4000f)]
    public float power = 700f;
    private NetworkManager networkManager;
    public int puzzleId;

    void Awake ()
    {
        JointMotor flipperMotor = physicsComponent.motor;
        flipperMotor.targetVelocity = power;
        physicsComponent.motor = flipperMotor;
    }

    private void Start()
    {
        networkManager = GameManager.Instance.networkManager;
    }

    private void Update()
    {
        physicsComponent.useMotor = networkManager.puzzleStates[puzzleId];
    }
}
