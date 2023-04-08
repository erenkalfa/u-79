using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trapdoor : InteractivePuzzlePiece<ConstantForce>
{
    [Range(10f, 50f)]
    public float openSpeed = 12f;
    public bool state;

    void Awake ()
    {
        physicsComponent.force = new Vector3(0f, openSpeed, 0f);
    }

    private void Update()
    {
        state = Input.GetKey(KeyCode.X);
        physicsComponent.enabled = state;
    }

}
