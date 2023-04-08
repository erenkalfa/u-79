using System;
using UnityEngine;

public class SwingHammer : InteractivePuzzlePiece<HingeJoint>
{
    [Range(1f, 10f)]
    public float power = 5f;
    private NetworkManager networkManager;
    
    void Awake ()
    {
        rb.mass = power;
    }

    private void Start()
    {
        networkManager = GameManager.Instance.networkManager;
    }

    private void Update()
    {
        physicsComponent.useMotor = networkManager.hammerState;
    }
    
}